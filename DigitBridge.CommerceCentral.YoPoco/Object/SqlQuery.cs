using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using DigitBridge.Base.Utility;

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
        public static SerializationBinder SerializationBinder { get; set; }

        static SqlQuery()
        {
            DbUtility.OnTransactionEnd(OnTransactionEnd);
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
            using var dbCommand = DbUtility.CreateCommand(cmd, commandType, parameters);
            dbCommand.ExecuteNonQuery();
        }

        public static async Task ExecuteNonQueryAsync(string cmd, params IDataParameter[] parameters) => 
            await ExecuteNonQueryAsync(cmd, CommandType.Text, parameters);

        public static async Task ExecuteNonQueryAsync(string cmd, CommandType commandType, params IDataParameter[] parameters)
        {
            using var dbCommand = DbUtility.CreateCommand(cmd, commandType, parameters);
            await ((SqlCommand)dbCommand).ExecuteNonQueryAsync();
        }

        public static IDataReader ExecuteCommand(string commandText, params IDataParameter[] parameters) =>
            ExecuteCommand(commandText, CommandType.Text, parameters);

        public static IDataReader ExecuteCommand(string commandText, CommandType commandType, params IDataParameter[] parameters)
        {
            using IDbCommand dbCommand = DbUtility.CreateCommand(commandText, commandType, parameters);
            return dbCommand.ExecuteReader();
        }

        public static async Task<SqlDataReader> ExecuteCommandAsync(string commandText, params IDataParameter[] parameters) =>
            await ExecuteCommandAsync(commandText, CommandType.Text, parameters);

        public static async Task<SqlDataReader> ExecuteCommandAsync(string commandText, CommandType commandType, params IDataParameter[] parameters)
        {
            using IDbCommand dbCommand = DbUtility.CreateCommand(commandText, commandType, parameters);
            return await ((SqlCommand)dbCommand).ExecuteReaderAsync();
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
                do { while (await dataReader.ReadAsync()) { results.Add(f(dataReader)); } } while (await dataReader.NextResultAsync());
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
            return await ExecuteCommandAsync(cmd, commandType, parameters);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static async Task<SqlDataReader> CreateDefaultCommandAsync(string cmd, params IDataParameter[] parameters)
        {
            return await ExecuteCommandAsync(cmd, CommandType.Text, parameters);
        }

        // 1 column
        public static List<R> Execute<P1, R>(string cmd, Func<P1, R> func, params IDataParameter[] parameters) => 
            _Execute(CreateDefaultCommand(cmd, parameters), reader => func(reader.GetValue<P1>(0)));

        public static List<R> Execute<P1, R>(string cmd, CommandType commandType, Func<P1, R> func, params IDataParameter[] parameters) =>
            _Execute(CreateDefaultCommand(cmd, commandType, parameters), reader => func(reader.GetValue<P1>(0)));

        public static async Task<List<R>> ExecuteAsync<P1, R>(string cmd, Func<P1, R> func, params IDataParameter[] parameters) => 
            await _ExecuteAsync(await CreateDefaultCommandAsync(cmd, parameters), reader => func(reader.GetValue<P1>(0)));

        public static async Task<List<R>> ExecuteAsync<P1, R>(string cmd, CommandType commandType, Func<P1, R> func, params IDataParameter[] parameters) => 
            await _ExecuteAsync(await CreateDefaultCommandAsync(cmd, commandType, parameters), reader => func(reader.GetValue<P1>(0)));

        // 2 columns
        public static List<R> Execute<P1, P2, R>(string cmd, Func<P1, P2, R> func, params IDataParameter[] parameters) =>
            _Execute(CreateDefaultCommand(cmd, parameters), reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1)));

        public static List<R> Execute<P1, P2, R>(string cmd, Func<P1, P2, R> func, CommandType commandType, params IDataParameter[] parameters) => 
            _Execute(CreateDefaultCommand(cmd, commandType, parameters), reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1)));

        public static async Task<List<R>> ExecuteAsync<P1, P2, R>(string cmd, 
            Func<P1, P2, R> func, params IDataParameter[] parameters) =>
            await _ExecuteAsync(await CreateDefaultCommandAsync(cmd, parameters),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1)));

        public static async Task<List<R>> ExecuteAsync<P1, P2, R>(string cmd, CommandType commandType, 
            Func<P1, P2, R> func, params IDataParameter[] parameters) => 
            await _ExecuteAsync(await CreateDefaultCommandAsync(cmd, commandType, parameters), 
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1)));

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
            await CreateDefaultCommandAsync(cmd, parameters),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2)));

        public static async Task<List<R>> ExecuteAsync<P1, P2, P3, R>(string cmd, CommandType commandType,
            Func<P1, P2, P3, R> func, params IDataParameter[] parameters) => await _ExecuteAsync(
            await CreateDefaultCommandAsync(cmd, commandType, parameters),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2)));

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
            await _ExecuteAsync(await CreateDefaultCommandAsync(cmd, parameters),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3)));

        public static async Task<List<R>> ExecuteAsync<P1, P2, P3, P4, R>(string cmd, CommandType commandType,
            Func<P1, P2, P3, P4, R> func, params IDataParameter[] parameters) => await _ExecuteAsync(
            await CreateDefaultCommandAsync(cmd, commandType, parameters),
            reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                reader.GetValue<P4>(3)));

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
            await _ExecuteAsync(await CreateDefaultCommandAsync(cmd, parameters),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4)));

        public static async Task<List<R>> ExecuteAsync<P1, P2, P3, P4, P5, R>(string cmd, CommandType commandType,
            Func<P1, P2, P3, P4, P5, R> func, params IDataParameter[] parameters) => 
            await _ExecuteAsync(await CreateDefaultCommandAsync(cmd, commandType, parameters),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4)));

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
            await _ExecuteAsync(await CreateDefaultCommandAsync(cmd, parameters),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4), reader.GetValue<P6>(5)));

        public static async Task<List<R>> ExecuteAsync<P1, P2, P3, P4, P5, P6, R>(string cmd, CommandType commandType,
            Func<P1, P2, P3, P4, P5, P6, R> func, params IDataParameter[] parameters) => 
            await _ExecuteAsync(await CreateDefaultCommandAsync(cmd, commandType, parameters),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4), reader.GetValue<P6>(5)));

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
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4), reader.GetValue<P6>(5), reader.GetValue<P7>(6)));

        public static async Task<List<R>> ExecuteAsync<P1, P2, P3, P4, P5, P6, P7, R>(string cmd, CommandType commandType, 
            Func<P1, P2, P3, P4, P5, P6, P7, R> func, params IDataParameter[] parameters) =>
            await _ExecuteAsync(CreateDefaultCommand(cmd, commandType, parameters),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4), reader.GetValue<P6>(5), reader.GetValue<P7>(6)));

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
            await _ExecuteAsync(await CreateDefaultCommandAsync(cmd, parameters),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4), reader.GetValue<P6>(5), reader.GetValue<P7>(6),
                    reader.GetValue<P8>(7)));

        public static async Task<List<R>> ExecuteAsync<P1, P2, P3, P4, P5, P6, P7, P8, R>(string cmd, CommandType commandType, 
            Func<P1, P2, P3, P4, P5, P6, P7, P8, R> func, params IDataParameter[] parameters) => 
            await _ExecuteAsync(await CreateDefaultCommandAsync(cmd, commandType, parameters),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4), reader.GetValue<P6>(5), reader.GetValue<P7>(6),
                    reader.GetValue<P8>(7)));

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
            await _ExecuteAsync(await CreateDefaultCommandAsync(cmd, parameters),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4), reader.GetValue<P6>(5), reader.GetValue<P7>(6),
                    reader.GetValue<P8>(7), reader.GetValue<P9>(8)));

        public static async Task<List<R>> ExecuteAsync<P1, P2, P3, P4, P5, P6, P7, P8, P9, R>(string cmd, CommandType commandType, 
            Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, R> func, params IDataParameter[] parameters) => 
            await _ExecuteAsync(await CreateDefaultCommandAsync(cmd, commandType, parameters),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4), reader.GetValue<P6>(5), reader.GetValue<P7>(6),
                    reader.GetValue<P8>(7), reader.GetValue<P9>(8)));

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
            await _ExecuteAsync(await CreateDefaultCommandAsync(cmd, parameters),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4), reader.GetValue<P6>(5), reader.GetValue<P7>(6),
                    reader.GetValue<P8>(7), reader.GetValue<P9>(8), reader.GetValue<P10>(9)));

        public static async Task<List<R>> ExecuteAsync<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, R>(string cmd, CommandType commandType, 
            Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, R> func, params IDataParameter[] parameters) => 
            await _ExecuteAsync(await CreateDefaultCommandAsync(cmd, commandType, parameters),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4), reader.GetValue<P6>(5), reader.GetValue<P7>(6),
                    reader.GetValue<P8>(7), reader.GetValue<P9>(8), reader.GetValue<P10>(9)));

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
            await _ExecuteAsync(await CreateDefaultCommandAsync(cmd, parameters),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4), reader.GetValue<P6>(5), reader.GetValue<P7>(6),
                    reader.GetValue<P8>(7), reader.GetValue<P9>(8), reader.GetValue<P10>(9), reader.GetValue<P11>(10)));

        public static async Task<List<R>> ExecuteAsync<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, R>(string cmd, CommandType commandType, 
            Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, R> func, params IDataParameter[] parameters) => 
            await _ExecuteAsync(await CreateDefaultCommandAsync(cmd, commandType, parameters),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4), reader.GetValue<P6>(5), reader.GetValue<P7>(6),
                    reader.GetValue<P8>(7), reader.GetValue<P9>(8), reader.GetValue<P10>(9), reader.GetValue<P11>(10)));

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
                    reader.GetValue<P12>(11)));

        public static async Task<List<R>> ExecuteAsync<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, R>(string cmd, CommandType commandType, 
            Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, R> func, params IDataParameter[] parameters) => 
            await _ExecuteAsync(CreateDefaultCommand(cmd, commandType, parameters),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4), reader.GetValue<P6>(5), reader.GetValue<P7>(6),
                    reader.GetValue<P8>(7), reader.GetValue<P9>(8), reader.GetValue<P10>(9), reader.GetValue<P11>(10),
                    reader.GetValue<P12>(11)));

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
            await _ExecuteAsync(await CreateDefaultCommandAsync(cmd, parameters),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4), reader.GetValue<P6>(5), reader.GetValue<P7>(6),
                    reader.GetValue<P8>(7), reader.GetValue<P9>(8), reader.GetValue<P10>(9), reader.GetValue<P11>(10),
                    reader.GetValue<P12>(11), reader.GetValue<P13>(12)));

        public static async Task<List<R>> ExecuteAsync<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, R>(string cmd, CommandType commandType,
            Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, R> func, params IDataParameter[] parameters) => 
            await _ExecuteAsync(await CreateDefaultCommandAsync(cmd, commandType, parameters),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4), reader.GetValue<P6>(5), reader.GetValue<P7>(6),
                    reader.GetValue<P8>(7), reader.GetValue<P9>(8), reader.GetValue<P10>(9), reader.GetValue<P11>(10),
                    reader.GetValue<P12>(11), reader.GetValue<P13>(12)));

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
            await _ExecuteAsync(await CreateDefaultCommandAsync(cmd, parameters),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4), reader.GetValue<P6>(5), reader.GetValue<P7>(6),
                    reader.GetValue<P8>(7), reader.GetValue<P9>(8), reader.GetValue<P10>(9), reader.GetValue<P11>(10),
                    reader.GetValue<P12>(11), reader.GetValue<P13>(12), reader.GetValue<P14>(13)));

        public static async Task<List<R>> ExecuteAsync<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, R>(string cmd, CommandType commandType,
            Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, R> func, params IDataParameter[] parameters) =>
            await _ExecuteAsync(await CreateDefaultCommandAsync(cmd, commandType, parameters),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4), reader.GetValue<P6>(5), reader.GetValue<P7>(6),
                    reader.GetValue<P8>(7), reader.GetValue<P9>(8), reader.GetValue<P10>(9), reader.GetValue<P11>(10),
                    reader.GetValue<P12>(11), reader.GetValue<P13>(12), reader.GetValue<P14>(13)));

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
            await _ExecuteAsync(await CreateDefaultCommandAsync(cmd, parameters),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4), reader.GetValue<P6>(5), reader.GetValue<P7>(6),
                    reader.GetValue<P8>(7), reader.GetValue<P9>(8), reader.GetValue<P10>(9), reader.GetValue<P11>(10),
                    reader.GetValue<P12>(11), reader.GetValue<P13>(12), reader.GetValue<P14>(13),
                    reader.GetValue<P15>(14)));

        public static async Task<List<R>> ExecuteAsync<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, R>(string cmd, CommandType commandType, 
            Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, R> func, params IDataParameter[] parameters) => 
            await _ExecuteAsync(await CreateDefaultCommandAsync(cmd, commandType, parameters),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4), reader.GetValue<P6>(5), reader.GetValue<P7>(6),
                    reader.GetValue<P8>(7), reader.GetValue<P9>(8), reader.GetValue<P10>(9), reader.GetValue<P11>(10),
                    reader.GetValue<P12>(11), reader.GetValue<P13>(12), reader.GetValue<P14>(13),
                    reader.GetValue<P15>(14)));

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
            await _ExecuteAsync(await CreateDefaultCommandAsync(cmd, parameters),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4), reader.GetValue<P6>(5), reader.GetValue<P7>(6),
                    reader.GetValue<P8>(7), reader.GetValue<P9>(8), reader.GetValue<P10>(9), reader.GetValue<P11>(10),
                    reader.GetValue<P12>(11), reader.GetValue<P13>(12), reader.GetValue<P14>(13), reader.GetValue<P15>(14),
                    reader.GetValue<P16>(15)));

        public static async Task<List<R>> ExecuteAsync<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, R>(string cmd, CommandType commandType,
            Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, R> func, params IDataParameter[] parameters) => 
            await _ExecuteAsync(await CreateDefaultCommandAsync(cmd, commandType, parameters),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4), reader.GetValue<P6>(5), reader.GetValue<P7>(6),
                    reader.GetValue<P8>(7), reader.GetValue<P9>(8), reader.GetValue<P10>(9), reader.GetValue<P11>(10),
                    reader.GetValue<P12>(11), reader.GetValue<P13>(12), reader.GetValue<P14>(13), reader.GetValue<P15>(14),
                    reader.GetValue<P16>(15)));

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
            await _ExecuteAsync(await CreateDefaultCommandAsync(cmd, parameters),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4), reader.GetValue<P6>(5), reader.GetValue<P7>(6),
                    reader.GetValue<P8>(7), reader.GetValue<P9>(8), reader.GetValue<P10>(9), reader.GetValue<P11>(10),
                    reader.GetValue<P12>(11), reader.GetValue<P13>(12), reader.GetValue<P14>(13), reader.GetValue<P15>(14),
                    reader.GetValue<P16>(15), reader.GetValue<P17>(16)));

        public static async Task<List<R>> ExecuteAsync<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, R>(string cmd, CommandType commandType,
            Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, R> func, params IDataParameter[] parameters) => 
            await _ExecuteAsync(await CreateDefaultCommandAsync(cmd, commandType, parameters),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4), reader.GetValue<P6>(5), reader.GetValue<P7>(6),
                    reader.GetValue<P8>(7), reader.GetValue<P9>(8), reader.GetValue<P10>(9), reader.GetValue<P11>(10),
                    reader.GetValue<P12>(11), reader.GetValue<P13>(12), reader.GetValue<P14>(13), reader.GetValue<P15>(14),
                    reader.GetValue<P16>(15), reader.GetValue<P17>(16)));

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
            await _ExecuteAsync(await CreateDefaultCommandAsync(cmd, parameters),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4), reader.GetValue<P6>(5), reader.GetValue<P7>(6),
                    reader.GetValue<P8>(7), reader.GetValue<P9>(8), reader.GetValue<P10>(9), reader.GetValue<P11>(10),
                    reader.GetValue<P12>(11), reader.GetValue<P13>(12), reader.GetValue<P14>(13), reader.GetValue<P15>(14),
                    reader.GetValue<P16>(15), reader.GetValue<P17>(16), reader.GetValue<P18>(17)));

        public static async Task<List<R>> ExecuteAsync<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, R>(string cmd, CommandType commandType,
            Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, R> func, params IDataParameter[] parameters) => 
            await _ExecuteAsync(await CreateDefaultCommandAsync(cmd, commandType, parameters),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4), reader.GetValue<P6>(5), reader.GetValue<P7>(6),
                    reader.GetValue<P8>(7), reader.GetValue<P9>(8), reader.GetValue<P10>(9), reader.GetValue<P11>(10),
                    reader.GetValue<P12>(11), reader.GetValue<P13>(12), reader.GetValue<P14>(13), reader.GetValue<P15>(14),
                    reader.GetValue<P16>(15), reader.GetValue<P17>(16), reader.GetValue<P18>(17)));

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
            await _ExecuteAsync(await CreateDefaultCommandAsync(cmd, parameters),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4), reader.GetValue<P6>(5), reader.GetValue<P7>(6),
                    reader.GetValue<P8>(7), reader.GetValue<P9>(8), reader.GetValue<P10>(9), reader.GetValue<P11>(10),
                    reader.GetValue<P12>(11), reader.GetValue<P13>(12), reader.GetValue<P14>(13), reader.GetValue<P15>(14),
                    reader.GetValue<P16>(15), reader.GetValue<P17>(16), reader.GetValue<P18>(17), reader.GetValue<P19>(18)));

        public static async Task<List<R>> ExecuteAsync<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19, R>(string cmd, CommandType commandType,
            Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19, R> func, params IDataParameter[] parameters) =>
            await _ExecuteAsync(await CreateDefaultCommandAsync(cmd, commandType, parameters),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4), reader.GetValue<P6>(5), reader.GetValue<P7>(6),
                    reader.GetValue<P8>(7), reader.GetValue<P9>(8), reader.GetValue<P10>(9), reader.GetValue<P11>(10),
                    reader.GetValue<P12>(11), reader.GetValue<P13>(12), reader.GetValue<P14>(13), reader.GetValue<P15>(14),
                    reader.GetValue<P16>(15), reader.GetValue<P17>(16), reader.GetValue<P18>(17), reader.GetValue<P19>(18)));

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
            await _ExecuteAsync(await CreateDefaultCommandAsync(cmd, parameters),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4), reader.GetValue<P6>(5), reader.GetValue<P7>(6),
                    reader.GetValue<P8>(7), reader.GetValue<P9>(8), reader.GetValue<P10>(9), reader.GetValue<P11>(10),
                    reader.GetValue<P12>(11), reader.GetValue<P13>(12), reader.GetValue<P14>(13), reader.GetValue<P15>(14),
                    reader.GetValue<P16>(15), reader.GetValue<P17>(16), reader.GetValue<P18>(17), reader.GetValue<P19>(18),
                    reader.GetValue<P20>(19)));

        public static async Task<List<R>> ExecuteAsync<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19, P20, R>(string cmd, CommandType commandType,
            Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, P17, P18, P19, P20, R> func, params IDataParameter[] parameters) =>
            await _ExecuteAsync(await CreateDefaultCommandAsync(cmd, commandType, parameters),
                reader => func(reader.GetValue<P1>(0), reader.GetValue<P2>(1), reader.GetValue<P3>(2),
                    reader.GetValue<P4>(3), reader.GetValue<P5>(4), reader.GetValue<P6>(5), reader.GetValue<P7>(6),
                    reader.GetValue<P8>(7), reader.GetValue<P9>(8), reader.GetValue<P10>(9), reader.GetValue<P11>(10),
                    reader.GetValue<P12>(11), reader.GetValue<P13>(12), reader.GetValue<P14>(13), reader.GetValue<P15>(14),
                    reader.GetValue<P16>(15), reader.GetValue<P17>(16), reader.GetValue<P18>(17), reader.GetValue<P19>(18), 
                    reader.GetValue<P20>(19)));

        #endregion excute command and return sqlreader value to callback function

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

        public static async Task<SqlQueryResultData> QuerySqlQueryResultDataAsync(string sql, CommandType commandType, params IDataParameter[] parameters)
        {
            SqlQueryResultData returnItem = new SqlQueryResultData();

            using (var dataReader = await SqlQuery.ExecuteCommandAsync(sql, commandType, parameters))
            {
                returnItem.heading = Enumerable.Range(0, dataReader.FieldCount).Select(dataReader.GetName)
                    .Where(s => !s.StartsWith("_")).ToList();
                while (await dataReader.ReadAsync())
                {
                    returnItem.data.Add(Enumerable.Range(0, dataReader.FieldCount)
                        .Where(i => !dataReader.GetName(i).StartsWith("_"))
                        .Select(i => dataReader.GetValue(i).ConvertObject(dataReader.GetFieldType(i))).ToArray());
                }
            }
            return returnItem;
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