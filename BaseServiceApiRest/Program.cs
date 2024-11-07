using BaseServiceApiRest.Configuration;
using BaseServiceApiRest_Core.Middleware;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDatabaseConfiguration(builder.Configuration);
builder.Services.DefineSwaggerGenConfiguration("Digital Elections");
builder.Services.AddInjectionDependency();
builder.Services.AddAuthenticationConfiguration();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCorsConfiguration();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("standard_policy");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseMiddleware<OperationsPermissionMiddleware>();
app.MapControllers();
app.Run();