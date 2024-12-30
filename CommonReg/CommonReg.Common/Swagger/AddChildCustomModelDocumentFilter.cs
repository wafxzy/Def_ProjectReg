using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CommonReg.Common.Swagger
{

    public class AddChildCustomModelDocumentFilter<T> : IDocumentFilter where T : class
    {
        private readonly Type _abstractType;
        private readonly bool _applyForAll;
        private readonly HashSet<string> _documentTitles;

        public AddChildCustomModelDocumentFilter() : this(Array.Empty<string>())
        { }

        public AddChildCustomModelDocumentFilter(
            string documentTitle
            ) : this(new[] { documentTitle })
        { }

        public AddChildCustomModelDocumentFilter(
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
            if (!_applyForAll && !_documentTitles.Contains(swaggerDoc.Info.Title))
                return;

            SchemaRepository schemaRepository = context.SchemaRepository;
            ISchemaGenerator schemaGenerator = context.SchemaGenerator;

            // register all subclasses
            IEnumerable<Type> derivedTypes = _abstractType.GetTypeInfo().Assembly.GetTypes()
                .Where(x => _abstractType != x && !x.IsAbstract && x.IsPublic && _abstractType.IsAssignableFrom(x));

            foreach (Type type in derivedTypes)
                schemaGenerator.GenerateSchema(type, schemaRepository);
        }
    }

}
