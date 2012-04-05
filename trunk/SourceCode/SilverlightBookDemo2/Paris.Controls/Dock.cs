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
    public enum Dock : byte
    {
        Bottom = 3,
        Left = 0,
        Right = 1,
        Top = 2
    }

    public enum PageFoldAction
    {
        TurnPageOnClick,
        TurnPageOnDoubleClick,
        None
    }



    public enum PageFoldVisibility
    {
        OnMouseOver,
        Never,
        Always
    }

 


    public enum BookZone
    {
        Out,
        BottomLeft,
        TopLeft,
        Center,
        TopRight,
        BottomRight
    }

 

}
