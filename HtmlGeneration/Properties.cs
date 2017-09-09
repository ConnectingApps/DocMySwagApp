using ApiModel;
using System.Collections.Generic;
using System.Linq;

namespace HtmlGeneration
{
    internal class Properties : HtmlTemplateBase
    {
        private readonly IEnumerable<DataProperty> _dataProperties;

        public Properties(string htmlTemplate, IEnumerable<DataProperty> dataProperties)
            : base(htmlTemplate)
        {
            _dataProperties = dataProperties ?? new List<DataProperty>();
        }

        public override string GenerateHtml()
        {
            var htmlFragment = from d in _dataProperties
                select _templateText.Replace("[PropertyName]", d.ShortName)
                    .Replace("[Summary]", d.Summary);
            return string.Join('\n', htmlFragment);
        }

        public override bool IsValid(out string explanation)
        {
            return base.IsValid(out explanation, "PropertyName", "Summary");
        }
    }
}
