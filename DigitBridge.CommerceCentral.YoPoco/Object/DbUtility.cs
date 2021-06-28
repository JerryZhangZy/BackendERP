using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Reactive.Subjects;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.YoPoco
{
    public enum TransactionAction
    {
        BeforeBegin,
        AfterBegin,
        BeforeCommit,
        AfterCommit,
        BeforeAbort,
        AfterAbort
    }

    public class ConnectionThreadContext
    {
        public IDbConnection Connection { get; protected internal set; }
        public IDbTransaction Transaction { get; protected internal set; }
        public Subject<TransactionAction> TransactionEvents { get; protected internal set; }
        public Dictionary<string, object> Data { get; }

        public ConnectionThreadContext()
        {
            Data = new Dictionary<string, object>();
            TransactionEvents = new Subject<TransactionAction>();
        }
    }

    public class DbUtility
    {
        public static readonly DateTime _SqlMinDateTime = new DateTime(1753, 1, 1);
        public static readonly int DefaultTimeout = 180;
        public static readonly string TimestampFormat = "yyyy-MM-dd HH:mm:ss";
        public static readonly string DateFormat = "yyyy-MM-dd";

        private static readonly List<Action<TransactionAction>> _onTransactionEnd =
            new List<Action<TransactionAction>>();

        private static ThreadContext<ConnectionThreadContext> _connectionThreadContext =
            new ThreadContext<ConnectionThreadContext>("DatabaseUtil._connectionThreadContext");

        public static string ConnectionString => ConfigurationManager.AppSettings["dsn"];
        public static Dictionary<string, object> Data => _connectionThreadContext.Value?.Data;
        public static IDbTransaction Transaction => _connectionThreadContext.Value?.Transaction;
        public static IDbConnection Connection => _connectionThreadContext.Value?.Connection;

        public static bool IsInTransaction => (_connectionThreadContext.Value?.Transaction != null) &&
                                              (_connectionThreadContext.Value.Connection.State !=
                                               ConnectionState.Closed);

        public static bool IsConnectionOpen =>
            (_connectionThreadContext.Value?.Connection?.State == ConnectionState.Open);

        #region connection and transaction

        public static IDbConnection CreateConnection(MsSqlUniversalDBConfig config)
        {
            var SqlConn = new SqlConnection(config.DbConnectionString);
            if (config.UseAzureManagedIdentity)
                SqlConn.AccessToken = config.AccessToken;
            return SqlConn;
        }

        public static void CloseConnection()
        {
            var dbConnection = Connection;
            if (dbConnection.State != ConnectionState.Closed)
                dbConnection.Close();
            dbConnection?.Dispose();

            var connectionThreadContext = _connectionThreadContext.Value;
            connectionThreadContext.TransactionEvents.Dispose();
            _connectionThreadContext.Clear();
        }

        public static void Begin(MsSqlUniversalDBConfig config, bool withTx = true)
        {
            var connectionThreadContext = _connectionThreadContext.Value;
            if (connectionThreadContext != null)
                throw new Exception("Cannot have two connections for the same execution ThreadContext");

            connectionThreadContext = _connectionThreadContext.Set(new ConnectionThreadContext());
            try
            {
                if (withTx)
                    connectionThreadContext.TransactionEvents.OnNext(TransactionAction.BeforeBegin);

                connectionThreadContext.Connection = CreateConnection(config);
                connectionThreadContext.Connection.Open();
                Debug.Assert(connectionThreadContext.Connection.State == ConnectionState.Open);
            }
            catch (Exception)
            {
                CloseConnection();
                throw;
            }

            if (!withTx || connectionThreadContext.Transaction != null)
                return;

            connectionThreadContext.Transaction =
                connectionThreadContext.Connection.BeginTransaction(IsolationLevel.ReadCommitted);
            connectionThreadContext.TransactionEvents.OnNext(TransactionAction.AfterBegin);
        }

        public static void Begin(string dsn, bool withTx = true)
        {
            Begin(new MsSqlUniversalDBConfig(dsn), withTx);
        }

        public static void Begin()
        {
            Begin(ConnectionString, true);
        }

        public static void Abort()
        {
            var connectionThreadContext = _connectionThreadContext.Value;
            if (connectionThreadContext == null)
                return;

            var DbTransaction = connectionThreadContext.Transaction;
            var transactionEvents = connectionThreadContext.TransactionEvents;
            try
            {
                if (DbTransaction != null)
                {

                    Invoke(TransactionAction.BeforeAbort);

                    transactionEvents?.OnNext(TransactionAction.BeforeAbort);

                    if ((DbTransaction.Connection != null) && (DbTransaction.Connection.State == ConnectionState.Open))
                    {
                        DbTransaction.Rollback();
                        DbTransaction.Dispose();
                    }
                }
            }
            finally
            {
                var dbConnection = connectionThreadContext.Connection;
                dbConnection?.Dispose();

                _connectionThreadContext.Clear();
                if (transactionEvents != null)
                {
                    transactionEvents.OnNext(TransactionAction.AfterAbort);
                    transactionEvents.OnCompleted();
                    transactionEvents.Dispose();
                    connectionThreadContext.TransactionEvents = null;
                }
            }
        }

        public static void Commit()
        {
            var connectionThreadContext = _connectionThreadContext.Value;
            if (connectionThreadContext == null)
                return;

            var dbTransaction = connectionThreadContext.Transaction;
            var transactionAction = TransactionAction.AfterCommit;
            var transactionEvents = connectionThreadContext.TransactionEvents;
            try
            {
                if (dbTransaction?.Connection != null && (dbTransaction.Connection.State == ConnectionState.Open))
                {
                    transactionEvents?.OnNext(TransactionAction.BeforeCommit);
                    Invoke(TransactionAction.BeforeCommit);
                    dbTransaction.Commit();
                    dbTransaction.Dispose();
                }
            }
            catch (Exception ex)
            {
                try
                {
                    if (dbTransaction != null)
                    {
                        Invoke(TransactionAction.BeforeAbort);
                        transactionEvents?.OnNext(TransactionAction.BeforeAbort);
                    }
                }
                finally
                {
                    if (dbTransaction != null)
                    {
                        if ((dbTransaction.Connection != null) &&
                            (dbTransaction.Connection.State == ConnectionState.Open))
                            dbTransaction.Rollback();
                        dbTransaction.Dispose();
                        transactionAction = TransactionAction.AfterAbort;
                    }
                }

                if (ex.InnerException != null)
                    throw ex.InnerException;
                throw;
            }
            finally
            {
                var dbConnection = Connection;
                dbConnection?.Dispose();
                _connectionThreadContext.Clear();
                if (transactionEvents != null)
                {
                    transactionEvents.OnNext(transactionAction);
                    transactionEvents.OnCompleted();
                    transactionEvents.Dispose();
                    connectionThreadContext.TransactionEvents = null;
                }
            }
        }

        public static void OnTransactionEnd(Action<TransactionAction> a) => _onTransactionEnd.Add(a);
        private static void Invoke(TransactionAction action) => _onTransactionEnd.ForEach(t => t(action));

        public static IObservable<TransactionAction> TransactionEvents =>
            _connectionThreadContext.Value?.TransactionEvents;

        public static void SetThreadContext() => _connectionThreadContext.Set(new ConnectionThreadContext());
        public static void ClearThreadContext() => _connectionThreadContext.Clear();

        #endregion connection and transaction

        #region command
        private static SqlCommand CreateCommandDefault(string strCommand, CommandType commandType, params IDataParameter[] parameters)
        {
            SqlCommand sqlCommand = new SqlCommand(strCommand)
            {
                CommandTimeout = DefaultTimeout,
                CommandType = commandType
            };

            if (parameters != null)
            {
                foreach (var parameter in parameters)
                    sqlCommand.Parameters.Add(parameter);
            }

            return sqlCommand;
        }

        public static IDbCommand CreateCommand(string strCommand, CommandType commandType, params IDataParameter[] parameters)
        {
            SqlCommand sqlCommand = CreateCommandDefault(strCommand, commandType, parameters);

            if (Transaction == null)
            {
                Begin();
            }

            var sqlTransaction = (SqlTransaction)Transaction;
            if (sqlTransaction != null)
            {
                var sqlConnection = sqlTransaction.Connection;
                sqlCommand.Transaction = sqlTransaction;
                sqlCommand.Connection = sqlConnection;
            }
            return sqlCommand;
        }
        #endregion command
    }

    public static class DbUtilityExtensions
    {
        public static IDataParameter ToSqlParameter(this int nValue, string name)
        {
            return new SqlParameter($"@{name}", SqlDbType.Int, 4) {Value = nValue};
        }

        public static IDataParameter ToSqlParameter(this long nValue, string name)
        {
            return new SqlParameter($"@{name}", SqlDbType.BigInt) {Value = nValue};
        }

        public static IDataParameter ToSqlParameter(this byte[] value, string name)
        {
            return new SqlParameter($"@{name}", SqlDbType.Image) {Value = value};
        }

        public static IDataParameter ToSqlParameter(this Guid[] values, string name)
        {
            var param = new SqlParameter($"@{name}", SqlDbType.VarBinary, 7900);
            if (values == null)
                return param;

            var value = new byte[values.Length * 16];
            var i = 0;
            foreach (var guid in values)
            {
                if (Guid.Empty == guid)
                    continue;

                guid.ToByteArray().CopyTo(value, i * 16);
                i++;
            }

            param.Value = value;
            return param;
        }

        public static IDataParameter ToSqlParameter(this bool? bValue, string name)
        {
            var param = new SqlParameter($"@{name}", SqlDbType.Bit, 1);
            if (bValue.HasValue)
                param.Value = bValue.Value;
            return param;
        }
        public static IDataParameter ToSqlParameter(this string strValue, string name)
        {
            return new SqlParameter($"@{name}", SqlDbType.VarChar, strValue.Length) {Value = strValue};
        }

        public static IDataParameter ToSqlParameter(this Guid? value, string name)
        {
            var param = new SqlParameter($"@{name}", SqlDbType.UniqueIdentifier);
            if (!value.HasValue || Guid.Empty == value)
                param.Value = DBNull.Value;
            else
                param.Value = value;
            return param;
        }

        public static IDataParameter ToSqlParameter(this float fValue, string name)
        {
            return new SqlParameter($"@{name}", DbType.Single) {Value = fValue};
        }

        public static IDataParameter ToSqlParameter(this double? dValue, string name)
        {
            return new SqlParameter($"@{name}", DbType.Double)
            {
                Value = dValue switch
                {
                    null => null,
                    double.NaN => 0.0,
                    _ => dValue
                }
            };
        }

        public static IDataParameter ToSqlParameter(this decimal? dValue, string name, byte precision = 24, byte scale = 6)
        {
            return new SqlParameter($"@{name}", DbType.Decimal)
            {
                Value = dValue switch
                {
                    null => 0.0,
                    _ => dValue
                },
                Precision = precision,
                Scale = scale
            };
        }
        public static IDataParameter ToSqlParameter(this decimal dValue, string name, byte precision = 24, byte scale = 6)
        {
            return new SqlParameter($"@{name}", SqlDbType.Decimal)
            {
                Value = dValue, Precision = precision, Scale = scale
            };
        }

        public static IDataParameter ToSqlParameter(this DateTime? dtValue, string name)
        {
            var param = new SqlParameter($"@{name}", SqlDbType.DateTime);
            if (dtValue < DbUtility._SqlMinDateTime)
                dtValue = DbUtility._SqlMinDateTime;
            if (dtValue.HasValue)
                param.Value = dtValue.Value;
            return param;
        }

    }
}