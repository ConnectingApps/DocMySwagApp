namespace HtmlGeneration
{
    internal interface IHtmlTemplate
    {
        bool IsValid(out string explanation);
        string GenerateHtml();
    }
}