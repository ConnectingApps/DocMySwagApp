namespace HtmlGeneration
{
    public class NoDataHtml : HtmlTemplateBase
    {
        public NoDataHtml(string htmlTemplate)
            :base(htmlTemplate) 
        {
        }

        public override string GenerateHtml()
        {
            return _templateText;
        }

        public override bool IsValid(out string explanation)
        {
            explanation = null;
            return true;
        }
    }
}
