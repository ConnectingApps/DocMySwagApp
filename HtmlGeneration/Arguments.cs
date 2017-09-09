using ApiModel;
using System.Collections.Generic;
using System.Linq;

namespace HtmlGeneration
{
    internal class Arguments : HtmlTemplateBase
    {
        private readonly IEnumerable<Argument> _arguments;

        public Arguments(string htmlTemplate, IEnumerable<Argument> arguments)
            : base(htmlTemplate)
        {
            _arguments = arguments;
        }

        public override string GenerateHtml()
        {
            var htmlFragment = from a in _arguments
                select _templateText.Replace("[ArgumentName]", a.Name)
                                    .Replace("[Description]",a.Description)
                                    .Replace("[DataType]", a.Type.Name);
            return string.Join('\n', htmlFragment);
        }

        public override bool IsValid(out string explanation)
        {
            return base.IsValid(out explanation, "ArgumentName", "Description", "DataType");
        }
    }
}
