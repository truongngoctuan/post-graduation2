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
            //string str = "/PageFlip;component/Images/HomeMenuPage/home_02.jpg";




            Data.TurnTypeManager = TurnType.ClickedImage;

//            Data._previousPageLeftPart = null;
//            Data._previousPageRightPart = null;

//            Data._currentPageLeftPage = null;
//            Data._currentPageRightPage = null;

//            Data._nextPageLeftPart = null;
//            Data._nextPageRightPart = null;

//            Data.PreNextPageLeftPart = null;
//            Data.PreNextPageRightPart = null;

//            #region create grid
//            int NGridColumns = 1;
//            int NGridRows = 1;

//            string xamlColumns = string.Empty;
//            for (int i = 0; i < NGridColumns; i++)
//            {
//                xamlColumns += "<ColumnDefinition Width='*'></ColumnDefinition>\r\n";
//            }

//            string xamlRows = string.Empty;
//            for (int i = 0; i < NGridRows; i++)
//            {
//                xamlRows += "<RowDefinition Height='*'></RowDefinition>\r\n";
//            }

//            string xaml = string.Format(@"
//<Grid 
//xmlns='http://schemas.microsoft.com/client/2007'
//    xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'
//Background='White'>
//        <Grid.ColumnDefinitions>
//            {0}
//        </Grid.ColumnDefinitions>
//        <Grid.RowDefinitions>
//            {1}
//        </Grid.RowDefinitions>
//    </Grid>
//", xamlColumns, xamlRows);

//            #endregion
            



//            string xaml2 = @"
//<Button 
//xmlns='http://schemas.microsoft.com/client/2007'
//xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'
//Grid.Row='0' Grid.Column='0' Grid.ColumnSpan='1' Grid.RowSpan='1'
//>
//            <Image Source='{0}'></Image>
//        </Button>
//";
            
//            xaml2 = string.Format(xaml2, str);//Data.FirstCoverImageSource);
//            Button bt = (Button)Ultis.LoadXamlFromString(xaml2);
//            //bt.Style = App.Current.Resources["customButtonNoStyle"] as Style;

//            bt.Click += new RoutedEventHandler(bt_Click);


//            Grid grd = (Grid)Ultis.LoadXamlFromString(xaml);
//            grd.Children.Add(bt);
            
//            //grd.Background = new SolidColorBrush(Colors.White);

//            Data._currentPageRightPage = grd;
        }

        //void bt_Click(object sender, RoutedEventArgs e)
        //{
        //    MessageBox.Show("bt_Click: DataEventInitFirstCoverPage");
        //}

        public void AfterAnimation(ref BookData Data)
        {
        }
    }
}
