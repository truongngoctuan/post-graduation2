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

namespace Paris.Controls
{
    internal static class PlatformIndependent
    {
        internal static UIElement GetRootVisual(this UIElement element)
        {
            return Application.Current.RootVisual;
        }

        internal static ControlTemplate AdjustContentControlTemplate(ControlTemplate template)
        {
            return template;
        }



 


    }
}
