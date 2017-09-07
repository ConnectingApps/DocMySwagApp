using ApiModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HtmlGeneration
{
    public class FullHtmlGenerator : IFullHtmlGenerator
    {
        private string _argumentHeader;
        private string _arguments;
        private string _assemblyDescription;
        private string _controllers;
        private string _dataModel;
        private string _finish;
        private string _methods;
        private string _properties;
        private string _methodHeader;

        public bool TryInitialize(out string explanation)
        {
            var directoryName = AppContext.BaseDirectory;
            string HtmlTemplate(string templateName) => File.ReadAllText(Path.Combine(directoryName, templateName));
            try
            {
                _argumentHeader = HtmlTemplate("ArgumentHeader.html");
                _arguments = HtmlTemplate("Arguments.html");
                _assemblyDescription = HtmlTemplate("AssemblyDescription.html");
                _controllers = HtmlTemplate("Controllers.html");
                _dataModel = HtmlTemplate("DataModel.html");
                _finish = HtmlTemplate("Finish.html");
                _methods = HtmlTemplate("Methods.html");
                _properties = HtmlTemplate("Properties.html");
                _methodHeader = HtmlTemplate("MethodHeader.html");
            }
            catch (FileNotFoundException ex)
            {
                explanation = ex.Message;
                return false;
            }
            explanation = null;
            return true;
        }

        public bool TryGenerateOutputFile(SwaggerModel swaggerModel, string outPutFile, out string explanation)
        {
            if (swaggerModel == null)
            {
                explanation = "No data model received";
                return false;
            }
            if (String.IsNullOrEmpty(outPutFile))
            {
                explanation = "No output file received";
                return false;
            }
            var directory = Path.GetDirectoryName(outPutFile);
            if (String.IsNullOrEmpty(directory))
            {
                explanation = $"The directory of outputfile {outPutFile} is not valid";
                return false;
            }
            var htmlInstances = TryGenerateTemplateInstances(swaggerModel, out explanation);
            if (htmlInstances == null)
            {
                return false;
            }
            var fullContent = String.Join('\n', htmlInstances.Select(h => h.GenerateHtml()));
            try
            {
                File.WriteAllText(outPutFile,fullContent);
            }
            catch (Exception e)
            {
                explanation = $"Error while writing file {outPutFile}: {e.Message}";
                return false;
            }
            explanation = null;
            return true;
        }

        private IList<IHtmlTemplate> TryGenerateTemplateInstances(SwaggerModel swaggerModel, out string explanation)
        {
            List<IHtmlTemplate> htmlTemplateInstances = new List<IHtmlTemplate>();
            var assemblyDescription = new AssemlyDescription(_assemblyDescription, swaggerModel.AssemblyName);
            if (!assemblyDescription.IsValid(out explanation))
            {
                return null;
            }
            htmlTemplateInstances.Add(assemblyDescription);
            var controllerTemplateInstances = TryGenerateControllerTemplateInstances(out explanation, swaggerModel.ControllerClasses);
            if (controllerTemplateInstances == null)
            {
                return null;
            }
            htmlTemplateInstances.AddRange(controllerTemplateInstances);
            var dataModelInstances = TryGenerateDataModelTemplateInstance(out explanation, swaggerModel.DataTypes);
            if (dataModelInstances == null)
            {
                return null;
            }
            htmlTemplateInstances.AddRange(dataModelInstances);
            explanation = null;
            return htmlTemplateInstances;
        }

        private IList<IHtmlTemplate> TryGenerateControllerTemplateInstances(out string explanation, IList<ControllerClass> controllerClasses)
        {
            List<IHtmlTemplate> htmlTemplateInstances = new List<IHtmlTemplate>();
            var controllerTemplateInstances = (from c in controllerClasses
                select new Controllers(_controllers, c.Name) as IHtmlTemplate).ToList();
            if (!controllerTemplateInstances.Any())
            {
                explanation = null;
                return controllerTemplateInstances;
            }
            if (!controllerTemplateInstances.First().IsValid(out explanation))
            {
                return null;
            }

            for (int i = 0; i < controllerTemplateInstances.Count; i++)
            {
                htmlTemplateInstances.Add(controllerTemplateInstances[i]);
                var methodHtmlTemplateInstance = 
                    TryGenerateMethodTemplateInstances(out explanation, controllerClasses[i].ControllerMethods);
                if (methodHtmlTemplateInstance == null)
                {
                    return null;
                }
                htmlTemplateInstances.AddRange(methodHtmlTemplateInstance);
            }
            explanation = null;
            return htmlTemplateInstances;
        }

        private IEnumerable<IHtmlTemplate> TryGenerateMethodTemplateInstances(out string explanation, IList<ControllerMethod> controllerMethods)
        {
            List<IHtmlTemplate> htmlTemplates = new List<IHtmlTemplate>();
            var methodHeader = new NoDataHtml(_methodHeader);
            if (!methodHeader.IsValid(out explanation))
            {
                return null;
            }
            if (controllerMethods == null)
            {
                return htmlTemplates;
            }
            foreach (var method in controllerMethods)
            {
                htmlTemplates.Add(methodHeader);
                var methods = new Methods(_methods,new [] { method });
                if (!methods.IsValid(out explanation))
                {
                    return null;
                }
                htmlTemplates.Add(methods);
                var argumentElements = TryGenerateArgumenTemplateInstance(out explanation, method.Arguments);
                if (argumentElements == null)
                {
                    return null;
                }
                
                htmlTemplates.AddRange(argumentElements);
            }
            explanation = null;
            return htmlTemplates;
        }

        private IEnumerable<IHtmlTemplate> TryGenerateArgumenTemplateInstance(out string explanation, IList<Argument> arguments)
        {
            List<IHtmlTemplate> htmlTemplates = new List<IHtmlTemplate>();
            var argumentHeader = new NoDataHtml(_argumentHeader);
            if (!argumentHeader.IsValid(out explanation))
            {
                return null;
            }
            var argumentHtmlInstance = new Arguments(_arguments, arguments);
            if (!argumentHtmlInstance.IsValid(out explanation))
            {
                return null;
            }
            htmlTemplates.Add(argumentHeader);
            htmlTemplates.Add(argumentHtmlInstance);
            htmlTemplates.Add(new NoDataHtml(_finish));
            return htmlTemplates;
        }

        private IEnumerable<IHtmlTemplate> TryGenerateDataModelTemplateInstance(out string explanation, IList<DataType> dataTypes)
        {
            List<IHtmlTemplate> htmlTemplates = new List<IHtmlTemplate>();
            foreach (var dataType in dataTypes)
            {
                var dataModelHtmlInstance = new DataModel(_dataModel,dataType.Name,dataType.Summary);
                if (!dataModelHtmlInstance.IsValid(out explanation))
                {
                    return null;
                }
                htmlTemplates.Add(dataModelHtmlInstance);
                var properties = new Properties(_properties, dataType.Properties);
                if (!properties.IsValid(out explanation))
                {
                    return null;
                }
                htmlTemplates.Add(properties);
                htmlTemplates.Add(new NoDataHtml(_finish));
            }
            explanation = null;
            return htmlTemplates;
        }
    }
}
