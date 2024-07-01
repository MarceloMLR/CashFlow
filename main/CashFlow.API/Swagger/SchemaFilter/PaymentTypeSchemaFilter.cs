using CashFlow.Domain.Enums;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CashFlow.Api.Swagger.SchemaFilter
{
    public class PaymentTypeSchemaFilter : IEnumSchemaFilter
    {
        public void Execute(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type.Name == typeof(PaymentType).Name)
            {
                var enumDescriptions = new List<string>
                {
                    "<span style='color:red; font-weight:bold;'>0</span> = Dinheiro",
                    "<span style='color:red'>1</span> = Cartão de Credito",
                    "<span style='color:red'>2</span> = Cartão de Debito",
                    "<span style='color:red'>3</span> = Transfêrencia Eletronica"
                };

                schema.Description = $"Valores disponíveis:<br><br>{string.Join("<br>", enumDescriptions)}";
                schema.Type = "integer";
                schema.Format = "int32";
                schema.Enum = Enum.GetValues(context.Type)
                    .Cast<object>()
                    .Select(value => new OpenApiInteger((int)value))
                    .ToList<IOpenApiAny>();
            }
        }
    }
}
