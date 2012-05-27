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
        public string ImageSource;//...
        public int GridColumn, GridRow, GridColumnSpan, GridRowSpan;
        public int Page;
        //margin
        //border
        public virtual UIElement generate()
        {
            return new Button() { Width = 100, Height = 25, Content = "Test Tile" };
        }
    }

    public class TileMenu : Tile
    {//tile when click vao open new menu
        //use 2 variable to indicate position of his item in global menu
        //to load new sub menu.
        public int CurrentLvl;
        public int CurrentIndexMenu;
        public override UIElement generate()
        {
            string xaml = @"
<Button 
xmlns='http://schemas.microsoft.com/client/2007'
xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'
Grid.Row='{0}' Grid.Column='{1}' Grid.ColumnSpan='{2}' Grid.RowSpan='{3}'>
            <Image Source='{4}'></Image>
        </Button>
";
            xaml = string.Format(xaml, GridRow, GridColumn, GridColumnSpan, GridRowSpan, ImageSource);
            Button bt = (Button)Ultis.LoadXamlFromString(xaml);
            bt.Click += new RoutedEventHandler(bt_Click);

            return bt;
        }

        void bt_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("bt_Click " + CurrentLvl.ToString() + " " + CurrentIndexMenu.ToString());
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
