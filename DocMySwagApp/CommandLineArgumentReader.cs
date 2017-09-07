using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace DocMySwagApp
{
    public class CommandLineArgumentReader
    {
        private readonly string[] _commandlineArguments;
        private readonly string[] _supportedFileFormats;
        private CommandlineArguments _argumentProperties;

        public CommandLineArgumentReader(string[] commandLineArguments, string[] supportedFileFormats)
        {
            _commandlineArguments = commandLineArguments;
            _supportedFileFormats = supportedFileFormats;
        }

        public ICommandlineArguments GetArgumentPropertiesIfValidated()
        {
            return _argumentProperties;
        }

        public bool IsValid(out string explanation)
        {
            explanation = null;
            if (_commandlineArguments == null || _commandlineArguments.Length != 3)
            {
                explanation = "3 Arguments expected!";
                return false;
            }

            if (_commandlineArguments.Any(a => a.Split('=').Count()!=2 || !(a.ToCharArray().Count(b => b == '=') == 1)) || _commandlineArguments.Any(c => c.Length < 3))
            {
                explanation = "One Assignment (=) for each argument expected";
                return false;
            }

            if (_commandlineArguments.Any(a => a.Split('=')[0].Length != 1 || !"iot".Contains(a.Split('=')[0])))
            {
                explanation = "Only i,o and t are valid arguments";
                return false;
            }

            _argumentProperties = new CommandlineArguments();
            _argumentProperties.FileType = _commandlineArguments.FirstOrDefault(a => a.Split('=')[0] == "t").Split('=')[1];
            _argumentProperties.InputFileName = _commandlineArguments.FirstOrDefault(a => a.Split('=')[0] == "i").Split('=')[1];
            _argumentProperties.OutputFileName = _commandlineArguments.FirstOrDefault(a => a.Split('=')[0] == "o").Split('=')[1];

            if (_argumentProperties.FileType == null || _argumentProperties.InputFileName == null || _argumentProperties.OutputFileName == null)
            {
                explanation = "Not all required properties (i,o and t) have been assigned.";
                return false;
            }

            if (!ValidateProperties(out explanation))
            {
                return false;
            }
            return true;
        }

        private bool ValidateProperties(out string explanation)
        {
            if (!FileTypeIsValid(_argumentProperties.FileType))
            {
                explanation = $"Filetype {_argumentProperties.FileType} is not valid.";
                return false;
            }
            if (!InputFileIsValid(out explanation, _argumentProperties.InputFileName))
            {
                return false;
            }
            if (!OutputFileIsValid(out explanation, _argumentProperties.OutputFileName))
            {
                return false;
            }
            return true;
        }

        private bool FileTypeIsValid(string fileType)
        {
            if (_supportedFileFormats.Contains(fileType))
            {
                return true;
            }
            return false;
        }

        private bool InputFileIsValid(out string explanation, string filePath)
        {
            explanation = null;
            if (!File.Exists(filePath))
            {
                explanation = $"File {filePath} does not exist";
                return false;
            }
            string content = File.ReadAllText(filePath);
            try
            {
                XDocument.Parse(content);
            }
            catch (Exception ex)
            {
                explanation = "Error while parsing input file: " + ex.Message;
                return false;
            }
            return true;
        }

        private bool OutputFileIsValid(out string explanation, string filePath)
        {
            string directoryName;
            explanation = null;
            try
            {
                // https://stackoverflow.com/a/674495/1987258
                directoryName = new FileInfo(filePath).Directory.FullName;
            }
            catch (Exception ex)
            {
                explanation = $"Error while finding directory of {filePath}: {ex.Message}";
                return false;
            }
            if (!Directory.Exists(directoryName))
            {
                return false;
            }
            return true;
        }



    }
}
