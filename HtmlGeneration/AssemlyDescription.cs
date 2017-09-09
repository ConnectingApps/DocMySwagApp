namespace HtmlGeneration
{
    internal class AssemlyDescription : HtmlTemplateBase
    {
        private readonly string _assemblyName;

        public AssemlyDescription(string htmlTemplate, string assemblyName)
            : base(htmlTemplate)
        {
            _assemblyName = assemblyName;
        }

        public override string GenerateHtml()
        {
            return _templateText.Replace("[Assembly]", _assemblyName);
        }

        public override bool IsValid(out string explanation)
        {
            return base.IsValid(out explanation, "Assembly");
        }
    }
}
