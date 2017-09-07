using ApiModel;
using System;

namespace HtmlGeneration
{
    internal class FullHtmlGeneratorAdapter : IFullHtmlGenerator, IDocumentGenerator
    {
        private readonly IFullHtmlGenerator _fullHtmlGenerator;

        public FullHtmlGeneratorAdapter(IFullHtmlGenerator fullHtmlGenerator)
        {
            _fullHtmlGenerator = fullHtmlGenerator;
        }

        public string FileType => "html";

        public string Description => "An html file will be generated based on the html fragments available in \n" +
                                     $"{AppContext.BaseDirectory} \nFeel free to modify them but please ensure that" +
                                     $"the elements between [] remain unchanged";

        public bool TryGenerateOutputFile(SwaggerModel swaggerModel, string outPutFile, out string explanation)
        {
            return _fullHtmlGenerator.TryGenerateOutputFile(swaggerModel, outPutFile, out explanation);
        }

        public bool TryInitialize(out string explanation)
        {
            return _fullHtmlGenerator.TryInitialize(out explanation);
        }
    }
}
