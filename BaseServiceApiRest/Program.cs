using BaseServiceApiRest.Configuration;
using BaseServiceApiRest_Core.Middleware;


var builder = WebApplication.CreateBuilder(args);

// Configura��es de servi�os
builder.Services.AddDatabaseConfiguration(builder.Configuration);
builder.Services.DefineSwaggerGenConfiguration("Digital Elections");
builder.Services.AddInjectionDependency();
builder.Services.AddAuthenticationConfiguration();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCorsConfiguration();
builder.Services.AddTransient<ExceptionHandlerMiddleware>(); // Registre seu middleware de tratamento de exce��es
builder.Services.AddTransient<OperationsPermissionMiddleware>(); // Registre seu middleware de permiss�es

var app = builder.Build();

// Use seu middleware de tratamento de exce��es primeiro
app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseHttpsRedirection();
app.UseCors("standard_policy");
app.UseRouting(); // Adicione UseRouting aqui
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<OperationsPermissionMiddleware>(); // Use seu middleware de permiss�es

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.Run();