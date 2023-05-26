using System;
using System.Data;
using System.Data.SqlClient;

namespace AlbyLib
{
    public class AlbySqlControllerMultiple
    {
        private string _connectionString = "";

        public string ConnectionString
        {
            get => _connectionString;
        }

        public AlbySqlControllerMultiple(string connectionString)
        {
            try
            {
                _connectionString = connectionString.Trim();

                if(string.IsNullOrEmpty(_connectionString))                
                    throw new Exception("Invalid connection string");
            }
            catch (Exception ex)
            {
                this._connectionString = "";
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Fa una query ma con dei parametri
        /// </summary>
        /// <param name="query"></param>
        /// <param name="parameters"></param>
        /// <returns>Il risultato della query</returns>
        /// 
        public object Query(string query, SqlParameter[] parameters, QueryType qt = QueryType.Query)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = this.ConnectionString;
            cn.Open();
            
            object response = null;

            SqlCommand cmd = new SqlCommand
            {
                Connection = cn,
                CommandType = CommandType.Text,
                CommandText = query
            };

            cmd.Parameters.AddRange(parameters);

            cmd.Prepare();

            switch (qt)
            {
                case QueryType.Query:

                    response = new DataTable();
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill((DataTable)response);

                    break;
                case QueryType.NonQuery:

                    response = cmd.ExecuteNonQuery();

                    break;
                case QueryType.Scalar:

                    response = cmd.ExecuteScalar();

                    break;
            }
            
            cn.Close();

            return response;
        }

        /// <summary>
        /// Metodo generale per le quey
        /// </summary>
        /// <param name="query">Decisamente autoesplicativo</param>
        /// <param name="qt">Il tipo di query che si vuole eseguire (usare enum QueryType)</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>

        public object Query(string query, QueryType qt = QueryType.Query)
        {
            object response = null;
            
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = this.ConnectionString;
            cn.Open();

            SqlCommand cmd = new SqlCommand
            {
                Connection = cn,
                CommandType = CommandType.Text,
                CommandText = query
            };

            switch (qt)
            {
                case QueryType.Query:

                    response = new DataTable();
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill((DataTable)response);

                    break;
                case QueryType.NonQuery:

                    response = cmd.ExecuteNonQuery();

                    break;
                case QueryType.Scalar:

                    response = cmd.ExecuteScalar();

                    break;
            }
            
            cn.Close();

            return response;
        }

        /// <summary>
        /// Construisce il paramentro SqlParameter, usare questo metodo solo per varchar
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <returns>SqlParameter costruito</returns>
        
        public SqlParameter BuildParam(string name, SqlDbType type, string value)
            => new SqlParameter(name, type) { Value = value };

        /// <summary>
        /// Construisce il paramentro SqlParameter, non usare questo metodo per varchar
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <returns>SqlParameter costruito</returns>
         
        public SqlParameter BuildParam(string name, SqlDbType type,int size, string value)
            => new SqlParameter(name, type, size) { Value = value };
    }
    
    // --------------------------------------------------------------------------
    
    public class AlbySqlControllerSingle
    {
        private string _connectionString = "";
        private SqlConnection cn;
        private SqlCommand cmd;
        private SqlDataAdapter adp;

        public string ConnectionString
        {
            get => _connectionString;
        }

        public AlbySqlControllerSingle(string connectionString)
        {
            try
            {
                _connectionString = connectionString.Trim();

                if(string.IsNullOrEmpty(_connectionString))                
                    throw new Exception("Invalid connection string");
                
                cn = new SqlConnection();
                cn.ConnectionString = this.ConnectionString;
                cn.Open();
            }
            catch (Exception ex)
            {
                this._connectionString = "";
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Fa una query ma con dei parametri
        /// </summary>
        /// <param name="query"></param>
        /// <param name="parameters"></param>
        /// <returns>Il risultato della query</returns>
        /// 
        public object Query(string query, SqlParameter[] parameters, QueryType qt = QueryType.Query)
        {
            object response = null;

            cmd = new SqlCommand
            {
                Connection = cn,
                CommandType = CommandType.Text,
                CommandText = query
            };

            cmd.Parameters.AddRange(parameters);

            cmd.Prepare();

            switch (qt)
            {
                case QueryType.Query:

                    response = new DataTable();
                    adp = new SqlDataAdapter(cmd);
                    adp.Fill((DataTable)response);

                    break;
                case QueryType.NonQuery:

                    response = cmd.ExecuteNonQuery();

                    break;
                case QueryType.Scalar:

                    response = cmd.ExecuteScalar();

                    break;
            }
            
            cn.Close();

            return response;
        }

        /// <summary>
        /// Metodo generale per le quey
        /// </summary>
        /// <param name="query">Decisamente autoesplicativo</param>
        /// <param name="qt">Il tipo di query che si vuole eseguire (usare enum QueryType)</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>

        public object Query(string query, QueryType qt = QueryType.Query)
        {
            object response = null;
            
            cmd = new SqlCommand
            {
                Connection = cn,
                CommandType = CommandType.Text,
                CommandText = query
            };

            switch (qt)
            {
                case QueryType.Query:

                    response = new DataTable();
                    adp = new SqlDataAdapter(cmd);
                    adp.Fill((DataTable)response);

                    break;
                case QueryType.NonQuery:

                    response = cmd.ExecuteNonQuery();

                    break;
                case QueryType.Scalar:

                    response = cmd.ExecuteScalar();

                    break;
            }
            
            cn.Close();

            return response;
        }

        /// <summary>
        /// Construisce il paramentro SqlParameter, usare questo metodo solo per varchar
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <returns>SqlParameter costruito</returns>
        
        public SqlParameter BuildParam(string name, SqlDbType type, string value)
            => new SqlParameter(name, type) { Value = value };

        /// <summary>
        /// Construisce il paramentro SqlParameter, non usare questo metodo per varchar
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <returns>SqlParameter costruito</returns>
         
        public SqlParameter BuildParam(string name, SqlDbType type,int size, string value)
            => new SqlParameter(name, type, size) { Value = value };
    }

    public enum QueryType
    {
        Query = 0,
        NonQuery = 1,
        Scalar = 2,
    }
}
