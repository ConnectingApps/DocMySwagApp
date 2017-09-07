using System.IO;
using Xunit;

namespace DocMySwagApp.UnitTest
{
    public class CommandLineArgumentReaderTest : TestBase
    {
        [Fact]
        public void IsValidTest()
        {
            var inputarguments = new[] 
                {$"i={Path.Combine(InputTestDir, "SwaggerFile.xml")}"
                ,$"o={Path.Combine(OutputTestDir, "SwaggerFile.docx")}",
                "t=docx"    
                };
            string[] fileTypes = {"docx"};
            var reader = new CommandLineArgumentReader(inputarguments, fileTypes);
            var valid = reader.IsValid(out string explanation);
            var readerData = reader.GetArgumentPropertiesIfValidated();
            Assert.True(valid);
            Assert.Null(explanation);
            Assert.Equal("docx", readerData.FileType);
            Assert.Equal(Path.Combine(InputTestDir, "SwaggerFile.xml"), readerData.InputFileName);
            Assert.Equal(Path.Combine(OutputTestDir, "SwaggerFile.docx"), readerData.OutputFileName);
            Assert.Equal("docx", readerData.FileType);
        }

        [Fact]
        public void IsValidTestFailNoFileType()
        {
            var inputarguments = new[]
            {$"i={Path.Combine(InputTestDir, "SwaggerFile.xml")}"
                ,$"o={Path.Combine(OutputTestDir, "SwaggerFile.docx")}"
            };
            string[] fileTypes = { "docx" };
            var reader = new CommandLineArgumentReader(inputarguments, fileTypes);
            var valid = reader.IsValid(out string explanation);
            Assert.False(valid);
            Assert.NotNull(explanation);
        }

        [Fact]
        public void IsValidTestNoInputFile()
        {
            var inputarguments = new[]
            {
                $"o={Path.Combine(OutputTestDir, "SwaggerFile.docx")}",
                "t=docx"
            };
            string[] fileTypes = { "docx" };
            var reader = new CommandLineArgumentReader(inputarguments, fileTypes);
            var valid = reader.IsValid(out string explanation);
            Assert.False(valid);
            Assert.NotNull(explanation);
        }

        [Fact]
        public void IsValidTestNoOutputFile()
        {
            var inputarguments = new[]
            {$"i={Path.Combine(InputTestDir, "SwaggerFile.xml")}",
                "t=docx"
            };
            string[] fileTypes = { "docx" };
            var reader = new CommandLineArgumentReader(inputarguments, fileTypes);
            var valid = reader.IsValid(out string explanation);
            Assert.False(valid);
            Assert.NotNull(explanation);
        }

        [Fact]
        public void IsValidTestNoOutputFileExists()
        {
            var inputarguments = new[]
            {$"i={Path.Combine(InputTestDir, "SwaggerFileNotExists.xml")}"
                ,$"o={Path.Combine(OutputTestDir, "SwaggerFileNotExists.docx")}",
                "t=docx"
            };
            string[] fileTypes = { "docx" };
            var reader = new CommandLineArgumentReader(inputarguments, fileTypes);
            var valid = reader.IsValid(out string explanation);
            Assert.False(valid);
            Assert.NotNull(explanation);
        }

        [Fact]
        public void IsValidTestInvalidFile()
        {
            var inputarguments = new[]
            {$"i={Path.Combine(InputTestDir, "SwaggerFileInvalid.xml")}"
                ,$"o={Path.Combine(OutputTestDir, "SwaggerFile.docx")}",
                "t=docx"
            };
            string[] fileTypes = { "docx" };
            var reader = new CommandLineArgumentReader(inputarguments, fileTypes);
            var valid = reader.IsValid(out string explanation);
            Assert.False(valid);
            Assert.NotNull(explanation);
        }
    }
}
