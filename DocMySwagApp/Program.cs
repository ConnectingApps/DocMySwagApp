using ApiModel;
using DocMySwagApp.Properties;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace DocMySwagApp
{
    class Program
    {
        /// <summary>
        /// All the document generators
        /// To support a new document type (e.g. docx), just add it to the array
        /// Logically, at first make an implementation of IDocumentGenerator in a new project
        /// </summary>
        private static readonly IDocumentGenerator[] DocumentGenerators = { HtmlGeneration.DocumentGeneratorFactory.Create()};

        internal static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                GiveIntrodocutionDescription();
                DescribeAllDocumentGenerators();
                return;
            }
            var documentTypes = DocumentGenerators.Select(d => d.FileType).ToArray();
            var reader = new CommandLineArgumentReader(args, documentTypes);
            if (!reader.IsValid(out string explanation))
            {
                WriteErrorLine("Invalid arguments");
                WriteErrorLine(explanation);
                GiveIntrodocutionDescription();
                DescribeAllDocumentGenerators();
                return;
            }
            Debug.Assert(explanation == null);
            var argumentProperties = reader.GetArgumentPropertiesIfValidated();
            var documentGeneratorToUse = DocumentGenerators.First(d => d.FileType == argumentProperties.FileType);

            if (!documentGeneratorToUse.TryInitialize(out string nextExplanation))
            {
                WriteErrorLine("Initialisation error");
                WriteErrorLine(nextExplanation);
                return;
            }
            Debug.Assert(nextExplanation == null);

            SwaggerModel swaggerModel = GetSwaggerModel(argumentProperties.InputFileName);
            if (swaggerModel == null)
            {
                return;
            }

            if (!documentGeneratorToUse.TryGenerateOutputFile(swaggerModel, argumentProperties.OutputFileName, out string generationIssue))
            {
                WriteErrorLine("Generation error");
                WriteErrorLine(generationIssue);
                return;
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("-----------------------------------------------------------");
            Console.WriteLine($"Successfully created {argumentProperties.OutputFileName}");
            Console.ResetColor();
        }

        private static SwaggerModel GetSwaggerModel(string filePath)
        {
            var fileContent = File.ReadAllText(filePath);
            var xdoc = XDocument.Parse(fileContent);
            var reader = new SwaggerXmlReader(xdoc.Root);
            try
            {
                return reader.CreateDataModel();
            }
            catch (FormatException e)
            {
                WriteErrorLine(e.Message);
                return null;
            }
        }

        private static void WriteErrorLine(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        private static void GiveIntrodocutionDescription()
        {
            var intro = Resources.Intro.Replace('$', '\n');
            Console.WriteLine(intro);
        }

        private static void DescribeAllDocumentGenerators()
        {
            foreach (var docGenerator in DocumentGenerators)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(docGenerator.FileType);
                Console.ResetColor();
                Console.WriteLine(docGenerator.Description);
            }
        }
    }
}
