using System;
using MinApi.Data;
using static MinApi.Data.LivroContext;
using MinApi.Extentions;
using Dapper.Contrib.Extensions;
using Dapper;

namespace MinApi.Endpoints{

    public static class LivrosEndpoints
    {
        public static void MapLivroEndpoints(this WebApplication app)
        {
            app.MapGet("/", () =>
            {
                return "Bem Vindo a Api";
            });

            app.MapGet("/livros", async (GetConnection connectionGetter) =>
            {
                using var con = await connectionGetter();
                return con.GetAll<Livro>().ToList();
            });

            app.MapGet("/livros/{id}", async (int id, GetConnection connectionGetter) =>
            {
                using var con = await connectionGetter();
                var result = con.Get<Livro>(id);
                return result;
            });

            app.MapPost("/livros", async (Livro livro, GetConnection connectionGetter) =>
                {
                    using var con = await connectionGetter();
                    var id = con.Insert(livro);
                    return Results.Created($"/livros/{id}", livro);
                }
            );

            app.MapPut("/livros/{id}", async (int id, Livro livro, GetConnection connectionGetter) =>
                {
                    using var con = await connectionGetter();
                    con.Update(livro);
                    return Results.Ok("Atualizado com sucesso.");
                });

            app.MapDelete("/livro/{id}", async (int id, GetConnection connectionGetter) =>
                {
                    using var con = await connectionGetter();
                    con.Delete(new Livro(id, "", "", Convert.ToInt32("")));
                    return Results.Ok("Excluído com Sucesso.");
                });

        }
    }
}