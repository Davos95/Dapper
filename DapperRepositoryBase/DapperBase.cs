using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using static Dapper.SqlMapper;

namespace DapperRepositoryBase
{
    internal class DapperBase : IDisposable
    {
        private string ConnectionString;
        private IDbConnection _connection;
        private IDbTransaction _transaction;

        public DapperBase()
        {
            ConnectionString = string.Empty;
        }

        public void StartTransaction()
        {
            _connection = new SqlConnection(ConnectionString);
            _connection.Open();

            _transaction = _connection.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _transaction?.Commit();
            _connection?.Close();
        }

        public void RollbackTransaction()
        {
            _transaction?.Rollback();
            _connection?.Close();
        }


        public List<T> SetQuery<T, U>(string storedProcedure, U parameters)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionString))
            {
                List<T> rows = connection.Query<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure).ToList();

                return rows;
            }
        }

        public List<T> SetQueryInTransaction<T, U>(string storedProcedure, U parameters)
        {
            List<T> rows = _connection.Query<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure, transaction: _transaction).ToList();

            return rows;
        }

        public void SetExecute<T>(string storedProcedure, T parameters)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Execute(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public void SetExecuteInTransaction<T>(string storedProcedure, T parameters)
        {
            _connection.Execute(storedProcedure, parameters, commandType: CommandType.StoredProcedure, transaction: _transaction);
        }


        public GridReader SetQueryMultiple<T>(string storedProcedure, T parameters)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionString))
            {
                var reader = connection.QueryMultiple(storedProcedure, parameters, commandType: CommandType.StoredProcedure);

                return reader;
            }

        }

        public GridReader SetQueryMultipleInTransaction<T>(string storedProcedure, T parameters)
        {
            var reader = _connection.QueryMultiple(storedProcedure, parameters, commandType: CommandType.StoredProcedure, transaction: _transaction);

            return reader;
        }

        public void Dispose()
        {
            CommitTransaction();
        }
    }
}
