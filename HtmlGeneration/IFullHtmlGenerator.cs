using ApiModel;

namespace HtmlGeneration
{
    public interface IFullHtmlGenerator
    {
        bool TryInitialize(out string explanation);
        bool TryGenerateOutputFile(SwaggerModel swaggerModel, string outPutFile, out string explanation);
    }
}