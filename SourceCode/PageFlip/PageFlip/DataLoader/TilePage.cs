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
   
    public abstract class TilePage
    {//generate a page from multiple tiles
        //tiles in this case mean chapter thumnails
        public virtual UIElement generatePage(int iPage)
        {
            return new Button() { Width = 100, Height = 25, Content = "Test Tile Page " +iPage.ToString() };
        }

        public abstract void InitParams(List<object> Params);
    }
    
    public class TilePageMenu : TilePage
    {
        public List<Tile> Tiles;
        public int NGridRows;
        public int NGridColumns;

        public TilePageMenu()
        {
            Tiles = new List<Tile>();
        }

        public override UIElement generatePage(int iPage)
        {
//            string xaml = @"
//	<Grid 
//xmlns='http://schemas.microsoft.com/client/2007'
//    xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'
//x:Name='tilepage'>
//        <Grid.ColumnDefinitions>
//            <ColumnDefinition Width='*'></ColumnDefinition>
//            <ColumnDefinition Width='*'></ColumnDefinition>
//            <ColumnDefinition Width='*'></ColumnDefinition>
//            <ColumnDefinition Width='*'></ColumnDefinition>
//        </Grid.ColumnDefinitions>
//        <Grid.RowDefinitions>
//            <RowDefinition Height='*'></RowDefinition>
//            <RowDefinition Height='*'></RowDefinition>
//            <RowDefinition Height='*'></RowDefinition>
//            <RowDefinition Height='*'></RowDefinition>
//            <RowDefinition Height='*'></RowDefinition>
//        </Grid.RowDefinitions>
//    </Grid>
//
//";
            string xamlColumns = string.Empty;
            for (int i = 0; i < NGridColumns; i++)
            {
                xamlColumns += "<ColumnDefinition Width='*'></ColumnDefinition>\r\n";
            }

            string xamlRows = string.Empty;
            for (int i = 0; i < NGridRows; i++)
            {
                xamlRows += "<RowDefinition Height='*'></RowDefinition>\r\n";
            }

            string xaml = string.Format(@"
<Grid 
xmlns='http://schemas.microsoft.com/client/2007'
    xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'
x:Name='tilepage'>
        <Grid.ColumnDefinitions>
            {0}
        </Grid.ColumnDefinitions>
        <Grid.RowDefinitions>
            {1}
        </Grid.RowDefinitions>
    </Grid>
", xamlColumns, xamlRows);

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

        public override void InitParams(List<object> Params)
        {
            NGridRows = int.Parse(Params[0].ToString());
            NGridColumns = int.Parse(Params[1].ToString());
        }
        
    }
}
