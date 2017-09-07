using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApiModel
{
    /// <summary>
    /// Desribed with P
    /// </summary>
    public class DataProperty
    {
        public string ShortName => Name.Split('.').Last();
        public string Name { get; set; }
        public string Summary { get; set; }
    }
}
