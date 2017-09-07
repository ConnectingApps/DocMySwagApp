using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Xunit;

namespace ApiModel.UnitTest
{
    public class SwaggerXmlReaderTest
    {
        [Fact]
        public void TestWithValidModel()
        {
            var fileContent = File.ReadAllText("SwaggerFile.xml");
            var xdoc = XDocument.Parse(fileContent);
            var instance = new SwaggerXmlReader(xdoc.Root);
            var dataModel = instance.CreateDataModel();
            Assert.NotNull(dataModel);
            Assert.Equal(2, dataModel.ControllerClasses.Count);
            Assert.True(dataModel.ControllerClasses.Any(c => c.ControllerMethods.Count == 2 
            && c.ControllerMethods.Any(cm => cm.Returns.Contains("some string"))
            && c.ControllerMethods.Any(m => m.Name.Contains("GetOutput")
            && m.Arguments.Count == 2 && m.Arguments.Any(a => a.Name == "inputData" && a.Type.Name.Contains("InputData")
            )
            )));
            Assert.True(dataModel.ControllerClasses.Any(c => c.ControllerMethods.Count == 1
            && c.ControllerMethods.Any(m => m.Name.Contains("Get") && m.Summary.Contains("Get iets"))
            ));

            Assert.Equal(2,dataModel.DataTypes.Count);
            Assert.True(dataModel.DataTypes.Any(a => a.Name.Contains("InputData") && a.Summary.Contains("input data")));
            Assert.True(dataModel.DataTypes.Any(a => a.Name.Contains("OutputData") && 
            a.Properties.Any(p => p.Name.Contains("Id") && p.Summary.Contains("The input id") ) ));
        }

        [Fact]
        public void TestWithInvalidModel()
        {
            var fileContent = File.ReadAllText("SwaggerFile.xml").Replace("name", "namechange");
            var xdoc = XDocument.Parse(fileContent);
            var instance = new SwaggerXmlReader(xdoc.Root);
            Assert.Throws<FormatException>(() => instance.CreateDataModel());
        }
    }
}
