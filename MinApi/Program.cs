using Microsoft.EntityFrameworkCore;
using MinApi.Endpoints;
using MinApi.Extentions;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.AddPersistence();

var app = builder.Build();

app.MapLivroEndpoints();

app.UseSwagger();


app.UseSwaggerUI();

app.UseHttpsRedirection();

app.Run();
