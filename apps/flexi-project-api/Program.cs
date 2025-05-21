using FlexiProject.Application;
using FlexiProject.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApp().AddInfra();

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.MapUsersEndpoints();

app.Run();

public partial class Program { }
