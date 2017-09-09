using System.Linq;

namespace HtmlGeneration
{
    internal abstract class HtmlTemplateBase : IHtmlTemplate
    {
        protected readonly string _templateText;
        protected HtmlTemplateBase(string templateText)
        {
            _templateText = templateText;
        }

        protected virtual bool IsValid(out string explanation, params string[] required)
        {
            var missingParameters = (from r in required
                where !_templateText.Contains($"[{r}]")
                select r).ToList();
            explanation = missingParameters.Any() ? $"Missing Parameters: {string.Join(',', missingParameters)}" : null;
            return explanation == null;
        }

        public abstract string GenerateHtml();
        public abstract bool IsValid(out string explanation);

    }
}
