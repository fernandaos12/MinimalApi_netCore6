using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

//habilitar o swagger como serviço
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//adicionando serviço usando tempo de vida AddSingleton incluindo testeService como dependencia na colecao de servicos
builder.Services.AddSingleton<TesteService>(new TesteService());

//Adicionando DBContext para EF depois de adicionar o package Microsoft.EntityFrameworkCore.InMemory
builder.Services.AddDbContext<AppDbContext>(options=>
   options.UseInMemoryDatabase("Livros"));


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger();
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


app.UseSwaggerUI();

await app.RunAsync();

public record Catalogo(string descricao, string autor);

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

    public DbSet<Livro>? Livros{get;set;}
}