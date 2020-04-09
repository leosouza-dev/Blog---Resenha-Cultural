using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Infra
{
    public class ConnectionFactory
    {
        public static SqlConnection CriaConexaoAberta()
        {
            // precisamos acessar o "appsettings.json"
            var builder = new ConfigurationBuilder()
                           .SetBasePath(Directory.GetCurrentDirectory())
                           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                           .AddEnvironmentVariables();

            // recupera a connection string
            IConfiguration configuration = builder.Build();
            string stringConexao = configuration.GetConnectionString("Blog");

            // Abre a conexão
            SqlConnection conexao = new SqlConnection(stringConexao);
            conexao.Open();

            return conexao;
        }
    }
}
