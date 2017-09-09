using System.Collections.Generic;

namespace ApiModel
{
    /// <summary>
    /// Desribed with T
    /// </summary>
    public class DataType
    {
        public string Name { get; set; }
        public string Summary { get; set; }
        public List<DataProperty> Properties { get; set; } 
    }
}
