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
    public abstract class MenuItem
    {
        public List<MenuItem> listSubMenu = new List<MenuItem>();

        public abstract void FromXml(XmlReader reader, int CurrentLevel, int CurrentIndex);
        public static MenuItem ReadFromXml(XmlReader reader, int CurrentLevel, int CurrentIndex)
        {
            MenuItem item = null;
            if (true == reader.MoveToFirstAttribute())
            {
                if (reader.Name == "Type")
                {
                    switch (reader.Value)
                    {
                        case "Menu":
                            {
                                item = new TileMenu();
                                
                                break;
                            }
                        case "Page":
                            {
                                item = new TilePageMenu();
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }
                    reader.MoveToNextAttribute();
                    item.FromXml(reader, CurrentLevel, CurrentIndex);
                }

                reader.MoveToElement();
            }

            return item;
        }

        public static List<MenuItem> ReadDeeper(ref XmlReader reader, int CurrentLevel)
        {
            if (reader.ReadToDescendant("item"))
            {
                List<MenuItem> Tiles = new List<MenuItem>();
                int CurrentIndex = 0;

                //read current tile
                MenuItem item = MenuItem.ReadFromXml(reader, CurrentLevel, CurrentIndex);
                item.listSubMenu = MenuItem.ReadDeeper(ref reader, CurrentLevel + 1);
                Tiles.Add(item);
                CurrentIndex++;

                while (reader.ReadToNextSibling("item"))
                {
                    MenuItem item2 = MenuItem.ReadFromXml(reader, CurrentLevel, CurrentIndex);
                    item2.listSubMenu = MenuItem.ReadDeeper(ref reader, CurrentLevel + 1);
                    Tiles.Add(item2);
                    CurrentIndex++;
                }
                return Tiles;
            }
            return new List<MenuItem>();
        }

        public static List<MenuItem> Load(StringReader stream)
        {
            XmlReader reader = XmlReader.Create(stream);
            return ReadDeeper(ref reader, -1);
        }
    }
    public class Tile : MenuItem
    {
        public string ImageSource;//...
        public int GridColumn, GridRow, GridColumnSpan, GridRowSpan;
        //public int Page;
        //margin
        //border

        
        public virtual UIElement generate()
        {
            return new Button() { Width = 100, Height = 25, Content = "Test Tile" };
        }

        public override void FromXml(XmlReader reader, int CurrentLevel, int CurrentIndex)
        {
            //return new Tile();
        }
    }

    public class TileMenu : Tile
    {//tile when click vao open new menu
        //use 2 variable to indicate position of his item in global menu
        //to load new sub menu.
        public int CurrentLvl;
        public int CurrentIndexMenu;

        public string Name;
        public int NGridRows;
        public int NGridColumns;

        public override void FromXml(XmlReader reader, int CurrentLevel, int CurrentIndex)
        {
            //TileMenu tile = new TileMenu();
            for (int i = 0; i < reader.AttributeCount - 1; i++)
            {
                switch (reader.Name)
                {
                    case "NGridRows":
                        {
                            this.NGridRows = int.Parse(reader.Value);
                            break;
                        }
                    case "NGridColumns":
                        {
                            this.NGridColumns = int.Parse(reader.Value);
                            break;
                        }
                    case "Name":
                        {
                            this.Name = reader.Value;
                            break;
                        }
                    //case "Page":
                    //    {
                    //        tile.Page = int.Parse(reader.Value);
                    //        break;
                    //    }
                    case "GridRow":
                        {
                            this.GridRow = int.Parse(reader.Value);
                            break;
                        }
                    case "GridColumn":
                        {
                            this.GridColumn = int.Parse(reader.Value);
                            break;
                        }
                    case "GridColumnSpan":
                        {
                            this.GridColumnSpan = int.Parse(reader.Value);
                            break;
                        }
                    case "GridRowSpan":
                        {
                            this.GridRowSpan = int.Parse(reader.Value);
                            break;
                        }
                    case "ImageSource":
                        {
                            this.ImageSource = reader.Value;
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
                reader.MoveToNextAttribute();
            }

            this.CurrentLvl = CurrentLevel;
            this.CurrentIndexMenu = CurrentIndex;

            //return tile;
        }

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

        public void bt_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("bt_Click " + CurrentLvl.ToString() + " " + CurrentIndexMenu.ToString());
            BookLoader.Instance().OnClickedTile(CurrentLvl, CurrentIndexMenu);
        }
    }
    
    //public class TileArticle : Tile
    //{//tile that click open new article
    //    public override UIElement generate()
    //    {
    //        return base.generate();
    //    }
    //}
}
