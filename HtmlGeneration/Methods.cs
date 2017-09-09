using ApiModel;
using System.Collections.Generic;
using System.Linq;

namespace HtmlGeneration
{
    internal class Methods : HtmlTemplateBase
    {
        private readonly IEnumerable<ControllerMethod> _controllerMethods;

        public Methods(string htmlTemplate, IEnumerable<ControllerMethod> controllerMethods)
            : base(htmlTemplate)
        {
            _controllerMethods = controllerMethods;
        }

        public override string GenerateHtml()
        {
            var htmlFragment = from c in _controllerMethods
                               select _templateText.Replace("[MethodName]", c.ShortName)
                                                   .Replace("[Summary]", c.Summary)
                                                   .Replace("[Returns]", c.Returns);
            return string.Join('\n', htmlFragment);
        }

        public override bool IsValid(out string explanation)
        {
            return base.IsValid(out explanation, "MethodName", "Summary", "Returns");
        }
    }
}
