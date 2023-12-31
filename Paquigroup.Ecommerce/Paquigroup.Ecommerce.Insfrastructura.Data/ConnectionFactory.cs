﻿using Microsoft.Extensions.Configuration;
using Paquigroup.Ecommerce.Transversal.Common;
using System.Data;
using System.Data.SqlClient;



namespace Paquigroup.Ecommerce.Infrastructura.Data
{
    public class ConnectionFactory : IConnectionFactory
    {
        private readonly IConfiguration _configuration;

        public ConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection GetConnection
        {
            get
            {
                var sqlConnection = new SqlConnection();
                if (sqlConnection == null) return null;

                sqlConnection.ConnectionString = _configuration.GetConnectionString("NorthWindConnection");
                sqlConnection.Open();
                return sqlConnection;

            }
        }
    }
}