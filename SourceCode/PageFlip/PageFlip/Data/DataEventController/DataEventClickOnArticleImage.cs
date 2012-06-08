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
using DataManager.DataEventController;
using DataManager;

namespace PageFlip.Data.DataEventController
{
    public class DataEventClickOnArticleImage : IDataEvent
    {
        public void BeforeAnimation(ref BookData Data)
        {
            string str = "/PageFlip;component/Images/HomeMenuPage/home_02.jpg";




            Data.TurnTypeManager = TurnType.ClickedImage;
            Data._previousPageLeftPart = null;
            Data._previousPageRightPart = null;

            Data._currentPageLeftPage = null;
            Data._currentPageRightPage = null;

            Data._nextPageLeftPart = null;
            Data._nextPageRightPart = null;

            Data.PreNextPageLeftPart = null;
            Data.PreNextPageRightPart = null;

            string xaml = @"
<Button 
xmlns='http://schemas.microsoft.com/client/2007'
xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'
>
            <Image Source='{0}'></Image>
        </Button>
";
            xaml = string.Format(xaml, str);//Data.FirstCoverImageSource);
            Button bt = (Button)Ultis.LoadXamlFromString(xaml);
            bt.Style = App.Current.Resources["customButtonNoStyle"] as Style;

            bt.Click += new RoutedEventHandler(bt_Click);

            Data._currentPageRightPage = bt;
        }

        void bt_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("bt_Click: DataEventInitFirstCoverPage");
        }

        public void AfterAnimation(ref BookData Data)
        {
        }
    }
}
