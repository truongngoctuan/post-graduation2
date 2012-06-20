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

namespace PageFlip.Animation.UISlider
{
    public class SelectedItemChanged : EventArgs
    {
        public int selectedItem;
        public SelectedItemChanged(int selectedIndex)
        {
            this.selectedItem = selectedIndex;
        }
    }
}
