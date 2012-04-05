using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;
using System.Xml;

namespace SilverlightBookDemo2
{
    public static class Utils
    {
        //public static UIElement cloneElement(UIElement orig)
        //{

        //    if (orig == null)

        //        return (null);

        //    string s = XamlWriter.Save(orig);

        //    StringReader stringReader = new StringReader(s);

        //    XmlReader xmlReader = XmlTextReader.Create(stringReader, new XmlReaderSettings());

        //    return (UIElement)XamlReader.Load(xmlReader);

        //}

        ////dll reference to System.Runtime.Serialization
        //public static T DeepCopy<T>(this T oSource)
        //{
        //    T oClone;

        //    System.Runtime.Serialization.DataContractSerializer dcs = new System.Runtime.Serialization.DataContractSerializer(typeof(T));

        //    using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
        //    {
        //        dcs.WriteObject(ms, oSource);
        //        ms.Position = 0;
        //        oClone = (T)dcs.ReadObject(ms);
        //    }

        //    return oClone;
        //}

        public static List<UIElement> LoadXamlFromStream(System.IO.Stream s)
        {
            List<UIElement> listEles = new List<UIElement>();

            System.Xml.XmlReader reader = System.Xml.XmlReader.Create(s);

            //if (reader.Name == "Items")
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.Name != "Items")
                    {
                        //MessageBox.Show(reader.ReadOuterXml());
                        if (reader.Name == "SilverlightControl1")
                        {
                            Type calcType = testAssembly.GetType("Test.Calculator");

                            // create instance of class Calculator
                            object calcInstance = Activator.CreateInstance(calcType);


                            // Load a particular assembly from the XAP package
                            Assembly a = GetAssemblyFromPackage(assemblyName, e.Result);

                            // Get an instance of the XAML object
                            object page = a.CreateInstance(className);
                        }
                        else
                        {
                            listEles.Add(System.Windows.Markup.XamlReader.Load(reader.ReadOuterXml()) as UIElement);
                        }
                    }
                }

            }

            return listEles;
        }

        ////system.xml.linq
        //public static UIElement LoadXamlFromStream(System.IO.Stream s)
        //{
        //    System.Xml.Linq.XDocument doc = System.Xml.Linq.XDocument.Load(s);
        //    string xaml = doc.ToString(System.Xml.Linq.SaveOptions.OmitDuplicateNamespaces);

        //    return System.Windows.Markup.XamlReader.Load(xaml) as UIElement;
        //}
    }
}
