using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SnjMedical.SelfHost.Features.Swagger.Filters;

/// <summary>
/// swagger deafult values operation filter
/// </summary>
internal class SwaggerDefaultValuesOperationFilter : IOperationFilter
{
    /// <summary>
    /// method applying logic
    /// </summary>
    /// <param name="operation"></param>
    /// <param name="context"></param>
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var apiDescription = context.ApiDescription;

        operation.Deprecated |= apiDescription.IsDeprecated();

        if (operation.Parameters is null)
        {
            return;
        }

        foreach (var parameter in operation.Parameters)
        {
            var description = apiDescription.ParameterDescriptions
                .First(p => p.Name == parameter.Name);
            parameter.Description ??= description.ModelMetadata.Description;
            parameter.Required |= description.IsRequired;
        }
    }
}
