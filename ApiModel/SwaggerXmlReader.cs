using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ApiModel
{
    public class SwaggerXmlReader
    {
        private readonly XElement _rootElement;

        public SwaggerXmlReader(XElement rootElement)
        {
            _rootElement = rootElement;
        }

        public SwaggerModel CreateDataModel()
        {
            try
            {
                return ReadData();
            }
            catch (NullReferenceException e)
            {
                throw new FormatException("Invalid data model", e);
            }
        }

        private SwaggerModel ReadData()
        {
            var model = new SwaggerModel();
            model.AssemblyName = _rootElement.Element("assembly").Element("name").Value;
            var members = _rootElement.Element("members").Elements("member").ToList();

            List<XElement> FindSpecificMembers(string beginningMemberName) =>
                members.Where(a => a.Attribute("name").Value.StartsWith(beginningMemberName)).ToList();

            var dataTypes = FindSpecificMembers("T:");
            var properties = FindSpecificMembers("P:");
            var methods = FindSpecificMembers("M:");

            model.DataTypes = (from d in dataTypes
                let dataTypeName = d.Attribute("name").Value.Substring(2)
                select new DataType
                {
                    Name = dataTypeName,
                    Summary = d.Element("summary").Value,
                    Properties = (from p in properties
                                  let propertyName = p.Attribute("name").Value.Substring(2)
                                  where propertyName.Contains(dataTypeName) 
                                  && propertyName.Split('.').Length -1 == dataTypeName.Split('.').Length
                                  select new DataProperty
                                  {
                                     Name = propertyName,
                                     Summary = p.Element("summary").Value
                                  }).ToList()
                }).ToList();

            var controllerNames = (from m in methods
                                   let names = m.Attribute("name").Value.Split('(')[0].Split('.')
                                   select String.Join(".",names.Take(names.Length-1)).Substring(2)).Distinct().ToList();

            List<Argument> FindArgumentsInMethodElement(XElement el)
            {
                if (!el.Attribute("name").Value.Contains('('))
                {
                    return new List<Argument>();
                }

                var dataTypeNames = el.Attribute("name").Value.Split('(')[1].Split(')')[0].Split(',');
                var results = (from dataTypeName in dataTypeNames
                              select new Argument
                              {
                                  Name = null, // to find later
                                  Type = model.DataTypes.FirstOrDefault(a => a.Name == dataTypeName) ??
                                  new DataType
                                  {
                                      Name = dataTypeName
                                  }
                              }).ToList();

                var argumentNames = (from argel in el.Elements("param")
                                     select argel.Attribute("name").Value).ToList();

                var arguments = (from argel in el.Elements("param")
                    select new
                    {
                        Name = argel.Attribute("name").Value,
                        Description = argel.Value
                    }).ToList();

                int count = arguments.Count > results.Count ? results.Count : arguments.Count;
                for (int i = 0; i < count; i++)
                {
                    results[i].Name = arguments[i].Name;
                    results[i].Description = arguments[i].Description;
                }
                return results;
            }

            model.ControllerClasses = (from c in controllerNames
                select new ControllerClass
                {
                    Name = c,
                    ControllerMethods = (from m in methods
                                         let normalName = m.Attribute("name").Value.Substring(2)
                                         where normalName.Contains(c) && normalName.Split('(')[0].Split('.').Length == c.Split('.').Length + 1
                                         /*&& !controllerNames.Any(cc => cc.Split('(')[0].Length > c.Split('(')[0].Length 
                                          && cc.Split('(')[0].Contains(normalName))*/
                                         select new ControllerMethod
                                         {
                                             Summary = m.Elements("summary").First().Value,
                                             Returns = m.Elements("returns").First().Value,
                                             Name = normalName,
                                             Arguments = FindArgumentsInMethodElement(m)
                                         }).ToList()
                }).ToList();

            return model;
        }
    }
}
