using System;
using System.Collections.Generic;
using System.IO;
using ApiModel;
using Xunit;

namespace HtmlGeneration.UnitTest
{
    public class FullHtmlGeneratorTest
    {
        private SwaggerModel BuildSwaggerModel()
        {
            var model = new SwaggerModel
            {
                AssemblyName = "MyAssemblyName",
                DataTypes = new List<DataType>()
                {
                    new DataType
                    {
                        Name = "MyName1",
                        Summary = "Summary1",
                        Properties = new List<DataProperty>
                        {
                            new DataProperty
                            {
                                Name = "Property1",
                                Summary = "SummaryP1"
                            },
                            new DataProperty
                            {
                                Name = "Property2",
                                Summary = "SummaryP2"
                            },

                        }
                    },
                    new DataType
                    {
                        Name = "MyName2",
                        Summary = "Summary2"
                    }
                },
                ControllerClasses = new List<ControllerClass>
                {
                    new ControllerClass
                    {
                        Name = "ControllerName1",                 
                        ControllerMethods = new List<ControllerMethod>
                        {
                            new ControllerMethod
                            {
                                Name = "Method1",
                                Summary = "Summary1",
                                Returns = "Return1",
                                Arguments = new List<Argument>
                                {
                                    new Argument
                                    {
                                        Name = "ArgumentName1",
                                        Description = "Description1",
                                        Type = new DataType
                                        {
                                            Name = "System.String"
                                        }             
                                    },
                                    new Argument
                                    {
                                        Name = "ArgumentName2",
                                        Description = "Description2",
                                        Type = new DataType
                                        {
                                            Name = "System.int"
                                        }
                                    }
                                }                            
                            },
                            new ControllerMethod
                            {
                                Name = "Method2",
                                Summary = "Summary2",
                                Returns = "Return2",
                                Arguments = new List<Argument>
                                {
                                    new Argument
                                    {
                                        Name = "2ArgumentName1",
                                        Description = "2Description1",
                                        Type = new DataType
                                        {
                                            Name = "System.Bool"
                                        }
                                    },
                                    new Argument
                                    {
                                        Name = "2ArgumentName2",
                                        Description = "2Description2",
                                        Type = new DataType
                                        {
                                            Name = "System.Int64"
                                        }
                                    }
                                }
                            }
                        }
                    },
                    new ControllerClass
                    {
                        Name = "ControllerName2"
                    },
                }
                
            };
            return model;
        }

        [Fact]
        public void InitalizeTest()
        {
            var testInstance = new FullHtmlGenerator();
            var actual = testInstance.TryInitialize(out string explanation);
            Assert.Null(explanation);
            Assert.True(actual);        
        }

        [Fact]
        public void GenerateTest()
        {
            var dataModel = BuildSwaggerModel();
            var testInstance = new FullHtmlGenerator();
            string outPutFile = Path.Combine(AppContext.BaseDirectory, $"{Guid.NewGuid().ToString()}.html");
            testInstance.TryInitialize(out string _);
            var actual = testInstance.TryGenerateOutputFile(dataModel, outPutFile, out string eplanationGeneration);
            Assert.Null(eplanationGeneration);
            Assert.True(actual);
            Assert.True(File.Exists(outPutFile));
        }


    }
}
