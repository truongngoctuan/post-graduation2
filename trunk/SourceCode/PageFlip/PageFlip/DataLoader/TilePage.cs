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
using System.Collections.Generic;

namespace PageFlip.DataLoader
{
   
    public class TilePage
    {//generate a page from multiple tiles
        //tiles in this case mean chapter thumnails
        public virtual UIElement generatePage()
        {
            return new Button() { Width = 100, Height = 25, Content = "Test Tile" };
        }
    }


    public class TilePageMenu : TilePage
    {
        //public List<Article> Articles;
        public List<Tile> Tiles;

        //        public UIElement CreateMenuPageFromTiles()
        //        {
        //            if (Index == 0)
        //            {
        //                string xaml = @"<Grid x:Name=""pageRoot"" 
        //								xmlns='http://schemas.microsoft.com/client/2007'
        //								xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'
        //							Width='900' Height='620'>
        //								<Image Source=""/PageFlip;component/Images/Chap0.jpg""></Image>
        //							</Grid>";

        //                return Ultis.LoadXamlFromString(xaml);
        //            }
        //            else
        //                if (Index == 1)
        //                {
        //                    string xaml = @"<Grid x:Name=""pageRoot"" 
        //								xmlns='http://schemas.microsoft.com/client/2007'
        //								xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'
        //							Width='900' Height='620'>
        //								<Image Source=""/PageFlip;component/Images/Chap1.jpg""></Image>
        //							</Grid>";

        //                    return Ultis.LoadXamlFromString(xaml);
        //                }
        //                else
        //                {
        //                    string xaml = @"<Grid x:Name=""pageRoot"" 
        //								xmlns='http://schemas.microsoft.com/client/2007'
        //								xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'
        //							Width='900' Height='620'>
        //								 <Image Source=""/PageFlip;component/Images/Chap2.jpg""></Image>
        //							</Grid>";

        //                    return Ultis.LoadXamlFromString(xaml);
        //                }

        //        }

        public TilePageMenu()
        {
            Tiles = new List<Tile>();
        }
        public static TilePage Load(int idx)
        {
            TilePageMenu pg = new TilePageMenu();

            for (int i = 0; i < 4; i++)
            {
                pg.Tiles.Add(new TileMenu());
            }

            return pg;
        }

        public override UIElement generatePage()
        {
            StackPanel grd = new StackPanel();
            grd.Orientation = Orientation.Horizontal;
            foreach (Tile item in Tiles)
            {
                grd.Children.Add(item.generate());
            }

            return grd;
        }
    }
}
