using System;
using System.Collections.Generic;
using System.Text;

namespace HtmlGeneration
{
    public class DataModel : HtmlTemplateBase
    {
        private readonly string _datamodel;
        private readonly string _summary;

        public DataModel(string htmlTemplate, string datamodel, string summary)
            : base(htmlTemplate)
        {
            _datamodel = datamodel;
            _summary = summary;
        }

        public override string GenerateHtml()
        {
            return _templateText.Replace("[DataModel]", _datamodel).
                                 Replace("[Summary]", _summary);
        }

        public override bool IsValid(out string explanation)
        {
            return base.IsValid(out explanation, "DataModel", "Summary");
        }
    }
}
