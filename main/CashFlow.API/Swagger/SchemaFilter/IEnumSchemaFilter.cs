using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CashFlow.Api.Swagger.SchemaFilter
{
    public interface IEnumSchemaFilter
    {
        void Execute(OpenApiSchema schema, SchemaFilterContext context);
    }
}
