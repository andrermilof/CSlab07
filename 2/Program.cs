using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using lab07;

namespace _2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            XDocument xDocument = new XDocument();

            Assembly assembly = Assembly.Load(new AssemblyName("lab07"));

            Type[] types = assembly.GetTypes();
            XElement[] xElements = new XElement[types.Length];

            XElement fields, props, methods, attrs;
            BindingFlags flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly;
            for (int i = 0; i < types.Length; i++)
            {
                xElements[i] = new XElement(types[i].Name);

                Attribute[] at = Attribute.GetCustomAttributes(types[i]);
                attrs = new XElement("attributes");
                foreach(var a in at)
                {
                    Comment cm;
                    if (a is Comment)
                    {
                        cm = (Comment)a;
                        attrs.Add(cm.com + " ");
                    }
                }
                xElements[i].Add(attrs);

                if (types[i].IsClass)
                {
                    if (types[i].IsAbstract)
                        xElements[i].Add(new XAttribute("type", "abstract"));

                    for (int j = 0; j < types.Length; j++)
                        if (types[i].IsSubclassOf(types[j]))
                        {
                            xElements[i].Add(new XAttribute("type", $"subclass of {types[j].Name}"));
                            break;
                        }
                   
                    FieldInfo[] fieldinfo = types[i].GetFields(flags);
                    PropertyInfo[] propinfo = types[i].GetProperties(flags);
                    MethodInfo[] methinfo = types[i].GetMethods(flags);
                    
                    if (fieldinfo.Length != 0)
                    {
                        fields = new XElement("fields");
                        foreach (var field in fieldinfo)
                            fields.Add(field.Name + ' ');

                        xElements[i].Add(fields);
                    }

                    if (propinfo.Length != 0)
                    {
                        props = new XElement("properties");
                        foreach (var prop in propinfo)
                            props.Add(prop.Name + ' ');

                        xElements[i].Add(props);
                    }

                    if (methinfo.Length != 0)
                    {
                        methods = new XElement("methods");
                        foreach (var prop in methinfo)
                            methods.Add(prop.Name + ' ');

                        xElements[i].Add(methods);
                    }
                }

                if (types[i].IsEnum)
                {
                    xElements[i].Add(new XAttribute("type", "enum"));
                    FieldInfo[] fieldinfo = types[i].GetFields();
                    fields = new XElement("fields");
                    foreach (var field in fieldinfo)
                        fields.Add(field.Name + ' ');

                    xElements[i].Add(fields);
                }
            }

            XElement all = new XElement("lab07");
            foreach(var el in xElements)
                all.Add(el);

            xDocument.Add(all);
            xDocument.Save("all.xml");
        }
    }
}
