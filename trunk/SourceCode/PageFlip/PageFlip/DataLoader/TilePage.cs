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

            //for (int i = 0; i < 4; i++)
            //{
            //    pg.Tiles.Add(new TileMenu());
            //}

            pg.Tiles.Add(new TileMenu()
            {
                xamlImage = @"
<Image xmlns='http://schemas.microsoft.com/client/2007'
    xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'
	Grid.Row='0' Grid.Column='0' Grid.ColumnSpan='2' Grid.RowSpan='5' Width='628' Height='646' Source='/PageFlip;component/Images/HomeMenuPage/home_01.jpg'></Image>
"
            });

            pg.Tiles.Add(new TileMenu()
            {
                xamlImage = @"
<Image xmlns='http://schemas.microsoft.com/client/2007'
    xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'
	Grid.Row='0' Grid.Column='2' Grid.ColumnSpan='1' Grid.RowSpan='3' Width='297' Height='359' Source='/PageFlip;component/Images/HomeMenuPage/home_02.jpg'></Image>
"
            });

            pg.Tiles.Add(new TileMenu()
            {
                xamlImage = @"
<Image xmlns='http://schemas.microsoft.com/client/2007'
    xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'
	Grid.Row='0' Grid.Column='3' Grid.ColumnSpan='1' Grid.RowSpan='2' Width='287' Height='219' Source='/PageFlip;component/Images/HomeMenuPage/home_03.jpg'></Image>
"
            });

            pg.Tiles.Add(new TileMenu()
            {
                xamlImage = @"
<Image xmlns='http://schemas.microsoft.com/client/2007'
    xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'
	Grid.Row='3' Grid.Column='2' Grid.ColumnSpan='1' Grid.RowSpan='2' Width='296' Height='232' Source='/PageFlip;component/Images/HomeMenuPage/home_04.jpg'></Image>
"
            });

            pg.Tiles.Add(new TileMenu()
            {
                xamlImage = @"
<Image xmlns='http://schemas.microsoft.com/client/2007'
    xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'
	Grid.Row='2' Grid.Column='3' Grid.ColumnSpan='1' Grid.RowSpan='3' Width='286' Height='316' Source='/PageFlip;component/Images/HomeMenuPage/home_05.jpg'></Image>
"
            });
            return pg;
        }

        public override UIElement generatePage()
        {
            //StackPanel grd = new StackPanel();
            //grd.Orientation = Orientation.Horizontal;

            string xaml = @"
	<Grid 
xmlns='http://schemas.microsoft.com/client/2007'
    xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'
x:Name='tilepage'>
        <Grid.ColumnDefinitions>
            <ColumnDefinition Width='*'></ColumnDefinition>
            <ColumnDefinition Width='*'></ColumnDefinition>
            <ColumnDefinition Width='*'></ColumnDefinition>
            <ColumnDefinition Width='*'></ColumnDefinition>
        </Grid.ColumnDefinitions>
        <Grid.RowDefinitions>
            <RowDefinition Height='*'></RowDefinition>
            <RowDefinition Height='*'></RowDefinition>
            <RowDefinition Height='*'></RowDefinition>
            <RowDefinition Height='*'></RowDefinition>
            <RowDefinition Height='*'></RowDefinition>
        </Grid.RowDefinitions>
    </Grid>

";

            Grid grd = (Grid)PageFlipUltis.Ultis.LoadXamlFromString(xaml);
            foreach (Tile item in Tiles)
            {
                grd.Children.Add(item.generate());
            }

            return grd;
        }
    }
}
