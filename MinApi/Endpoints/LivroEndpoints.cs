using System;
using MinApi.Data;
using static MinApi.Data.LivroContext;

namespace MinApi.Endpoints{

    public static void MapLivroEndpoints(this Webapplication app)
    {
        app.MapGet("/", () => { return "Bem Vindo a Api"; });

        app.MapGet("/livros", async (GetConnection connectionGetter) =>
        {
            using var con = await connectionGetter();
            return con.GetAll<Livro>().ToList();
        });

        app.MapGet("/livros/{id}", async (int id, GetConnection connectionGetter) =>
        {
            using var con = await connectionGetter();    
            return con.Get<Livro>().ToList();
        });

        app.MapPost("/livros", async (Livro livro, GetConnection connectionGetter) =>
            {
                using var con = await connectionGetter(); 
                var id = con.Insert(livro);
                return Results.Created($"/livros/{id}", Livro);
            }
        );

        app.MapPut("/livros/{id}", async(int id, Livro livro, GetConnection connectionGetter) =>
            {
             using var con = await connectionGetter(); 
             var id = con.Update(livro);
             return Results.Ok("Atualizado com sucesso.");
            }
        );

        app.MapDelete("/livro/{id}", async (int id, AppDbContext dbContext) =>
            {
              using var con = await connectionGetter(); 
              con.Delete(new Livro(id, ""));
              return Results.Ok("Excluído com Sucesso.");
        });

    } 
}