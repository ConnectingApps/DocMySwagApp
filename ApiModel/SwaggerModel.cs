using System;
using System.Collections.Generic;
using System.Text;

namespace ApiModel
{
    public class SwaggerModel
    {
        public string AssemblyName { get; set; }
        public List<DataType> DataTypes { get; set; }
        public List<ControllerClass> ControllerClasses { get; set; }
    }
}
