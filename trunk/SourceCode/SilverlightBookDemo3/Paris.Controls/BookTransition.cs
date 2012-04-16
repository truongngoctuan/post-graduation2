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
    internal abstract class BookTransition
    {
        // Methods
        protected BookTransition()
        {
        }

        public abstract void Turn(BookItem item, bool showOuterShadows, bool showInnerShadows, Size bookSize);
    }


}
