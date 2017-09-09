namespace HtmlGeneration
{
    internal class Controllers : HtmlTemplateBase
    {
        private readonly string _controllerName;

        public Controllers(string htmlTemplate, string controllerName)
            : base(htmlTemplate)
        {
            _controllerName = controllerName;
        }

        public override string GenerateHtml()
        {
            return _templateText.Replace("[Controller]", _controllerName);
        }

        public override bool IsValid(out string explanation)
        {
            return base.IsValid(out explanation, "Controller");
        }
    }
}
