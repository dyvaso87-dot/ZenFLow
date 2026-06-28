using ZenFlow.API.Proxy;
using ZenFlow.Datos;
using ZenFlow.Logica;
using ZenFlow.Repositorios;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new()
    {
        Title = "ZenFlow API",
        Version = "v1",
        Description = "API REST para gestionar tareas y hábitos de ZenFlow"
    });
    // Mostrar el campo Authorization en Swagger
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Escribe: zenflow-token-2025"
    });
    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddHttpContextAccessor();

// Singleton — misma instancia que la app WPF
builder.Services.AddSingleton<ITareaRepo>(_ => TareaRepoJson.Instancia);
builder.Services.AddSingleton<IHabitoRepo>(_ => HabitoRepoJson.Instancia);
builder.Services.AddSingleton<GestorTareas>();
builder.Services.AddSingleton<GestorHabitos>();

// Proxy — los controllers reciben IGestorTareas, no GestorTareas directamente
builder.Services.AddScoped<IGestorTareas, TareaServiceProxy>();
builder.Services.AddScoped<IGestorHabitos, HabitoServiceProxy>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ZenFlow API v1");
    c.RoutePrefix = string.Empty;
});

app.UseAuthorization();
app.MapControllers();
app.Run();