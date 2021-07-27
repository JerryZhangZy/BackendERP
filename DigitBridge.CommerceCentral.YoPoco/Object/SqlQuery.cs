using DigitBridge.Base.Utility;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.YoPoco
{
    public delegate void ObjectNotificationHandler<in T>(T Object);

    public delegate TResult Func<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9, in T10, in T11, in T12,
        in T13, in T14, in T15, in T16, in T17, out TResult>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6,
        T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16,
        T17 arg17);

    public delegate TResult Func<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9, in T10, in T11, in T12,
        in T13, in T14, in T15, in T16, in T17, in T18, out TResult>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6,
        T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16,
        T17 arg17, T18 arg18);

    public delegate TResult Func<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9, in T10, in T11, in T12,
        in T13, in T14, in T15, in T16, in T17, in T18, in T19, out TResult>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6,
        T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16,
        T17 arg17, T18 arg18, T19 arg19);

    public delegate TResult Func<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9, in T10, in T11, in T12,
        in T13, in T14, in T15, in T16, in T17, in T18, in T19, in T20, out TResult>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6,
        T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16,
        T17 arg17, T18 arg18, T19 arg19, T20 arg20);

    public static class SqlQuery
    {
        public static readonly DateTime _SqlMinDateTime = new DateTime(1753, 1, 1);
        public static readonly DateTime _AppMinDateTime = new DateTime(1900, 1, 1);
        public static readonly DateTime _AppMaxDateTime = DateTime.Now.AddYears(100);

        public static SerializationBinder SerializationBinder { get; set; }

        static SqlQuery()
        {
        }

        private static void OnTransactionEnd(TransactionAction Action)
        {
            if (Action == TransactionAction.BeforeCommit)
            {
            }
            else if (Action == TransactionAction.BeforeAbort)
            {
            }
        }

        private static string BuildCacheKey(string storedProcedure, IEnumerable<IDataParameter> parms)
        {
            var sb = new StringBuilder();
            sb.Append(storedProcedure);
            foreach (var dataParameter in parms)
                sb.Append(dataParameter.Value);

            return sb.ToString();
        }

        public static void ExecuteNonQuery(string cmd, params IDataParameter[] parameters) =>
            ExecuteNonQuery(cmd, CommandType.Text, parameters);

        public static void ExecuteNonQuery(string cmd, CommandType commandType, params IDataParameter[] parameters)
        {
            using var dbCommand = DataBaseFactory.CreateCommand(cmd, commandType, parameters);
            dbCommand.ExecuteNonQuery();
        }

        public static async Task ExecuteNonQueryAsync(string cmd, params IDataParameter[] parameters) =>
            await ExecuteNonQueryAsync(cmd, CommandType.Text, parameters).ConfigureAwait(false);

        public static async Task ExecuteNonQueryAsync(string cmd, CommandType commandType, params IDataParameter[] parameters)
        {
            using var dbCommand = DataBaseFactory.CreateCommand(cmd, commandType, parameters);
            await ((SqlCommand)dbCommand).ExecuteNonQueryAsync().ConfigureAwait(false);
        }

        public static T ExecuteScalar<T>(string commandText, params IDataParameter[] parameters) =>
            ExecuteScalar<T>(commandText, CommandType.Text, parameters);

        public static T ExecuteScalar<T>(string commandText, CommandType commandType, params IDataParameter[] parameters)
        {
            using IDbCommand dbCommand = DataBaseFactory.CreateCommand(commandText, commandType, parameters);
            var val = dbCommand.ExecuteScalar();
            if (val == null) return default(T);

            // Handle nullable types
            var u = Nullable.GetUnderlyingType(typeof(T));
            if (u != null && (val == null || val == DBNull.Value))
                return default(T);

            return (T)Convert.ChangeType(val, u == null ? typeof(T) : u);
        }

        public static async Task<T> ExecuteScalarAsync<T>(string commandText, params IDataParameter[] parameters) =>
            await ExecuteScalarAsync<T>(commandText, CommandType.Text, parameters).ConfigureAwait(false);

        public static async Task<T> ExecuteScalarAsync<T>(string commandText, CommandType commandType, params IDataParameter[] parameters)
        {
            using var dbCommand = DataBaseFactory.CreateCommand(commandText, commandType, parameters) as SqlCommand;
            var val = await dbCommand.ExecuteScalarAsync();
            if (val == null) return default(T);

            // Handle nullable types
            var u = Nullable.GetUnderlyingType(typeof(T));
            if (u != null && (val == null || val == DBNull.Value))
                return default(T);

            return (T)Convert.ChangeType(val, u == null ? typeof(T) : u);
        }


        public static IDataReader ExecuteCommand(string commandText, params IDataParameter[] parameters) =>
            ExecuteCommand(commandText, CommandType.Text, parameters);

        public static IDataReader ExecuteCommand(string commandText, CommandType commandType, params IDataParameter[] parameters)
        {
            using IDbCommand dbCommand = DataBaseFactory.CreateCommand(commandText, commandType, parameters);
            return dbCommand.ExecuteReader();
        }

        public static async Task<SqlDataReader> ExecuteCommandAsync(string commandText, params IDataParameter[] parameters) =>
            await ExecuteCommandAsync(commandText, CommandType.Text, parameters).ConfigureAwait(false);

        public static async Task<SqlDataReader> ExecuteCommandAsync(string commandText, CommandType commandType, params IDataParameter[] parameters)
        {
            using var dbCommand = DataBaseFactory.CreateCommand(commandText, commandType, parameters) as SqlCommand;
            return await dbCommand.ExecuteReaderAsync().ConfigureAwait(false);
        }

        #region excute command and return sqlreader value to callback function, upto 20 columns

        private static List<R> _Execute<R>(IDataReader reader, Func<IDataReader, R> f)
        {
            var results = new List<R>();
            using (var dataReader = reader)
            {
                do { while (dataReader.Read()) { results.Add(f(dataReader)); } } while (dataReader.NextResult());
            }
            return results;
        }

        private static async Task<List<R>> _ExecuteAsync<R>(IDataReader reader, Func<IDataReader, R> f)
        {
            var results = new List<R>();
            using (var dataReader = (SqlDataReader)reader)
            {
                do { while (await dataReader.ReadAsync().ConfigureAwait(false)) { results.Add(f(dataReader)); } } while (await dataReader.NextResultAsync().ConfigureAwait(false));
            }
            return results;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IDataReader CreateDefaultCommand(string cmd, CommandType commandType, params IDataParameter[] parameters)
        {
            return ExecuteCommand(cmd, commandType, parameters);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IDataReader CreateDefaultCommand(string cmd, params IDataParameter[] parameters)
        {
            return ExecuteCommand(cmd, CommandType.Text, parameters);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static async Task<SqlDataReader> CreateDefaultCommandAsync(string cmd, CommandType commandType, params IDataParameter[] parameters)
        {
            return await ExecuteCommandAsync(cmd, commandType, parameters).ConfigureAwait(false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static async Task<SqlDataReader> CreateDefaultCommandAsync(string cmd, params IDataParameter[] parameters)
        {
            return await ExecuteCommandAsync(cmd, CommandType.Text, parameters).ConfigureAwait(false);
        }

        // 1 column
        public static List<R> Execute<P1, R>(string cmd, Func<P1, R> func, params IDataParameter[] parameters) =>
            _Execute(CreateDefaultCommand(cmd, parameters), reader => func(reader.GetValue<P1>(0)));

        public static List<R> Execute<P1, R>(string cmd, CommandType commandType, Func<P1, R> func, params IDataParameter[] parameters) =>
            _Execute(CreateDefaultCommand(cmd, commandType, parameters), reader => func(reader.GetValue<P1>(0)));

        public static async Task<List<R>> ExecuteAsync<P1, R>(string cmd, Func<P1, R> func, params IDataParameter[] parameters) =>
            await _ExecuteAsync(await CreateDefaultCommandAsync(cmd, parameters).ConfigureAwait(false), reader => func(reader.GetValue<P1>(0))).ConfigureAwait(false);

        public static async Task<List<R>> ExecuteAsync<P1, R>(string cmd, CommandType commandType, Func<P1, R> func, params IDataParameter[] parameters) =>
            await _ExecuteAsync(await CreateDefaultCommandAsync(cmd, commandType, parameters).ConfigureAwait(false), reader => func(reader.GetValue<P1>(0))).ConfigureAwait(false);

        // 2 columns
        public static List<R> Execute<P1, P2, R>(string cmd, Func<P1, P2, R> func, params IDataParameter[] parameters) =>
            _Execute(CreateDefaultCommand(cmd, parameters), reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1)));

        public static List<R> Execute<P1, P2, R>(string cmd, Func<P1, P2, R> func, CommandType commandType, params IDataParameter[] parameters) =>
            _Execute(CreateDefaultCommand(cmd, commandType, parameters), reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1)));

        public static async Task<List<R>> ExecuteAsync<P1, P2, R>(string cmd,
            Func<P1, P2, R> func, params IDataParameter[] parameters) =>
            await _ExecuteAsync(await CreateDefaultCommandAsync(cmd, parameters).ConfigureAwait(false),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1))).ConfigureAwait(false);

        public static async Task<List<R>> ExecuteAsync<P1, P2, R>(string cmd, CommandType commandType,
            Func<P1, P2, R> func, params IDataParameter[] parameters) =>
            await _ExecuteAsync(await CreateDefaultCommandAsync(cmd, commandType, parameters).ConfigureAwait(false),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1))).ConfigureAwait(false);

        // 3 columns
        public static List<R> Execute<P1, P2, P3, R>(string cmd,
            Func<P1, P2, P3, R> func, params IDataParameter[] parameters) =>
            _Execute(CreateDefaultCommand(cmd, parameters),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2)));

        public static List<R> Execute<P1, P2, P3, R>(string cmd, CommandType commandType,
            Func<P1, P2, P3, R> func, params IDataParameter[] parameters) =>
            _Execute(CreateDefaultCommand(cmd, commandType, parameters),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2)));

        public static async Task<List<R>> ExecuteAsync<P1, P2, P3, R>(string cmd,
            Func<P1, P2, P3, R> func, params IDataParameter[] parameters) => await _ExecuteAsync(
            await CreateDefaultCommandAsync(cmd, parameters).ConfigureAwait(false),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2))).ConfigureAwait(false);

        public static async Task<List<R>> ExecuteAsync<P1, P2, P3, R>(string cmd, CommandType commandType,
            Func<P1, P2, P3, R> func, params IDataParameter[] parameters) => await _ExecuteAsync(
            await CreateDefaultCommandAsync(cmd, commandType, parameters).ConfigureAwait(false),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2))).ConfigureAwait(false);

        // 4 columns
        public static List<R> Execute<P1, P2, P3, P4, R>(string cmd,
            Func<P1, P2, P3, P4, R> func, params IDataParameter[] parameters) =>
            _Execute(CreateDefaultCommand(cmd, parameters),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3)));

        public static List<R> Execute<P1, P2, P3, P4, R>(string cmd, CommandType commandType,
            Func<P1, P2, P3, P4, R> func, params IDataParameter[] parameters) =>
            _Execute(CreateDefaultCommand(cmd, commandType, parameters),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3)));

        public static async Task<List<R>> ExecuteAsync<P1, P2, P3, P4, R>(string cmd,
            Func<P1, P2, P3, P4, R> func, params IDataParameter[] parameters) =>
            await _ExecuteAsync(await CreateDefaultCommandAsync(cmd, parameters).ConfigureAwait(false),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3))).ConfigureAwait(false);

        public static async Task<List<R>> ExecuteAsync<P1, P2, P3, P4, R>(string cmd, CommandType commandType,
            Func<P1, P2, P3, P4, R> func, params IDataParameter[] parameters) => await _ExecuteAsync(
            await CreateDefaultCommandAsync(cmd, commandType, parameters).ConfigureAwait(false),
            reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                reader.GetValue<P4>(3))).ConfigureAwait(false);

        // 5 columns
        public static List<R> Execute<P1, P2, P3, P4, P5, R>(string cmd,
            Func<P1, P2, P3, P4, P5, R> func, params IDataParameter[] parameters) =>
            _Execute(CreateDefaultCommand(cmd, parameters),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4)));

        public static List<R> Execute<P1, P2, P3, P4, P5, R>(string cmd, CommandType commandType,
            Func<P1, P2, P3, P4, P5, R> func, params IDataParameter[] parameters) =>
            _Execute(CreateDefaultCommand(cmd, commandType, parameters),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4)));

        public static async Task<List<R>> ExecuteAsync<P1, P2, P3, P4, P5, R>(string cmd,
            Func<P1, P2, P3, P4, P5, R> func, params IDataParameter[] parameters) =>
            await _ExecuteAsync(await CreateDefaultCommandAsync(cmd, parameters).ConfigureAwait(false),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4))).ConfigureAwait(false);

        public static async Task<List<R>> ExecuteAsync<P1, P2, P3, P4, P5, R>(string cmd, CommandType commandType,
            Func<P1, P2, P3, P4, P5, R> func, params IDataParameter[] parameters) =>
            await _ExecuteAsync(await CreateDefaultCommandAsync(cmd, commandType, parameters).ConfigureAwait(false),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4))).ConfigureAwait(false);

        // 6 columns
        public static List<R> Execute<P1, P2, P3, P4, P5, P6, R>(string cmd,
            Func<P1, P2, P3, P4, P5, P6, R> func, params IDataParameter[] parameters) =>
            _Execute(CreateDefaultCommand(cmd, parameters),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4), reader.GetValue<P6>(5)));

        public static List<R> Execute<P1, P2, P3, P4, P5, P6, R>(string cmd, CommandType commandType,
            Func<P1, P2, P3, P4, P5, P6, R> func, params IDataParameter[] parameters) =>
            _Execute(CreateDefaultCommand(cmd, commandType, parameters),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4), reader.GetValue<P6>(5)));

        public static async Task<List<R>> ExecuteAsync<P1, P2, P3, P4, P5, P6, R>(string cmd,
            Func<P1, P2, P3, P4, P5, P6, R> func, params IDataParameter[] parameters) =>
            await _ExecuteAsync(await CreateDefaultCommandAsync(cmd, parameters).ConfigureAwait(false),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4), reader.GetValue<P6>(5))).ConfigureAwait(false);

        public static async Task<List<R>> ExecuteAsync<P1, P2, P3, P4, P5, P6, R>(string cmd, CommandType commandType,
            Func<P1, P2, P3, P4, P5, P6, R> func, params IDataParameter[] parameters) =>
            await _ExecuteAsync(await CreateDefaultCommandAsync(cmd, commandType, parameters).ConfigureAwait(false),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4), reader.GetValue<P6>(5))).ConfigureAwait(false);

        // 7 columns
        public static List<R> Execute<P1, P2, P3, P4, P5, P6, P7, R>(string cmd,
            Func<P1, P2, P3, P4, P5, P6, P7, R> func, params IDataParameter[] parameters) =>
            Execute(cmd, CommandType.Text, func, parameters);

        public static List<R> Execute<P1, P2, P3, P4, P5, P6, P7, R>(string cmd, CommandType commandType,
            Func<P1, P2, P3, P4, P5, P6, P7, R> func, params IDataParameter[] parameters) =>
            _Execute(CreateDefaultCommand(cmd, commandType, parameters),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4), reader.GetValue<P6>(5), reader.GetValue<P7>(6)));

        public static async Task<List<R>> ExecuteAsync<P1, P2, P3, P4, P5, P6, P7, R>(string cmd,
            Func<P1, P2, P3, P4, P5, P6, P7, R> func, params IDataParameter[] parameters) =>
            await _ExecuteAsync(CreateDefaultCommand(cmd, parameters),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4), reader.GetValue<P6>(5), reader.GetValue<P7>(6))).ConfigureAwait(false);

        public static async Task<List<R>> ExecuteAsync<P1, P2, P3, P4, P5, P6, P7, R>(string cmd, CommandType commandType,
            Func<P1, P2, P3, P4, P5, P6, P7, R> func, params IDataParameter[] parameters) =>
            await _ExecuteAsync(CreateDefaultCommand(cmd, commandType, parameters),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4), reader.GetValue<P6>(5), reader.GetValue<P7>(6))).ConfigureAwait(false);

        // 8 columns
        public static List<R> Execute<P1, P2, P3, P4, P5, P6, P7, P8, R>(string cmd,
            Func<P1, P2, P3, P4, P5, P6, P7, P8, R> func, params IDataParameter[] parameters) =>
            _Execute(CreateDefaultCommand(cmd, parameters),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4), reader.GetValue<P6>(5), reader.GetValue<P7>(6),
                    reader.GetValue<P8>(7)));

        public static List<R> Execute<P1, P2, P3, P4, P5, P6, P7, P8, R>(string cmd, CommandType commandType,
            Func<P1, P2, P3, P4, P5, P6, P7, P8, R> func, params IDataParameter[] parameters) =>
            _Execute(CreateDefaultCommand(cmd, commandType, parameters),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4), reader.GetValue<P6>(5), reader.GetValue<P7>(6),
                    reader.GetValue<P8>(7)));

        public static async Task<List<R>> ExecuteAsync<P1, P2, P3, P4, P5, P6, P7, P8, R>(string cmd,
            Func<P1, P2, P3, P4, P5, P6, P7, P8, R> func, params IDataParameter[] parameters) =>
            await _ExecuteAsync(await CreateDefaultCommandAsync(cmd, parameters).ConfigureAwait(false),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4), reader.GetValue<P6>(5), reader.GetValue<P7>(6),
                    reader.GetValue<P8>(7))).ConfigureAwait(false);

        public static async Task<List<R>> ExecuteAsync<P1, P2, P3, P4, P5, P6, P7, P8, R>(string cmd, CommandType commandType,
            Func<P1, P2, P3, P4, P5, P6, P7, P8, R> func, params IDataParameter[] parameters) =>
            await _ExecuteAsync(await CreateDefaultCommandAsync(cmd, commandType, parameters).ConfigureAwait(false),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4), reader.GetValue<P6>(5), reader.GetValue<P7>(6),
                    reader.GetValue<P8>(7))).ConfigureAwait(false);

        // 9 columns
        public static List<R> Execute<P1, P2, P3, P4, P5, P6, P7, P8, P9, R>(string cmd,
            Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, R> func, params IDataParameter[] parameters) =>
            _Execute(CreateDefaultCommand(cmd, parameters),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4), reader.GetValue<P6>(5), reader.GetValue<P7>(6),
                    reader.GetValue<P8>(7), reader.GetValue<P9>(8)));

        public static List<R> Execute<P1, P2, P3, P4, P5, P6, P7, P8, P9, R>(string cmd, CommandType commandType,
            Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, R> func, params IDataParameter[] parameters) =>
            _Execute(CreateDefaultCommand(cmd, commandType, parameters),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4), reader.GetValue<P6>(5), reader.GetValue<P7>(6),
                    reader.GetValue<P8>(7), reader.GetValue<P9>(8)));

        public static async Task<List<R>> ExecuteAsync<P1, P2, P3, P4, P5, P6, P7, P8, P9, R>(string cmd,
            Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, R> func, params IDataParameter[] parameters) =>
            await _ExecuteAsync(await CreateDefaultCommandAsync(cmd, parameters).ConfigureAwait(false),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4), reader.GetValue<P6>(5), reader.GetValue<P7>(6),
                    reader.GetValue<P8>(7), reader.GetValue<P9>(8))).ConfigureAwait(false);

        public static async Task<List<R>> ExecuteAsync<P1, P2, P3, P4, P5, P6, P7, P8, P9, R>(string cmd, CommandType commandType,
            Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, R> func, params IDataParameter[] parameters) =>
            await _ExecuteAsync(await CreateDefaultCommandAsync(cmd, commandType, parameters).ConfigureAwait(false),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4), reader.GetValue<P6>(5), reader.GetValue<P7>(6),
                    reader.GetValue<P8>(7), reader.GetValue<P9>(8))).ConfigureAwait(false);

        // 10 columns
        public static List<R> Execute<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, R>(string cmd,
            Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, R> func, params IDataParameter[] parameters) =>
            _Execute(CreateDefaultCommand(cmd, parameters),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4), reader.GetValue<P6>(5), reader.GetValue<P7>(6),
                    reader.GetValue<P8>(7), reader.GetValue<P9>(8), reader.GetValue<P10>(9)));

        public static List<R> Execute<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, R>(string cmd, CommandType commandType,
            Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, R> func, params IDataParameter[] parameters) =>
            _Execute(CreateDefaultCommand(cmd, commandType, parameters),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4), reader.GetValue<P6>(5), reader.GetValue<P7>(6),
                    reader.GetValue<P8>(7), reader.GetValue<P9>(8), reader.GetValue<P10>(9)));

        public static async Task<List<R>> ExecuteAsync<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, R>(string cmd,
            Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, R> func, params IDataParameter[] parameters) =>
            await _ExecuteAsync(await CreateDefaultCommandAsync(cmd, parameters).ConfigureAwait(false),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4), reader.GetValue<P6>(5), reader.GetValue<P7>(6),
                    reader.GetValue<P8>(7), reader.GetValue<P9>(8), reader.GetValue<P10>(9))).ConfigureAwait(false);

        public static async Task<List<R>> ExecuteAsync<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, R>(string cmd, CommandType commandType,
            Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, R> func, params IDataParameter[] parameters) =>
            await _ExecuteAsync(await CreateDefaultCommandAsync(cmd, commandType, parameters).ConfigureAwait(false),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4), reader.GetValue<P6>(5), reader.GetValue<P7>(6),
                    reader.GetValue<P8>(7), reader.GetValue<P9>(8), reader.GetValue<P10>(9))).ConfigureAwait(false);

        // 11 columns
        public static List<R> Execute<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, R>(string cmd,
            Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, R> func, params IDataParameter[] parameters) =>
            _Execute(CreateDefaultCommand(cmd, parameters),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4), reader.GetValue<P6>(5), reader.GetValue<P7>(6),
                    reader.GetValue<P8>(7), reader.GetValue<P9>(8), reader.GetValue<P10>(9), reader.GetValue<P11>(10)));

        public static List<R> Execute<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, R>(string cmd, CommandType commandType,
            Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, R> func, params IDataParameter[] parameters) =>
            _Execute(CreateDefaultCommand(cmd, commandType, parameters),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4), reader.GetValue<P6>(5), reader.GetValue<P7>(6),
                    reader.GetValue<P8>(7), reader.GetValue<P9>(8), reader.GetValue<P10>(9), reader.GetValue<P11>(10)));

        public static async Task<List<R>> ExecuteAsync<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, R>(string cmd,
            Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, R> func, params IDataParameter[] parameters) =>
            await _ExecuteAsync(await CreateDefaultCommandAsync(cmd, parameters).ConfigureAwait(false),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4), reader.GetValue<P6>(5), reader.GetValue<P7>(6),
                    reader.GetValue<P8>(7), reader.GetValue<P9>(8), reader.GetValue<P10>(9), reader.GetValue<P11>(10))).ConfigureAwait(false);

        public static async Task<List<R>> ExecuteAsync<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, R>(string cmd, CommandType commandType,
            Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, R> func, params IDataParameter[] parameters) =>
            await _ExecuteAsync(await CreateDefaultCommandAsync(cmd, commandType, parameters).ConfigureAwait(false),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4), reader.GetValue<P6>(5), reader.GetValue<P7>(6),
                    reader.GetValue<P8>(7), reader.GetValue<P9>(8), reader.GetValue<P10>(9), reader.GetValue<P11>(10))).ConfigureAwait(false);

        // 12 columns
        public static List<R> Execute<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, R>(string cmd,
            Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, R> func, params IDataParameter[] parameters) =>
            _Execute(CreateDefaultCommand(cmd, parameters),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4), reader.GetValue<P6>(5), reader.GetValue<P7>(6),
                    reader.GetValue<P8>(7), reader.GetValue<P9>(8), reader.GetValue<P10>(9), reader.GetValue<P11>(10),
                    reader.GetValue<P12>(11)));

        public static List<R> Execute<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, R>(string cmd, CommandType commandType,
            Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, R> func, params IDataParameter[] parameters) =>
            _Execute(CreateDefaultCommand(cmd, commandType, parameters),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4), reader.GetValue<P6>(5), reader.GetValue<P7>(6),
                    reader.GetValue<P8>(7), reader.GetValue<P9>(8), reader.GetValue<P10>(9), reader.GetValue<P11>(10),
                    reader.GetValue<P12>(11)));

        public static async Task<List<R>> ExecuteAsync<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, R>(string cmd,
            Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, R> func, params IDataParameter[] parameters) =>
            await _ExecuteAsync(CreateDefaultCommand(cmd, parameters),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4), reader.GetValue<P6>(5), reader.GetValue<P7>(6),
                    reader.GetValue<P8>(7), reader.GetValue<P9>(8), reader.GetValue<P10>(9), reader.GetValue<P11>(10),
                    reader.GetValue<P12>(11))).ConfigureAwait(false);

        public static async Task<List<R>> ExecuteAsync<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, R>(string cmd, CommandType commandType,
            Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, R> func, params IDataParameter[] parameters) =>
            await _ExecuteAsync(CreateDefaultCommand(cmd, commandType, parameters),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4), reader.GetValue<P6>(5), reader.GetValue<P7>(6),
                    reader.GetValue<P8>(7), reader.GetValue<P9>(8), reader.GetValue<P10>(9), reader.GetValue<P11>(10),
                    reader.GetValue<P12>(11))).ConfigureAwait(false);

        // 13 columns
        public static List<R> Execute<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, R>(string cmd,
            Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, R> func, params IDataParameter[] parameters) =>
            _Execute(CreateDefaultCommand(cmd, parameters),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4), reader.GetValue<P6>(5), reader.GetValue<P7>(6),
                    reader.GetValue<P8>(7), reader.GetValue<P9>(8), reader.GetValue<P10>(9), reader.GetValue<P11>(10),
                    reader.GetValue<P12>(11), reader.GetValue<P13>(12)));

        public static List<R> Execute<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, R>(string cmd, CommandType commandType,
            Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, R> func, params IDataParameter[] parameters) =>
            _Execute(CreateDefaultCommand(cmd, commandType, parameters),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4), reader.GetValue<P6>(5), reader.GetValue<P7>(6),
                    reader.GetValue<P8>(7), reader.GetValue<P9>(8), reader.GetValue<P10>(9), reader.GetValue<P11>(10),
                    reader.GetValue<P12>(11), reader.GetValue<P13>(12)));

        public static async Task<List<R>> ExecuteAsync<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, R>(string cmd,
            Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, R> func, params IDataParameter[] parameters) =>
            await _ExecuteAsync(await CreateDefaultCommandAsync(cmd, parameters).ConfigureAwait(false),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4), reader.GetValue<P6>(5), reader.GetValue<P7>(6),
                    reader.GetValue<P8>(7), reader.GetValue<P9>(8), reader.GetValue<P10>(9), reader.GetValue<P11>(10),
                    reader.GetValue<P12>(11), reader.GetValue<P13>(12))).ConfigureAwait(false);

        public static async Task<List<R>> ExecuteAsync<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, R>(string cmd, CommandType commandType,
            Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, R> func, params IDataParameter[] parameters) =>
            await _ExecuteAsync(await CreateDefaultCommandAsync(cmd, commandType, parameters).ConfigureAwait(false),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4), reader.GetValue<P6>(5), reader.GetValue<P7>(6),
                    reader.GetValue<P8>(7), reader.GetValue<P9>(8), reader.GetValue<P10>(9), reader.GetValue<P11>(10),
                    reader.GetValue<P12>(11), reader.GetValue<P13>(12))).ConfigureAwait(false);

        // 14 columns
        public static List<R> Execute<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, R>(string cmd,
            Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, R> func, params IDataParameter[] parameters) =>
            _Execute(CreateDefaultCommand(cmd, parameters),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4), reader.GetValue<P6>(5), reader.GetValue<P7>(6),
                    reader.GetValue<P8>(7), reader.GetValue<P9>(8), reader.GetValue<P10>(9), reader.GetValue<P11>(10),
                    reader.GetValue<P12>(11), reader.GetValue<P13>(12), reader.GetValue<P14>(13)));

        public static List<R> Execute<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, R>(string cmd, CommandType commandType,
            Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, R> func, params IDataParameter[] parameters) =>
            _Execute(CreateDefaultCommand(cmd, commandType, parameters),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4), reader.GetValue<P6>(5), reader.GetValue<P7>(6),
                    reader.GetValue<P8>(7), reader.GetValue<P9>(8), reader.GetValue<P10>(9), reader.GetValue<P11>(10),
                    reader.GetValue<P12>(11), reader.GetValue<P13>(12), reader.GetValue<P14>(13)));

        public static async Task<List<R>> ExecuteAsync<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, R>(string cmd,
            Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, R> func, params IDataParameter[] parameters) =>
            await _ExecuteAsync(await CreateDefaultCommandAsync(cmd, parameters).ConfigureAwait(false),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4), reader.GetValue<P6>(5), reader.GetValue<P7>(6),
                    reader.GetValue<P8>(7), reader.GetValue<P9>(8), reader.GetValue<P10>(9), reader.GetValue<P11>(10),
                    reader.GetValue<P12>(11), reader.GetValue<P13>(12), reader.GetValue<P14>(13))).ConfigureAwait(false);

        public static async Task<List<R>> ExecuteAsync<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, R>(string cmd, CommandType commandType,
            Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, R> func, params IDataParameter[] parameters) =>
            await _ExecuteAsync(await CreateDefaultCommandAsync(cmd, commandType, parameters).ConfigureAwait(false),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4), reader.GetValue<P6>(5), reader.GetValue<P7>(6),
                    reader.GetValue<P8>(7), reader.GetValue<P9>(8), reader.GetValue<P10>(9), reader.GetValue<P11>(10),
                    reader.GetValue<P12>(11), reader.GetValue<P13>(12), reader.GetValue<P14>(13))).ConfigureAwait(false);

        // 15 columns
        public static List<R> Execute<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, R>(string cmd,
            Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, R> func, params IDataParameter[] parameters) =>
            _Execute(CreateDefaultCommand(cmd, parameters),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4), reader.GetValue<P6>(5), reader.GetValue<P7>(6),
                    reader.GetValue<P8>(7), reader.GetValue<P9>(8), reader.GetValue<P10>(9), reader.GetValue<P11>(10),
                    reader.GetValue<P12>(11), reader.GetValue<P13>(12), reader.GetValue<P14>(13),
                    reader.GetValue<P15>(14)));

        public static List<R> Execute<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, R>(string cmd, CommandType commandType,
            Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, R> func, params IDataParameter[] parameters) =>
            _Execute(CreateDefaultCommand(cmd, commandType, parameters),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4), reader.GetValue<P6>(5), reader.GetValue<P7>(6),
                    reader.GetValue<P8>(7), reader.GetValue<P9>(8), reader.GetValue<P10>(9), reader.GetValue<P11>(10),
                    reader.GetValue<P12>(11), reader.GetValue<P13>(12), reader.GetValue<P14>(13),
                    reader.GetValue<P15>(14)));

        public static async Task<List<R>> ExecuteAsync<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, R>(string cmd,
            Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, R> func, params IDataParameter[] parameters) =>
            await _ExecuteAsync(await CreateDefaultCommandAsync(cmd, parameters).ConfigureAwait(false),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4), reader.GetValue<P6>(5), reader.GetValue<P7>(6),
                    reader.GetValue<P8>(7), reader.GetValue<P9>(8), reader.GetValue<P10>(9), reader.GetValue<P11>(10),
                    reader.GetValue<P12>(11), reader.GetValue<P13>(12), reader.GetValue<P14>(13),
                    reader.GetValue<P15>(14))).ConfigureAwait(false);

        public static async Task<List<R>> ExecuteAsync<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, R>(string cmd, CommandType commandType,
            Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, R> func, params IDataParameter[] parameters) =>
            await _ExecuteAsync(await CreateDefaultCommandAsync(cmd, commandType, parameters).ConfigureAwait(false),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4), reader.GetValue<P6>(5), reader.GetValue<P7>(6),
                    reader.GetValue<P8>(7), reader.GetValue<P9>(8), reader.GetValue<P10>(9), reader.GetValue<P11>(10),
                    reader.GetValue<P12>(11), reader.GetValue<P13>(12), reader.GetValue<P14>(13),
                    reader.GetValue<P15>(14))).ConfigureAwait(false);

        // 16 columns
        public static List<R> Execute<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, R>(string cmd,
            Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, R> func, params IDataParameter[] parameters) =>
            _Execute(CreateDefaultCommand(cmd, parameters),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4), reader.GetValue<P6>(5), reader.GetValue<P7>(6),
                    reader.GetValue<P8>(7), reader.GetValue<P9>(8), reader.GetValue<P10>(9), reader.GetValue<P11>(10),
                    reader.GetValue<P12>(11), reader.GetValue<P13>(12), reader.GetValue<P14>(13), reader.GetValue<P15>(14),
                    reader.GetValue<P16>(15)));

        public static List<R> Execute<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, R>(string cmd, CommandType commandType,
            Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, R> func, params IDataParameter[] parameters) =>
            _Execute(CreateDefaultCommand(cmd, commandType, parameters),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4), reader.GetValue<P6>(5), reader.GetValue<P7>(6),
                    reader.GetValue<P8>(7), reader.GetValue<P9>(8), reader.GetValue<P10>(9), reader.GetValue<P11>(10),
                    reader.GetValue<P12>(11), reader.GetValue<P13>(12), reader.GetValue<P14>(13), reader.GetValue<P15>(14),
                    reader.GetValue<P16>(15)));

        public static async Task<List<R>> ExecuteAsync<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, R>(string cmd,
            Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, R> func, params IDataParameter[] parameters) =>
            await _ExecuteAsync(await CreateDefaultCommandAsync(cmd, parameters).ConfigureAwait(false),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4), reader.GetValue<P6>(5), reader.GetValue<P7>(6),
                    reader.GetValue<P8>(7), reader.GetValue<P9>(8), reader.GetValue<P10>(9), reader.GetValue<P11>(10),
                    reader.GetValue<P12>(11), reader.GetValue<P13>(12), reader.GetValue<P14>(13), reader.GetValue<P15>(14),
                    reader.GetValue<P16>(15))).ConfigureAwait(false);

        public static async Task<List<R>> ExecuteAsync<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, R>(string cmd, CommandType commandType,
            Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, R> func, params IDataParameter[] parameters) =>
            await _ExecuteAsync(await CreateDefaultCommandAsync(cmd, commandType, parameters).ConfigureAwait(false),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4), reader.GetValue<P6>(5), reader.GetValue<P7>(6),
                    reader.GetValue<P8>(7), reader.GetValue<P9>(8), reader.GetValue<P10>(9), reader.GetValue<P11>(10),
                    reader.GetValue<P12>(11), reader.GetValue<P13>(12), reader.GetValue<P14>(13), reader.GetValue<P15>(14),
                    reader.GetValue<P16>(15))).ConfigureAwait(false);

        // 17 columns
        public static List<R> Execute<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, R>(string cmd,
            Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, R> func, params IDataParameter[] parameters) =>
            _Execute(CreateDefaultCommand(cmd, parameters),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4), reader.GetValue<P6>(5), reader.GetValue<P7>(6),
                    reader.GetValue<P8>(7), reader.GetValue<P9>(8), reader.GetValue<P10>(9), reader.GetValue<P11>(10),
                    reader.GetValue<P12>(11), reader.GetValue<P13>(12), reader.GetValue<P14>(13), reader.GetValue<P15>(14),
                    reader.GetValue<P16>(15), reader.GetValue<P17>(16)));

        public static List<R> Execute<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, R>(string cmd, CommandType commandType,
            Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, R> func, params IDataParameter[] parameters) =>
            _Execute(CreateDefaultCommand(cmd, commandType, parameters),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4), reader.GetValue<P6>(5), reader.GetValue<P7>(6),
                    reader.GetValue<P8>(7), reader.GetValue<P9>(8), reader.GetValue<P10>(9), reader.GetValue<P11>(10),
                    reader.GetValue<P12>(11), reader.GetValue<P13>(12), reader.GetValue<P14>(13), reader.GetValue<P15>(14),
                    reader.GetValue<P16>(15), reader.GetValue<P17>(16)));

        public static async Task<List<R>> ExecuteAsync<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, R>(string cmd,
            Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, R> func, params IDataParameter[] parameters) =>
            await _ExecuteAsync(await CreateDefaultCommandAsync(cmd, parameters).ConfigureAwait(false),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4), reader.GetValue<P6>(5), reader.GetValue<P7>(6),
                    reader.GetValue<P8>(7), reader.GetValue<P9>(8), reader.GetValue<P10>(9), reader.GetValue<P11>(10),
                    reader.GetValue<P12>(11), reader.GetValue<P13>(12), reader.GetValue<P14>(13), reader.GetValue<P15>(14),
                    reader.GetValue<P16>(15), reader.GetValue<P17>(16))).ConfigureAwait(false);

        public static async Task<List<R>> ExecuteAsync<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, R>(string cmd, CommandType commandType,
            Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, R> func, params IDataParameter[] parameters) =>
            await _ExecuteAsync(await CreateDefaultCommandAsync(cmd, commandType, parameters).ConfigureAwait(false),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4), reader.GetValue<P6>(5), reader.GetValue<P7>(6),
                    reader.GetValue<P8>(7), reader.GetValue<P9>(8), reader.GetValue<P10>(9), reader.GetValue<P11>(10),
                    reader.GetValue<P12>(11), reader.GetValue<P13>(12), reader.GetValue<P14>(13), reader.GetValue<P15>(14),
                    reader.GetValue<P16>(15), reader.GetValue<P17>(16))).ConfigureAwait(false);

        // 18 columns
        public static List<R> Execute<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, R>(string cmd,
            Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, R> func, params IDataParameter[] parameters) =>
            _Execute(CreateDefaultCommand(cmd, parameters),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4), reader.GetValue<P6>(5), reader.GetValue<P7>(6),
                    reader.GetValue<P8>(7), reader.GetValue<P9>(8), reader.GetValue<P10>(9), reader.GetValue<P11>(10),
                    reader.GetValue<P12>(11), reader.GetValue<P13>(12), reader.GetValue<P14>(13), reader.GetValue<P15>(14),
                    reader.GetValue<P16>(15), reader.GetValue<P17>(16), reader.GetValue<P18>(17)));

        public static List<R> Execute<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, R>(string cmd, CommandType commandType,
            Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, R> func, params IDataParameter[] parameters) =>
            _Execute(CreateDefaultCommand(cmd, commandType, parameters),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4), reader.GetValue<P6>(5), reader.GetValue<P7>(6),
                    reader.GetValue<P8>(7), reader.GetValue<P9>(8), reader.GetValue<P10>(9), reader.GetValue<P11>(10),
                    reader.GetValue<P12>(11), reader.GetValue<P13>(12), reader.GetValue<P14>(13), reader.GetValue<P15>(14),
                    reader.GetValue<P16>(15), reader.GetValue<P17>(16), reader.GetValue<P18>(17)));

        public static async Task<List<R>> ExecuteAsync<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, R>(string cmd,
            Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, R> func, params IDataParameter[] parameters) =>
            await _ExecuteAsync(await CreateDefaultCommandAsync(cmd, parameters).ConfigureAwait(false),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4), reader.GetValue<P6>(5), reader.GetValue<P7>(6),
                    reader.GetValue<P8>(7), reader.GetValue<P9>(8), reader.GetValue<P10>(9), reader.GetValue<P11>(10),
                    reader.GetValue<P12>(11), reader.GetValue<P13>(12), reader.GetValue<P14>(13), reader.GetValue<P15>(14),
                    reader.GetValue<P16>(15), reader.GetValue<P17>(16), reader.GetValue<P18>(17))).ConfigureAwait(false);

        public static async Task<List<R>> ExecuteAsync<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, R>(string cmd, CommandType commandType,
            Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, R> func, params IDataParameter[] parameters) =>
            await _ExecuteAsync(await CreateDefaultCommandAsync(cmd, commandType, parameters).ConfigureAwait(false),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4), reader.GetValue<P6>(5), reader.GetValue<P7>(6),
                    reader.GetValue<P8>(7), reader.GetValue<P9>(8), reader.GetValue<P10>(9), reader.GetValue<P11>(10),
                    reader.GetValue<P12>(11), reader.GetValue<P13>(12), reader.GetValue<P14>(13), reader.GetValue<P15>(14),
                    reader.GetValue<P16>(15), reader.GetValue<P17>(16), reader.GetValue<P18>(17))).ConfigureAwait(false);

        // 19 columns
        public static List<R> Execute<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19, R>(string cmd,
            Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19, R> func, params IDataParameter[] parameters) =>
            _Execute(CreateDefaultCommand(cmd, parameters),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4), reader.GetValue<P6>(5), reader.GetValue<P7>(6),
                    reader.GetValue<P8>(7), reader.GetValue<P9>(8), reader.GetValue<P10>(9), reader.GetValue<P11>(10),
                    reader.GetValue<P12>(11), reader.GetValue<P13>(12), reader.GetValue<P14>(13), reader.GetValue<P15>(14),
                    reader.GetValue<P16>(15), reader.GetValue<P17>(16), reader.GetValue<P18>(17), reader.GetValue<P19>(18)));

        public static List<R> Execute<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19, R>(string cmd, CommandType commandType,
            Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19, R> func, params IDataParameter[] parameters) =>
            _Execute(CreateDefaultCommand(cmd, commandType, parameters),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4), reader.GetValue<P6>(5), reader.GetValue<P7>(6),
                    reader.GetValue<P8>(7), reader.GetValue<P9>(8), reader.GetValue<P10>(9), reader.GetValue<P11>(10),
                    reader.GetValue<P12>(11), reader.GetValue<P13>(12), reader.GetValue<P14>(13), reader.GetValue<P15>(14),
                    reader.GetValue<P16>(15), reader.GetValue<P17>(16), reader.GetValue<P18>(17), reader.GetValue<P19>(18)));

        public static async Task<List<R>> ExecuteAsync<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19, R>(string cmd,
            Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19, R> func, params IDataParameter[] parameters) =>
            await _ExecuteAsync(await CreateDefaultCommandAsync(cmd, parameters).ConfigureAwait(false),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4), reader.GetValue<P6>(5), reader.GetValue<P7>(6),
                    reader.GetValue<P8>(7), reader.GetValue<P9>(8), reader.GetValue<P10>(9), reader.GetValue<P11>(10),
                    reader.GetValue<P12>(11), reader.GetValue<P13>(12), reader.GetValue<P14>(13), reader.GetValue<P15>(14),
                    reader.GetValue<P16>(15), reader.GetValue<P17>(16), reader.GetValue<P18>(17), reader.GetValue<P19>(18))).ConfigureAwait(false);

        public static async Task<List<R>> ExecuteAsync<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19, R>(string cmd, CommandType commandType,
            Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19, R> func, params IDataParameter[] parameters) =>
            await _ExecuteAsync(await CreateDefaultCommandAsync(cmd, commandType, parameters).ConfigureAwait(false),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4), reader.GetValue<P6>(5), reader.GetValue<P7>(6),
                    reader.GetValue<P8>(7), reader.GetValue<P9>(8), reader.GetValue<P10>(9), reader.GetValue<P11>(10),
                    reader.GetValue<P12>(11), reader.GetValue<P13>(12), reader.GetValue<P14>(13), reader.GetValue<P15>(14),
                    reader.GetValue<P16>(15), reader.GetValue<P17>(16), reader.GetValue<P18>(17), reader.GetValue<P19>(18))).ConfigureAwait(false);

        // 20 columns
        public static List<R> Execute<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19, P20, R>(string cmd,
            Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19, P20, R> func, params IDataParameter[] parameters) =>
            _Execute(CreateDefaultCommand(cmd, parameters),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4), reader.GetValue<P6>(5), reader.GetValue<P7>(6),
                    reader.GetValue<P8>(7), reader.GetValue<P9>(8), reader.GetValue<P10>(9), reader.GetValue<P11>(10),
                    reader.GetValue<P12>(11), reader.GetValue<P13>(12), reader.GetValue<P14>(13), reader.GetValue<P15>(14),
                    reader.GetValue<P16>(15), reader.GetValue<P17>(16), reader.GetValue<P18>(17), reader.GetValue<P19>(18),
                    reader.GetValue<P20>(19)));

        public static List<R> Execute<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19, P20, R>(string cmd, CommandType commandType,
            Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19, P20, R> func, params IDataParameter[] parameters) =>
            _Execute(CreateDefaultCommand(cmd, commandType, parameters),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4), reader.GetValue<P6>(5), reader.GetValue<P7>(6),
                    reader.GetValue<P8>(7), reader.GetValue<P9>(8), reader.GetValue<P10>(9), reader.GetValue<P11>(10),
                    reader.GetValue<P12>(11), reader.GetValue<P13>(12), reader.GetValue<P14>(13), reader.GetValue<P15>(14),
                    reader.GetValue<P16>(15), reader.GetValue<P17>(16), reader.GetValue<P18>(17), reader.GetValue<P19>(18),
                    reader.GetValue<P20>(19)));

        public static async Task<List<R>> ExecuteAsync<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19, P20, R>(string cmd,
            Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19, P20, R> func, params IDataParameter[] parameters) =>
            await _ExecuteAsync(await CreateDefaultCommandAsync(cmd, parameters).ConfigureAwait(false),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4), reader.GetValue<P6>(5), reader.GetValue<P7>(6),
                    reader.GetValue<P8>(7), reader.GetValue<P9>(8), reader.GetValue<P10>(9), reader.GetValue<P11>(10),
                    reader.GetValue<P12>(11), reader.GetValue<P13>(12), reader.GetValue<P14>(13), reader.GetValue<P15>(14),
                    reader.GetValue<P16>(15), reader.GetValue<P17>(16), reader.GetValue<P18>(17), reader.GetValue<P19>(18),
                    reader.GetValue<P20>(19))).ConfigureAwait(false);

        public static async Task<List<R>> ExecuteAsync<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19, P20, R>(string cmd, CommandType commandType,
            Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19, P20, R> func, params IDataParameter[] parameters) =>
            await _ExecuteAsync(await CreateDefaultCommandAsync(cmd, commandType, parameters).ConfigureAwait(false),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4), reader.GetValue<P6>(5), reader.GetValue<P7>(6),
                    reader.GetValue<P8>(7), reader.GetValue<P9>(8), reader.GetValue<P10>(9), reader.GetValue<P11>(10),
                    reader.GetValue<P12>(11), reader.GetValue<P13>(12), reader.GetValue<P14>(13), reader.GetValue<P15>(14),
                    reader.GetValue<P16>(15), reader.GetValue<P17>(16), reader.GetValue<P18>(17), reader.GetValue<P19>(18),
                    reader.GetValue<P20>(19))).ConfigureAwait(false);

        #endregion excute command and return sqlreader value to callback function, upto 20 columns

        public static Action Observer(string cmd, Action<SqlNotificationEventArgs> func)
        {
            var connectionString = ConfigurationManager.AppSettings["dsn"];
            SqlDependency.Start(connectionString);
            var sqlConnection = new SqlConnection(connectionString);
            var sqlCommand = new SqlCommand(cmd, sqlConnection);
            void OnDependencyChange(object sender, SqlNotificationEventArgs args)
            {
                // Remove the handler, since it is only good
                // for a single notification.
                var d2 = (SqlDependency)sender;
                d2.OnChange -= OnDependencyChange;

                func(args);

                sqlCommand.Notification = null;
                var d = new SqlDependency(sqlCommand);
                d.OnChange += OnDependencyChange;
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();
                using (sqlCommand.ExecuteReader(CommandBehavior.CloseConnection)) { }
            }

            var dependency = new SqlDependency(sqlCommand);
            dependency.OnChange += OnDependencyChange;
            sqlConnection.Open();
            using (sqlCommand.ExecuteReader(CommandBehavior.CloseConnection)) { }

            return () => dependency.OnChange -= OnDependencyChange;
        }

        public static SqlQueryResultData QuerySqlQueryResultData(string sql, CommandType commandType, params IDataParameter[] parameters)
        {
            SqlQueryResultData returnItem = new SqlQueryResultData();

            using (var dataReader = SqlQuery.ExecuteCommand(sql, commandType, parameters))
            {
                returnItem.heading = Enumerable.Range(0, dataReader.FieldCount).Select(dataReader.GetName)
                    .Where(s => !s.StartsWith("_")).ToList();
                while (dataReader.Read())
                {
                    returnItem.data.Add(Enumerable.Range(0, dataReader.FieldCount)
                        .Where(i => !dataReader.GetName(i).StartsWith("_"))
                        .Select(i => dataReader.GetValue(i).ConvertObject(dataReader.GetFieldType(i))).ToArray());
                }
            }
            return returnItem;
        }

        public static async Task<SqlQueryResultData> QuerySqlQueryResultDataAsync(string sql, CommandType commandType, params IDataParameter[] parameters)
        {
            SqlQueryResultData returnItem = new SqlQueryResultData();

            using (var dataReader = await SqlQuery.ExecuteCommandAsync(sql, commandType, parameters).ConfigureAwait(false))
            {
                returnItem.heading = Enumerable.Range(0, dataReader.FieldCount).Select(dataReader.GetName)
                    .Where(s => !s.StartsWith("_")).ToList();
                while (await dataReader.ReadAsync().ConfigureAwait(false))
                {
                    returnItem.data.Add(Enumerable.Range(0, dataReader.FieldCount)
                        .Where(i => !dataReader.GetName(i).StartsWith("_"))
                        .Select(i => dataReader.GetValue(i).ConvertObject(dataReader.GetFieldType(i))).ToArray());
                }
            }
            return returnItem;
        }

        public static IEnumerable<T> Query<T>(string sql, CommandType commandType, params IDataParameter[] parameters)
        {
            var result = new List<T>();
            var pd = ObjectSchema.ForType(typeof(T));

            var dataReader = SqlQuery.ExecuteCommand(sql, commandType, parameters);
            var readerObject = dataReader as DbDataReader;
            var factory = pd.GetFactory();

            using (dataReader)
            {
                while (true)
                {
                    T poco;
                    try
                    {
                        if (readerObject != null)
                        {
                            if (!readerObject.Read())
                                return result;
                        }
                        else
                        {
                            if (!dataReader.Read())
                                return result;
                        }

                        poco = (T)factory(dataReader);
                        if (poco != null)
                            result.Add(poco);
                    }
                    catch (Exception e)
                    {
                        throw;
                    }
                }
            }
        }

        public static async Task<IEnumerable<T>> QueryAsync<T>(string sql, CommandType commandType, params IDataParameter[] parameters)
        {
            var result = new List<T>();
            var pd = ObjectSchema.ForType(typeof(T));

            var dataReader = await SqlQuery.ExecuteCommandAsync(sql, commandType, parameters).ConfigureAwait(false);
            var readerAsync = dataReader as DbDataReader;
            var factory = pd.GetFactory();

            using (dataReader)
            {
                while (true)
                {
                    T poco;
                    try
                    {
                        if (readerAsync != null)
                        {
                            if (!await readerAsync.ReadAsync().ConfigureAwait(false))
                                return result;
                        }
                        else
                        {
                            if (!await dataReader.ReadAsync().ConfigureAwait(false))
                                return result;
                        }

                        poco = (T)factory(dataReader);
                        if (poco != null)
                            result.Add(poco);
                    }
                    catch (Exception e)
                    {
                        throw;
                    }
                }
            }
        }

        public static IEnumerable<T> QueryJson<T>(string sql, CommandType commandType, params IDataParameter[] parameters)
        {
            var result = new List<T>();
            var jsonResult = new StringBuilder();
            if (!QueryJson(jsonResult, sql, commandType, parameters)) return new List<T>();
            if (jsonResult.Length <= 0) return result;

            var setting = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Include
            };

            try
            {
                result = JsonConvert.DeserializeObject<List<T>>(jsonResult.ToString(), setting);
            }
            catch (Exception ex)
            {
                return new List<T>();
            }

            return result.ToList();
        }

        public static async Task<IEnumerable<T>> QueryJsonAsync<T>(string sql, CommandType commandType, params IDataParameter[] parameters)
        {
            var result = new List<T>();
            var jsonResult = new StringBuilder();
            if (!(await QueryJsonAsync(jsonResult, sql, commandType, parameters))) return new List<T>();
            if (jsonResult.Length <= 0) return result;

            var setting = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Include
            };

            try
            {
                result = JsonConvert.DeserializeObject<List<T>>(jsonResult.ToString(), setting);
            }
            catch (Exception ex)
            {
                return new List<T>();
            }

            return result.ToList();
        }

        public static bool QueryJson(StringBuilder jsonResult, string sql, CommandType commandType, params IDataParameter[] parameters)
        {
            using (var dataReader = SqlQuery.ExecuteCommand(sql, commandType, parameters))
            {
                if (dataReader is null) return false;

                while (dataReader.Read())
                {
                    jsonResult.Append(dataReader.GetValue(0).ToString());
                }
            }
            return true;
        }

        public static async Task<bool> QueryJsonAsync(StringBuilder jsonResult, string sql, CommandType commandType, params IDataParameter[] parameters)
        {
            using (var dataReader = await SqlQuery.ExecuteCommandAsync(sql, commandType, parameters).ConfigureAwait(false))
            {
                if (dataReader is null) return false;

                while (await dataReader.ReadAsync().ConfigureAwait(false))
                {
                    jsonResult.Append(dataReader.GetValue(0).ToString());
                }
            }
            return true;
        }

        public static DataTable ToDataTable<T>(this IEnumerable<T> items)
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("item", typeof(T)));
            foreach (var val in items.ToList())
                table.Rows.Add(val);
            return table;
            return items.ToDataTableFromObject();
        }

        private static DataTable ToDataTableFromObject<T>(this IEnumerable<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            var items = data.ToList();
            var count = items.Count;
            for (var i = 0; i < count; i++)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                {
                    var value = prop.GetValue(items[i]);
                    if (value != null) row[prop.Name] = value;
                }
                table.Rows.Add(row);
            };
            return table;
        }

        public static DataTable ToEnumDataTable(this Type enumType)
        {
            if (!enumType.IsEnum)
                return null;
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("num", typeof(int)));
            table.Columns.Add(new DataColumn("text", typeof(string)));
            foreach (var item in Enum.GetValues(enumType))
            {
                FieldInfo fi = enumType.GetField(item.ToString());
                var attribute = fi.GetCustomAttributes(typeof(DescriptionAttribute), true).FirstOrDefault();
                var text = attribute == null ? item.ToString() : ((DescriptionAttribute)attribute).Description;
                table.Rows.Add((int)item, text);
            }
            return table;
        }

        public static SqlParameter AddListParameters<T>(this SqlParameter sqlParam, string typeName, T value)
        {
            sqlParam.TypeName = typeName;
            sqlParam.Value = value;
            return sqlParam;
        }

        public static SqlParameter AddListParameters<T>(this SqlParameter sqlParam, IEnumerable<T> data)
        {
            var t = typeof(T);
            var typeName = "dbo.StringListTableType";
            if (t == typeof(int) || t == typeof(int?))
                typeName = "dbo.IntListTableType";
            else if (t == typeof(long) || t == typeof(long?))
                typeName = "dbo.LongListTableType";
            else if (t == typeof(decimal) || t == typeof(decimal?))
                typeName = "dbo.DecimalListTableType";
            else if (t == typeof(DateTime) || t == typeof(DateTime?))
                typeName = "dbo.DateTimeListTableType";

            sqlParam.TypeName = typeName;
            sqlParam.Value = data.ToDataTable();
            return sqlParam;
        }

        public static SqlParameter AddEnumListParameters<T>(this SqlParameter sqlParam)
        {
            var typeName = "dbo.EnumListTableType";
            var t = typeof(T);

            sqlParam.TypeName = typeName;
            sqlParam.Value = t.ToEnumDataTable();
            return sqlParam;
        }

        public static SqlParameter ToParameter<T>(this IEnumerable<T> values, string name)
        {
            SqlParameter param = new SqlParameter(name.ToParameterName(), SqlDbType.Structured);
            param.AddListParameters<T>(values);
            return param;
        }
        public static SqlParameter ToEnumParameter<T>(this string name) where T: Enum
        {
            SqlParameter param = new SqlParameter(name.ToParameterName(), SqlDbType.Structured);
            param.AddEnumListParameters<T>();
            return param;
        }

        public static string ToParameterName(this string name) => name.First().ToString().Equals("@") ? name : $"@{name}";

        public static IDataParameter ToParameter<T>(this T value, string name, bool isNvarchar = false)
        {
            var t = typeof(T);
            if (t == typeof(string))
                return new SqlParameter(name.ToParameterName(), isNvarchar ? SqlDbType.NVarChar : SqlDbType.VarChar, value.ToString().Length)
                    {
                        Value = value
                    };
            
            if (t == typeof(int))
                return new SqlParameter(name.ToParameterName(), SqlDbType.Int, 4)
                    { 
                        Value = value
                    };

            if (t == typeof(long))
                return new SqlParameter(name.ToParameterName(), SqlDbType.BigInt)
                {
                    Value = value
                };

            if (t == typeof(bool?) || t == typeof(bool))
                return new SqlParameter(name.ToParameterName(), SqlDbType.TinyInt)
                {
                    Value = value.ToBool() ? 1 : 0 
                };

            if (t == typeof(float))
                return new SqlParameter(name.ToParameterName(), DbType.Single)
                {
                    Value = value
                };

            if (t == typeof(double))
                return new SqlParameter(name.ToParameterName(), DbType.Double)
                {
                    Value = value
                };

            if (t == typeof(decimal))
                return new SqlParameter(name.ToParameterName(), DbType.Double)
                {
                    Value = value,
                    Precision = 24,
                    Scale = 6
                };

            if (t == typeof(byte[]))
                return new SqlParameter(name.ToParameterName(), SqlDbType.Image)
                {
                    Value = value,
                    Precision = 24,
                    Scale = 6
                };

            if (t == typeof(DateTime))
                return new SqlParameter(name.ToParameterName(), SqlDbType.DateTime)
                {
                    Value = (value.ToDateTime() < _SqlMinDateTime) ? _SqlMinDateTime : value.ToDateTime()
                };

            return null;
        }

        public static IDataParameter ToParameter(this string value, string name, bool isNvarchar = false)
        {
            SqlParameter param = new SqlParameter(name.ToParameterName(), isNvarchar ? SqlDbType.NVarChar : SqlDbType.VarChar, value.Length);
            param.Value = value;
            return param;
        }

        public static IDataParameter ToParameter(this int value, string name)
        {
            SqlParameter param = new SqlParameter(name.ToParameterName(), SqlDbType.Int, 4);
            param.Value = value;
            return param;
        }

        public static IDataParameter ToParameter(this long value, string name)
        {
            SqlParameter param = new SqlParameter(name.ToParameterName(), SqlDbType.BigInt);
            param.Value = value;
            return param;
        }

        public static IDataParameter ToParameter(this bool? value, string name)
        {
            SqlParameter param = new SqlParameter(name.ToParameterName(), SqlDbType.TinyInt);
            param.Value = value.HasValue && value.Value ? 1 : 0;
            return param;
        }

        public static IDataParameter ToParameter(this float value, string name)
        {
            SqlParameter param = new SqlParameter(name.ToParameterName(), DbType.Single);
            param.Value = value;
            return param;
        }

        public static IDataParameter ToParameter(this double? value, string name)
        {
            SqlParameter param = new SqlParameter(name.ToParameterName(), DbType.Double);
            if (!value.HasValue)
                param.Value = null;
            else if (value.Equals(double.NaN))
                param.Value = 0.0;
            else
                param.Value = value;
            return param;
        }

        public static IDataParameter ToParameter(this decimal? value, string name, byte precision = 24, byte scale = 6)
        {
            SqlParameter param = new SqlParameter(name.ToParameterName(), DbType.Double);
            if (!value.HasValue)
                param.Value = null;
            else
                param.Value = value;
            param.Precision = precision;
            param.Scale = scale;
            return param;
        }

        public static IDataParameter ToParameter(this decimal value, string name, byte precision = 24, byte scale = 6)
        {
            SqlParameter param = new SqlParameter(name.ToParameterName(), DbType.Double);
            param.Value = value;
            param.Precision = precision;
            param.Scale = scale;
            return param;
        }

        public static IDataParameter ToParameter(this byte[] value, string name)
        {
            SqlParameter param = new SqlParameter(name.ToParameterName(), SqlDbType.Image);
            param.Value = value;
            return param;
        }

        public static IDataParameter ToParameter(this Guid? value, string name)
        {
            SqlParameter param = new SqlParameter(name.ToParameterName(), SqlDbType.UniqueIdentifier);
            if (!value.HasValue || Guid.Empty == value)
                param.Value = DBNull.Value;
            else
                param.Value = value;
            return param;
        }

        public static IDataParameter ToParameter(DateTime? value, string name)
        {
            SqlParameter param = new SqlParameter(name.ToParameterName(), SqlDbType.DateTime);
            if (value < _SqlMinDateTime)
                value = _SqlMinDateTime;
            if (value.HasValue)
                param.Value = value.Value;
            return param;
        }

        public static IDataParameter ToParameter(DateTime value, string name)
        {
            SqlParameter param = new SqlParameter(name.ToParameterName(), SqlDbType.DateTime);
            if (value < _SqlMinDateTime)
                value = _SqlMinDateTime;
            param.Value = value;
            return param;
        }

        public static string ToSqlFieldName(this string name, string prefix, bool isStringType = true)
        {
            if (string.IsNullOrEmpty(name)) 
                return string.Empty;

            StringBuilder sb = new StringBuilder();
            sb.Append("COALESCE(");

            // name already has prefix
            if (name.IndexOf(".") > 0)
            {
                sb.Append(name.TrimEnd().Replace(".", ".["));
                sb.Append("]");
            }
            else
            {
                if (!string.IsNullOrEmpty(prefix))
                {
                    sb.Append(prefix.TrimEnd());
                    sb.Append(".");
                }
                sb.Append("[");
                sb.Append(name.TrimEnd());
                sb.Append("]");
            }

            sb.Append(",");
            sb.Append((isStringType) ? "''" : "0");

            sb.Append(")");
            return sb.ToString();
        }
    }

    public static class ReaderExtensions
    {
        public static T GetValue<T>(this IDataReader dataReader, int i)
        {
            var value = dataReader.GetValue(i);
            if (value is T)
                return (T)value;

            var type = typeof(T);
            var isNullable = type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
            if (isNullable)
            {
                if (dataReader.IsDBNull(i))
                    return (T)(object)null;

                type = Nullable.GetUnderlyingType(type);
            }

            if (type == typeof(Guid))
            {
                var result = Guid.Empty;
                if (value == null)
                    return (T)(object)result;
                Guid.TryParse(value.ToString(), out result);
                return (T)(object)result;
            }

            return value.ConvertObject<T>(type);
        }
    }
}