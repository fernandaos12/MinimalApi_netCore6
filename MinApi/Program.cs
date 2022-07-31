using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

//habilitar o swagger como serviço
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

app.UseSwagger();


app.UseSwaggerUI();

await app.RunAsync();

//adicionando serviço usando tempo de vida AddSingleton incluindo testeService como dependencia na colecao de servicos
builder.Services.AddSingleton<TesteService>(new TesteService());


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}




public class TesteService
{
    public string BoasVindas(string nome)
    {
        return $"Bem Vindo {nome}";
    }
}


public class Livro{
    public int Id{get; set;}
    public string? Nome { get; set; }    
    
}

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options): base(options)
    {

    }

    public DbSet<Livro> Livros{get;set;}
}