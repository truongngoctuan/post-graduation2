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

namespace DataManager.DataEventController
{
    public interface IDataEvent
    {
        void BeforeAnimation(ref BookData Data);
        void AfterAnimation(ref BookData Data);
    }
}
