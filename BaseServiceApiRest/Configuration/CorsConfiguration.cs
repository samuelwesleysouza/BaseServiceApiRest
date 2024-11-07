namespace BaseServiceApiRest.Configuration;

public static class CorsConfiguration
{
    public static void AddCorsConfiguration(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("standard_policy", policy =>
                policy.AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin()
            );
        });
    }
}