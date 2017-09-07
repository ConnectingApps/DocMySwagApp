namespace ApiModel
{
    public interface IDocumentGenerator
    {
        string FileType { get; }
        string Description { get; }
        bool TryGenerateOutputFile(SwaggerModel swaggerModel, string outPutFile, out string explanation);
        bool TryInitialize(out string explanation);
    }
}