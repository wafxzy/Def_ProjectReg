using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CommonReg.Common.Helpers
{
    public static class ResourceHelper
    {

        public static Stream ReadResource(Assembly assembly, string folder, string fileName)
        {
            string assemblyName = GetAssemblyName(assembly);

            string resourcePath = folder != null
                ? $"{assemblyName}.{folder}.{fileName}"
                : $"{assemblyName}.{fileName}";

            Stream stream = assembly.GetManifestResourceStream(resourcePath);

            return stream;
        }

        public static string ConvertResourceToBase64String(Assembly assembly, string folder, string fileName)
        {
            using Stream stream = ReadResource(assembly, folder, fileName);
            using MemoryStream memStream = new();

            stream.CopyTo(memStream);

            return Convert.ToBase64String(memStream.ToArray());
        }

        public static string GetFullResourcePath(string folder, string fileName)
        {
            return Path.Combine(AppContext.BaseDirectory, folder, fileName);
        }

        private static string GetAssemblyName(Assembly assembly)
        {
            return assembly.GetName().Name.Replace("-", "_");
        }
    }
}

