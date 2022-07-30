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


app.MapGet("/", () => "Bem Vindo a Api");

//acessando servico
app.MapGet("/boasvindas", (HttpContext context, TesteService testeservice) =>
            testeservice.BoasVindas(context.Request.Query["nome"].ToString()));

//consumindo por model
app.MapGet("/catalogo", () => new Catalogo("livro01", "autor01"));

//consumindo pelo httpRest
app.MapGet("/album", async () =>
    await new HttpClient().GetStringAsync("https://jsonplaceholder.typicode.com/albums"));

//consumindo de um EF
app.MapGet("/Livros/{id}", async (int id, AppDbContext dbContext) =>
            {
               await dbContext.Livros.FirstOrDefaultAsync(a=> a.Id == id);
            });

app.MapPost("/Livros/", async (Livro livro, AppDbContext dbContext) =>
    {
        dbContext.Livros.Add(livro);
        await dbContext.SaveChangesAsync();
        return livro;
    }
);

app.MapPut("/Livros/{id}", async(int id, Livro livro, AppDbContext dbContext) =>
    {
        dbContext.Entry(livro).State = EntityState.Modified;
        await dbContext.SaveChangesAsync();
        return livro;
    }
);

 app.MapDelete("/Cliente/{id}", async (int id, AppDbContext dbContext) =>
    {
        var livro = await dbContext.Livros.FirstOrDefaultAsync(a=> a.Id == id);
        if(livro != null)
        {
            dbContext.Livros.Remove(livro);
            await dbContext.SaveChangesAsync();
        }
        return;
    });




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