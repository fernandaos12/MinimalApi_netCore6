using System;
using static MinApi.Data.LivroContext;
using System.Data.SqlClient;

namespace MinApi.Extentions
{
    public static class ServiceCollectionExtensions
    {
    //metodo de extensao
        public static WebApplicationBuilder AddPersistence(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<GetConnection>(p=>
            async () =>
            {
                var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
                var connection = new SqlConnection(connectionString);

                await connection.OpenAsync();
                return connection;
            });

            return builder;
        }

    }
}

/*
criando servico para conexao
*/