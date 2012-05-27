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
using System.Xml;
using System.IO;

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
        public static TilePage Load(StringReader stream, int lvl, int idx)
        {
//            TilePageMenu pg = new TilePageMenu();

//            //for (int i = 0; i < 4; i++)
//            //{
//            //    pg.Tiles.Add(new TileMenu());
//            //}

//            pg.Tiles.Add(new TileMenu()
//            {
//                xamlImage = @"
//<Image xmlns='http://schemas.microsoft.com/client/2007'
//    xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'
//	Grid.Row='0' Grid.Column='0' Grid.ColumnSpan='2' Grid.RowSpan='5' Stretch='Fill' Source='/PageFlip;component/Images/HomeMenuPage/home_01.jpg'></Image>
//"
//            });

//            pg.Tiles.Add(new TileMenu()
//            {
//                xamlImage = @"
//<Image xmlns='http://schemas.microsoft.com/client/2007'
//    xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'
//	Grid.Row='0' Grid.Column='2' Grid.ColumnSpan='1' Grid.RowSpan='3' Stretch='None' Source='/PageFlip;component/Images/HomeMenuPage/home_02.jpg'></Image>
//"
//            });

//            pg.Tiles.Add(new TileMenu()
//            {
//                xamlImage = @"
//<Image xmlns='http://schemas.microsoft.com/client/2007'
//    xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'
//	Grid.Row='0' Grid.Column='3' Grid.ColumnSpan='1' Grid.RowSpan='2' Stretch='None' Source='/PageFlip;component/Images/HomeMenuPage/home_03.jpg'></Image>
//"
//            });

//            pg.Tiles.Add(new TileMenu()
//            {
//                xamlImage = @"
//<Image xmlns='http://schemas.microsoft.com/client/2007'
//    xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'
//	Grid.Row='3' Grid.Column='2' Grid.ColumnSpan='1' Grid.RowSpan='2' Stretch='None' Source='/PageFlip;component/Images/HomeMenuPage/home_04.jpg'></Image>
//"
//            });

//            pg.Tiles.Add(new TileMenu()
//            {
//                xamlImage = @"
//<Image xmlns='http://schemas.microsoft.com/client/2007'
//    xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'
//	Grid.Row='2' Grid.Column='3' Grid.ColumnSpan='1' Grid.RowSpan='3' Stretch='None' Source='/PageFlip;component/Images/HomeMenuPage/home_05.jpg'></Image>
//"
//            });
//            return pg;

            TilePageMenu pg = new TilePageMenu();
            XmlReader reader = XmlReader.Create(stream);

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    if (reader.Name == "menu") continue;
                    if (reader.Name == "tile")
                    {
                        TileMenu tile = new TileMenu();
                        if (true == reader.MoveToFirstAttribute())
                        {
                            //Page='0' GridRow='0' GridColumn='0' 
                            //GridColumnSpan='2' GridRowSpan='5' 
                            //ImageSource='/PageFlip;component/Images/HomeMenuPage/home_01.jpg'
                            tile.Page = int.Parse(reader.Value);
                            reader.MoveToNextAttribute();

                            tile.GridRow = int.Parse(reader.Value);
                            reader.MoveToNextAttribute();
                            tile.GridColumn = int.Parse(reader.Value);
                            reader.MoveToNextAttribute();

                            tile.GridColumnSpan = int.Parse(reader.Value);
                            reader.MoveToNextAttribute();
                            tile.GridRowSpan = int.Parse(reader.Value);
                            reader.MoveToNextAttribute();

                            tile.ImageSource = reader.Value;
                        }

                        pg.Tiles.Add(tile);
                    }
                }
            }            
            return pg;
        }

        public override UIElement generatePage()
        {
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
                //if (item.Page == iPage)
                grd.Children.Add(item.generate());
            }

            return grd;
        }


        public static UIElement createPage(List<TileMenu> Tiles, int iPage)
        {
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

            int iCounter = 0;
            Grid grd = (Grid)PageFlipUltis.Ultis.LoadXamlFromString(xaml);
            foreach (Tile item in Tiles)
            {
                if (item.Page == iPage)
                {
                    grd.Children.Add(item.generate());
                    iCounter++;
                }
            }

            if (iCounter > 0)
                return grd;
            else
                return null;
        }
    }
}
