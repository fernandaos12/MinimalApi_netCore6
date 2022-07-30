using System;


namespace MinApi.Extentions
{
    public static WebApplicationBuilder AddPersistence(this webApplicationBuilder builder)
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

/*
criando servico para conexao
*/