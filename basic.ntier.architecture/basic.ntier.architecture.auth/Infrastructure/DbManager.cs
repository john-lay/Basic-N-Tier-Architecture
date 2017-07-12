namespace basic.ntier.architecture.auth.Infrastructure
{
    using System;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;

    /// <summary>
    /// A simple database connection manager
    /// </summary>
    public class DbManager : IDisposable
    {
        private IDbConnection conn;

        /// <summary>
        /// Create a new Sql database connection
        /// </summary>
        public DbManager()
        {
            var connString = ConfigurationManager.ConnectionStrings["AuthConnection"].ConnectionString;
            conn = new SqlConnection(connString);
        }

        /// <summary>
        /// Return open connection
        /// </summary>
        public IDbConnection Connection
        {
            get
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                return conn;
            }
        }

        /// <summary>
        /// Close and dispose of the database connection
        /// </summary>
        public void Dispose()
        {
            if (conn != null)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                    conn.Dispose();
                }
                conn = null;
            }
        }
    }
}