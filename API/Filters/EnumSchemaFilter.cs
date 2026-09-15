using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text.Json.Nodes;

namespace API.Filters
{
    public class EnumSchemaFilter : ISchemaFilter
    {
        public void Apply(IOpenApiSchema schema, SchemaFilterContext context)
        {
            if (!context.Type.IsEnum || schema is not OpenApiSchema openApiSchema)
                return;

            openApiSchema.Type = JsonSchemaType.String;
            openApiSchema.Enum = Enum.GetNames(context.Type)
                .Select(name => (JsonNode)JsonValue.Create(name))
                .ToList();
        }
    }
}
