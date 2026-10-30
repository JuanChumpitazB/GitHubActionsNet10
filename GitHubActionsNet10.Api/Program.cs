using GitHubActionsNet10.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Registrar nuestro servicio
builder.Services.AddSingleton<CalculadoraService>();

var app = builder.Build();

// Endpoint para sumar dos números
app.MapGet("/api/calculadora/sumar",
    (int numero1, int numero2, CalculadoraService calculadora) =>
    {
        var resultado = calculadora.Sumar(numero1, numero2);

        return Results.Ok(new
        {
            Numero1 = numero1,
            Numero2 = numero2,
            Resultado = resultado
        });
    });

app.Run();