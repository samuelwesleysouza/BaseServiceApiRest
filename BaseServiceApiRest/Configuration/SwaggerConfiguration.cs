using Microsoft.OpenApi.Models;

namespace BaseServiceApiRest.Configuration
{
    public static class SwaggerConfiguration
    {
        public static void DefineSwaggerGenConfiguration(this IServiceCollection services,
           string title = "Digital Elections API",
           string description = "")
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = title,
                    Version = "v1",
                    Description = description
                });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    In = ParameterLocation.Header,
                    Description = "Por favor informe um <b>Token JWT</b> válido no campo abaixo.",
                    Name = "Authorization",
                    BearerFormat = "JWT",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new List<string>()
                    }
                });
            });
        }
    }
}
