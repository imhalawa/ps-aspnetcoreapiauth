using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Globomantics.Api.Extensions.Swagger
{
    public static class SwaggerApiKeySecurity
    {
        public static void AddSwaggerApiKeySecurity(this SwaggerGenOptions self)
        {
            // Define Security Scheme
            self.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
            {
                Description = "ApiKey must appear in header",
                Type = SecuritySchemeType.ApiKey,
                Name = "XApiKey",
                In = ParameterLocation.Header,
                Scheme = "ApiKeyScheme"
            });

            // Turn the definition into a requirement
            self.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme()
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id="ApiKey"
                        },
                        In = ParameterLocation.Header
                    }
                    ,
                    new List<string>()
                }
            });

        }
    }
}
