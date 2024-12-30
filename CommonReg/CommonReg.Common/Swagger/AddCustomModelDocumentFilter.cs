using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonReg.Common.Swagger
{
    public class AddCustomModelDocumentFilter<T> : IDocumentFilter where T : class
    {
        private readonly Type _abstractType;
        private readonly bool _applyForAll;
        private readonly HashSet<string> _documentTitles;

        public AddCustomModelDocumentFilter() : this(Array.Empty<string>())
        { }

        public AddCustomModelDocumentFilter(
            string documentTitle
            ) : this(new[] { documentTitle })
        { }

        public AddCustomModelDocumentFilter(
            string[] documentsTitles
            )
        {
            if (documentsTitles == null) throw new ArgumentNullException(nameof(documentsTitles));

            _documentTitles = new HashSet<string>(documentsTitles.Where(x => !string.IsNullOrWhiteSpace(x)), StringComparer.Ordinal);
            _applyForAll = _documentTitles.Count == 0;
            _abstractType = typeof(T);
        }

        public void Apply(
            OpenApiDocument swaggerDoc,
            DocumentFilterContext context
            )
        {
            if (!_applyForAll && !_documentTitles.Contains(swaggerDoc.Info.Title)) return;

            SchemaRepository schemaRepository = context.SchemaRepository;
            ISchemaGenerator schemaGenerator = context.SchemaGenerator;
            schemaGenerator.GenerateSchema(_abstractType, schemaRepository);
        }
    }
}
