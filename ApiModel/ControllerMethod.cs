using System.Collections.Generic;
using System.Linq;

namespace ApiModel
{
    public class ControllerMethod
    {
        public string ShortName => Name.Split('(').First().Split('.').Last();
        public string Summary { get; set; }
        public string Name { get; set; }
        public string Returns { get; set; }
        public List<Argument> Arguments { get; set; }
    }
}
