namespace HtmlGeneration
{
    public interface IHtmlTemplate
    {
        bool IsValid(out string explanation);
        string GenerateHtml();
    }
}