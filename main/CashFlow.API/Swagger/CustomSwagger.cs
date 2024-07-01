using CashFlow.Api.Swagger.SchemaFilter;
using CashFlow.Communication.Enums;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Reflection;

namespace CashFlow.Api.CustomSwagger
{
    public static class CustomSwagger
    {
        public static void AddCustomSwagger(this IServiceCollection services)
        {

            services.AddSwaggerGen(config =>
            {
                config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = @"JWT Authorization header using Bearer scheme.
                        Enter 'Bearer' [space] and then your token in the text input below.
                        Example: 'Bearer 1234abcdfe'",
                    In = ParameterLocation.Header,
                    Scheme = "Bearer",
                    Type = SecuritySchemeType.ApiKey
                });

                config.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
                config.EnableAnnotations();
                
                config.SchemaFilter<EnumSchemaFilter>();
            });
        }

        private class EnumSchemaFilter : ISchemaFilter
        {
            private readonly IEnumerable<IEnumSchemaFilter> _enumSchemaFilters;

            public EnumSchemaFilter()
            {
               
                _enumSchemaFilters = new List<IEnumSchemaFilter>
                {
                    new PaymentTypeSchemaFilter()
                };
            }
            public void Apply(OpenApiSchema schema, SchemaFilterContext context)
            {
                foreach (var filter in _enumSchemaFilters)
                {
                    filter.Execute(schema, context);
                }
            }
        }
    }

}
