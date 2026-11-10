
using BibliotecaApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Configuração dos Controllers
builder.Services.AddControllers();

// Configuração do Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Repository registrado como Singleton
builder.Services.AddSingleton<IMaterialRepository, MaterialRepository>();

// Configuração do CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirFrontend", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Ativar o Swagger
app.UseSwagger();
app.UseSwaggerUI();

// Ativar o CORS
app.UseCors("PermitirFrontend");

// Mapear os Controllers
app.MapControllers();

app.Run();
