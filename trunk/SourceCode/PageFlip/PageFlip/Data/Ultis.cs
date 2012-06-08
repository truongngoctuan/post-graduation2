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

namespace DataManager
{
    public static class Ultis
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

        public static UIElement LoadXamlFromString(string xaml)
        {
            //            XDocument doc = XDocument.Load(s);
            //OutputTextBlock.Text = doc.ToString(SaveOptions.OmitDuplicateNamespaces);
            //string xaml = doc.ToString(SaveOptions.OmitDuplicateNamespaces);

            return System.Windows.Markup.XamlReader.Load(xaml) as UIElement;
        }

        public static T FindVisualChildByName<T>(DependencyObject parent, string name) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                string controlName = child.GetValue(Control.NameProperty) as string;
                if (controlName == name)
                {
                    return child as T;
                }
                else
                {
                    T result = FindVisualChildByName<T>(child, name);
                    if (result != null)
                        return result;
                }
            }
            return null;
        }
    }
}
