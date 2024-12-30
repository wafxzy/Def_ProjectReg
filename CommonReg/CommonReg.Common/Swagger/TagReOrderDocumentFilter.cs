using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonReg.Common.Swagger
{

    public class TagReOrderDocumentFilter : IDocumentFilter
    {
        public void Apply(
            OpenApiDocument swaggerDoc,
            DocumentFilterContext context
            )
        {
            swaggerDoc.Tags = swaggerDoc.Tags
                .OrderBy(tag => tag.Name)
                .ToList();
        }
    }
}
