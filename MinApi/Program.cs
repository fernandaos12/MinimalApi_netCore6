var builder = WebApplication.CreateBuilder(args);

//habilitar o swagger como serviço
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//adicionando serviço usando tempo de vida AddSingleton incluindo testeService como dependencia na colecao de servicos
builder.Services.AddSingleton<TesteService>(new TesteService());

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