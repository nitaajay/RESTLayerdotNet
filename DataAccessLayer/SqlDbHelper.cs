using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Globalization;

/*
 * Created by: Yogesh Sonawane
 * Created date:  28 Jan. 2018 
 * Purpose: Communication channel between Sql Server Database and Web Api
*/

namespace WebApiSelfHostApp.DataAccessLayer
{
    public sealed class SqlDbHelper : IDisposable
    {
        private SqlConnection _connection;
        private DataSet _dataSet;
        private DataTable _dtTable;
        private OleDbConnection _oleDbConn = new OleDbConnection();

        public SqlDbHelper()
        {
            _connection = new SqlConnection();
        }

        public string ConString { get; set; }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        private static string GetSqlConnectionString()
        {
            var constr = ConfigurationManager.AppSettings["ConnectionString"].ToString(CultureInfo.InvariantCulture);
            return constr;
        }

        private SqlConnection OpenConnection()
        {
            if (_connection == null)
                _connection = new SqlConnection();
            if (_connection.State != ConnectionState.Closed) return _connection;

            _connection.ConnectionString = GetSqlConnectionString();
            _connection.Open();
            return _connection;
        }

        private OleDbConnection OpenConnectionOleDb()
        {
            if (_oleDbConn.State != ConnectionState.Closed) return _oleDbConn;

            _oleDbConn.ConnectionString =
                ConfigurationManager.ConnectionStrings["ConnectionStringOleDb"].ToString();
            _oleDbConn.Open();
            return _oleDbConn;
        }

        public DataTable ExecuteDatasetSp(string spName, SqlParameter[] paramArray, string opt)
        {
            try
            {
                _connection = null;
                using (_connection = new SqlConnection(opt))
                {
                    _connection.Open();
                    using (var sqlCommand = new SqlCommand
                    {
                        Connection = _connection,
                        CommandText = " Exec (' use " + paramArray[1].Value + "  Select * from " + paramArray[0].Value +
                                      " ')"
                    })
                    {
                        DataTable schemaTable;
                        using (IDataReader dr2 = sqlCommand.ExecuteReader(CommandBehavior.KeyInfo))
                        {
                            schemaTable = dr2.GetSchemaTable();
                            dr2.Close();
                        }

                        return schemaTable;
                    }
                }
            }
            finally
            {
                if (_connection != null) _connection.Close();
            }
        }

        public DataTable ExecuteStoredProcedure(string spName)
        {
            using (_dtTable = new DataTable())
            {
                using (_connection = OpenConnection())
                {
                    using (
                        var sqlCommand = new SqlCommand(spName, _connection) { CommandType = CommandType.StoredProcedure }
                    )
                    {
                        using (var reader = sqlCommand.ExecuteReader())
                        {
                            _dtTable.Load(reader);
                            return _dtTable;
                        }
                    }
                }
            }
        }

        public DataTable ExecuteStoredProcedure(string spName, string opt = null, params SqlParameter[] param)
        {
            using (_dtTable = new DataTable())
            {
                using (_connection = OpenConnection())
                {
                    using (var cmd = new SqlCommand(spName, _connection) { CommandType = CommandType.StoredProcedure })
                    {
                        foreach (var p in param)
                        {
                            cmd.Parameters.Add(p);
                        }

                        using (var reader = cmd.ExecuteReader())
                        {
                            _dtTable.Load(reader);
                            _connection.Close();
                            return _dtTable;
                        }
                    }
                }
            }
        }

        public DataSet ExecuteStoredProcedure(string spName, params SqlParameter[] param)
        {
            using (_dataSet = new DataSet())
            {
                using (_connection = OpenConnection())
                {
                    using (var cmd = new SqlCommand
                    {
                        Connection = _connection,
                        CommandType = CommandType.StoredProcedure,
                        CommandText = spName
                    })
                    {
                        if (param != null)
                        {
                            foreach (var p in param)
                            {
                                cmd.Parameters.Add(p);
                            }
                        }

                        using (var da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(_dataSet);
                            _connection.Close();
                            return _dataSet;
                        }
                    }
                }
            }
        }

        public void ExecuteInsertSp(string spName, params SqlParameter[] paramArray)
        {
            using (_connection = OpenConnection())
            {
                using (var cmd = new SqlCommand
                {
                    Connection = _connection,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = spName
                })
                {
                    foreach (var p in paramArray)
                    {
                        cmd.Parameters.Add(p);
                    }

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public int ExecuteNonQuery(string sqlQuery)
        {
            using (_dtTable = new DataTable())
            {
                using (_connection = OpenConnection())
                {
                    using (var sqlCommand = new SqlCommand { CommandText = sqlQuery, Connection = _connection })
                    {
                        var retVal = sqlCommand.ExecuteNonQuery();
                        return retVal;
                    }
                }
            }
        }

        public DataTable ExecuteNonQuery(string sqlQuery, CommandType commandType)
        {
            using (_dtTable = new DataTable())
            {
                using (_connection = OpenConnection())
                {
                    using (var selectCommand = new SqlCommand
                    {
                        CommandText = sqlQuery,
                        CommandType = commandType,
                        Connection = _connection
                    })
                    {
                        using (var adp = new SqlDataAdapter(selectCommand))
                        {
                            adp.Fill(_dtTable);
                            return _dtTable;
                        }
                    }
                }
            }
        }
        
        public DataTable ExecuteNonQueryOleDb(string sqlQuery, string entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            using (_dtTable = new DataTable())
            {
                using (_oleDbConn = OpenConnectionOleDb())
                {
                    _dtTable = _oleDbConn.GetOleDbSchemaTable(OleDbSchemaGuid.Primary_Keys,
                        new object[] { null, null, entity });
                    _oleDbConn.Close();
                    return _dtTable;
                }
            }
        }

        public DataTable ExecuteNonQueryTaskStatus(string sqlQuery)
        {
            using (var dtStatus = new DataTable())
            {
                using (var sqlConnection = new SqlConnection { ConnectionString = GetSqlConnectionString() })
                {
                    if (sqlConnection.State == ConnectionState.Closed)
                        sqlConnection.Open();
                    using (var selectCommand = new SqlCommand { CommandText = sqlQuery, Connection = sqlConnection })
                    {
                        using (var adp = new SqlDataAdapter(selectCommand))
                        {
                            adp.Fill(dtStatus);
                            return dtStatus;
                        }
                    }
                }
            }
        }

        public DataSet ExecuteNonQuery(string sqlQuery, string opt)
        {
            using (_dataSet = new DataSet())
            {
                using (_connection = OpenConnection())
                {
                    using (var sqlCommand = new SqlCommand { CommandText = sqlQuery, Connection = _connection })
                    {
                        using (var adp = new SqlDataAdapter(sqlCommand))
                        {
                            adp.Fill(_dataSet);
                            return _dataSet;
                        }
                    }
                }
            }
        }

        ~SqlDbHelper()
        {
            Dispose();
        }
    }
}