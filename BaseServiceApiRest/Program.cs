using BaseServiceApiRest.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Adicionando a configura��o do banco de dados
builder.Services.AddDatabaseConfiguration(builder.Configuration);

// Adicionando Swagger
builder.Services.DefineSwaggerGenConfiguration();

// Adicionando os controllers da API
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configura��o do Swagger (s� no ambiente de desenvolvimento)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
