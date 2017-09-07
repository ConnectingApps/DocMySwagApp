using DocMySwagApp.UnitTest;
using System.IO;
using Xunit;

namespace DocMySwagApp.IntegrationTest
{
    public class ProgramTest : TestBase
    {
        [Theory]
        [InlineData("SwaggerFile.html","html")]
        // TOOD: Add something new for docx
        public void HappyPathTest(string outputFile,string type)
        {
            string inputFilePath = Path.Combine(InputTestDir, "SwaggerFile.xml");
            string outputFilePath = Path.Combine(OutputTestDir, outputFile);
            Program.Main(new []{$"i={inputFilePath}", $"o={outputFilePath}", $"t={type}"});
            Assert.True(File.Exists(outputFilePath));
        }

        [Theory]
        [InlineData("SwaggerFile.html", "html")]
        public void UnHappyPathTestInvalidFileName(string outputFile, string type)
        {
            string inputFilePath = Path.Combine(InputTestDir, "SwaggerFileInvalid.xml");
            string outputFilePath = Path.Combine(OutputTestDir, outputFile);
            Program.Main(new[] { $"i={inputFilePath}", $"o={outputFilePath}", $"t={type}" });
            Assert.True(!File.Exists(outputFilePath));
        }

        [Theory]
        [InlineData("SwaggerFile.html", "html")]
        public void UnHappyPathTestInvalidIncorrectArguments(string outputFile, string type)
        {
            string inputFilePath = Path.Combine(InputTestDir, "SwaggerFile.xml");
            string outputFilePath = Path.Combine(OutputTestDir, outputFile);
            Program.Main(new[] { $"i={inputFilePath}", $"q={outputFilePath}", $"t={type}" });
            Assert.True(!File.Exists(outputFilePath));
        }
    }
}
