
using ApiModel;

namespace HtmlGeneration
{
    public static class DocumentGeneratorFactory
    {
        public static IDocumentGenerator Create()
        {
            return new FullHtmlGeneratorAdapter(new FullHtmlGenerator());
        }
    }
}
