using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonReg.Common.Swagger
{
    public class NSwagEnumExtensionSchemaFilter : ISchemaFilter
    {
        public void Apply(
            OpenApiSchema model,
            SchemaFilterContext context
            )
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (!context.Type.IsEnum) return;

            string[] names = Enum.GetNames(context.Type);
            OpenApiArray openApiArray = new();
            openApiArray.AddRange(names.Select(x => new OpenApiString(x)));
            model.Extensions.Add("x-enumNames", openApiArray);
        }
    }

}
