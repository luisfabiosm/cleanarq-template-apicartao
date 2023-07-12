using Microsoft.OpenApi.Models;

namespace Infra.Register
{
    public static class SwaggerExtensions
    {
        public static void AddSwaggerAdapter(this IServiceCollection service)
        {

            service.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "API Cartão",
                    Description = "API tem como objetivo demonstrar uma abordagem Clean Arq",
                    Contact = new OpenApiContact
                    {
                        Name = "Fabio Magalhães",
                        Email = "luisfabiosm@gmail.com",
                    }
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                            new string[] {}

                    }
                });
            });
        }
    }
}