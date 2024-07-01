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
                    "<strong>0</strong> = Dinheiro",
                    "<strong>1</strong> = Cartão de Credito",
                    "<strong>2</strong> = Cartão de Debito",
                    "<strong>3</strong> = Transfêrencia Eletronica"
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
