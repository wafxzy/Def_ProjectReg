using CommonReg.Common.Helpers;
using RazorLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonReg.EmailSender.Helpers
{
    public static  class EmailTemplateGeneratorHelper
    {
    
        private const string BASE_FOLDER_NAME = "Views.";
        private const string IMAGE_FOLDER_NAME = "Images";
        private const string LOGO_IMAGE = "image.png";
        private const string SIGNATURE_IMAGE_NAME = "logo.png";

        private static readonly RazorLightEngine _engine = new RazorLightEngineBuilder()
                   .UseEmbeddedResourcesProject(typeof(CommonRegEmailAssemblyMarket))
                   .UseMemoryCachingProvider()
                   .Build();
        public static string SignatureImgBase64 { get; } = ResourceHelper.ConvertResourceToBase64String(
       typeof(CommonRegEmailAssemblyMarket).Assembly,
       IMAGE_FOLDER_NAME,
       SIGNATURE_IMAGE_NAME);

        public static string CompasImgBase64 { get; } = ResourceHelper.ConvertResourceToBase64String(
typeof(CommonRegEmailAssemblyMarket).Assembly,
IMAGE_FOLDER_NAME,
LOGO_IMAGE);

        public static Task<string> GenerateTemplateAsync<T>(
            string templateKey,
            T model = null
            ) where T : class => _engine.CompileRenderAsync(string.Concat(BASE_FOLDER_NAME, templateKey), model);
    }
}

