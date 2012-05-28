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
using System.Collections.Generic;
using System.IO;
using System.Xml;

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

        public List<Tile> listSubMenu = new List<Tile>();

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

        public static TileMenu ReadTile(XmlReader reader, int CurrentLevel, int CurrentIndex)
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

                tile.CurrentLvl = CurrentLevel;
                tile.CurrentIndexMenu = CurrentIndex;


                reader.MoveToElement();
            }

            return tile;
        }

        public static List<Tile> ReadDeeper(ref XmlReader reader, int CurrentLevel)
        {
            if (reader.ReadToDescendant("tile"))
            {
                List<Tile> Tiles = new List<Tile>();
                int CurrentIndex = 0;

                //read current tile
                TileMenu item = TileMenu.ReadTile(reader, CurrentLevel, CurrentIndex);
                item.listSubMenu = TileMenu.ReadDeeper(ref reader, CurrentLevel + 1);
                Tiles.Add(item);
                CurrentIndex++;

                while (reader.ReadToNextSibling("tile"))
                {
                    TileMenu item2 = TileMenu.ReadTile(reader, CurrentLevel, CurrentIndex);
                    item2.listSubMenu = TileMenu.ReadDeeper(ref reader, CurrentLevel + 1);
                    Tiles.Add(item2);
                    CurrentIndex++;
                }
                return Tiles;
            }
            return null;
        }

        public static List<Tile> Load(StringReader stream)
        {
            XmlReader reader = XmlReader.Create(stream);

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    if (reader.Name == "menu")
                    {
                        return ReadDeeper(ref reader, 0);
                    }
                }
            }

            return null;
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
