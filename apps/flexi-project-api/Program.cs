var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApp().AddInfra();

builder.Services.AddOpenApi();

const string corsPolicyName = "FrontEnd";

builder.Services.AddCors(q =>
{
    q.AddPolicy(corsPolicyName, builder =>
    {
        builder
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.MapUsersEndpoints();

app.UseCors(corsPolicyName);
app.Run();

public partial class Program { }
