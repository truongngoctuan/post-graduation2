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
using PageFlipUltis;

namespace PageFlip.DataLoader
{
    public class Tile
    {
        //string strImageSource;//...
        //double dWidth;
        //double dHeight;
        //margin
        //border
        //gridcol, gridrow

        public virtual UIElement generate()
        {
            return new Button() { Width = 100, Height = 25, Content = "Test Tile" };
        }
    }

    public class TileMenu : Tile
    {//tile when click vao open new menu
        public string xamlImage;
        public override UIElement generate()
        {
            return Ultis.LoadXamlFromString(xamlImage);

            //Button bt = new Button() { Width = 100, Height = 100, Content = "Test Menu Tile" };

            //bt.Click += new RoutedEventHandler(bt_Click);

            //return bt;
        }

        void bt_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("bt_Click " + " ");
        }
    }
    
    public class TileArticle : Tile
    {//tile that click open new article
        public override UIElement generate()
        {
            return base.generate();
        }
    }
}
