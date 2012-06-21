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
//using PageFlipUltis;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using PageFlip;
using PageFlip.Animation.UISlider;
using PageFlip.Animation.UISliderR;
using System.Windows.Media.Imaging;

namespace DataManager
{
    public abstract class MenuItem
    {
        public MenuItem Parent;
        public int currentIndex;

        public List<MenuItem> _listSubMenu = new List<MenuItem>();
        public List<MenuItem> ListSubMenu
        {
            get { return _listSubMenu; }
            set { _listSubMenu = value;
            for (int i = 0; i < _listSubMenu.Count; i++)
            {
                _listSubMenu[i].Parent = this;
            }
            }
        }

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
                        case "Article":
                            {
                                item = new TileArticle();
                                break;
                            }//TilePage
                        case "TilePage":
                            {
                                item = new TilePage();
                                break;
                            }
                        //case "MenuPage":
                        //    {
                        //        item = new TilePageMenu();
                        //        break;
                        //    }
                        //case "ArticlePage":
                        //    {
                        //        item = new TilePageMenu();
                        //        break;
                        //    }
                        case "TileClickableImage":
                            {
                                item = new TileClickableImage();
                                break;
                            }
                        case "TileScrollUI":
                            {
                                item = new TileScrollUI();
                                break;
                            }
                        case "TileScrollUI_LR":
                            {
                                item = new TileScrollUI_LR();
                                break;
                            }
                        default:
                            {
                                MessageBox.Show("ReadFromXml: no type");
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
                item.currentIndex = CurrentIndex;
                item.ListSubMenu = MenuItem.ReadDeeper(ref reader, CurrentLevel + 1);
                Tiles.Add(item);
                CurrentIndex++;

                while (reader.ReadToNextSibling("item"))
                {
                    MenuItem item2 = MenuItem.ReadFromXml(reader, CurrentLevel, CurrentIndex);
                    item2.currentIndex = CurrentIndex;
                    item2.ListSubMenu = MenuItem.ReadDeeper(ref reader, CurrentLevel + 1);
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
        public string VerticalAlignment;
        public string HorizontalAlignment;

        public int CurrentLvl;
        
        //public int NGridRows;
        //public int NGridColumns;

        public Tile()
        {
            VerticalAlignment = "Center";
            HorizontalAlignment = "Center";
        }

        public virtual UIElement generate()
        {
            return new Button() { Width = 100, Height = 25, Content = "Test Tile" };
        }

        public void FromXmlBasicAttribute(string AttName, string AttValue)
        {
            switch (AttName)
            {
                //case "NGridRows":
                //    {
                //        this.NGridRows = int.Parse(AttValue);
                //        break;
                //    }
                //case "NGridColumns":
                //    {
                //        this.NGridColumns = int.Parse(AttValue);
                //        break;
                //    }
                //case "Name":
                //    {
                //        this.Name = AttValue;
                //        break;
                //    }
                case "GridRow":
                    {
                        this.GridRow = int.Parse(AttValue);
                        break;
                    }
                case "GridColumn":
                    {
                        this.GridColumn = int.Parse(AttValue);
                        break;
                    }
                case "GridColumnSpan":
                    {
                        this.GridColumnSpan = int.Parse(AttValue);
                        break;
                    }
                case "GridRowSpan":
                    {
                        this.GridRowSpan = int.Parse(AttValue);
                        break;
                    }
                case "ImageSource":
                    {
                        this.ImageSource = AttValue;
                        break;
                    }
                case "VerticalAlignment":
                    {
                        this.VerticalAlignment = AttValue;
                        break;
                    }
                case "HorizontalAlignment":
                    {
                        this.HorizontalAlignment = AttValue;
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        public override void FromXml(XmlReader reader, int CurrentLevel, int CurrentIndex)
        {
            for (int i = 0; i < reader.AttributeCount - 1; i++)
            {
                this.FromXmlBasicAttribute(reader.Name, reader.Value);
                reader.MoveToNextAttribute();
            }
        }
    }

    public class TileMenu : Tile
    {//tile when click vao open new menu
        //use 2 variable to indicate position of his item in global menu
        //to load new sub menu.
        public string Name;

        public string BackImageSource;
        public TileMenu()
        {
            BackImageSource = string.Empty;
        }
        public override void FromXml(XmlReader reader, int CurrentLevel, int CurrentIndex)
        {
            //TileMenu tile = new TileMenu();
            for (int i = 0; i < reader.AttributeCount - 1; i++)
            {
                this.FromXmlBasicAttribute(reader.Name, reader.Value);
                switch (reader.Name)
                {
                    case "Name":
                        {
                            this.Name = reader.Value;
                            break;
                        }
                    case "BackImageSource": { this.BackImageSource = reader.Value; break; }
                    default:
                        {
                            break;
                        }
                }
                reader.MoveToNextAttribute();
            }

            this.CurrentLvl = CurrentLevel;
            this.currentIndex = CurrentIndex;

            //return tile;
        }

        public override UIElement generate()
        {
            string xaml = @"
<Button 
xmlns='http://schemas.microsoft.com/client/2007'
xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'
Grid.Row='{0}' Grid.Column='{1}' Grid.ColumnSpan='{2}' Grid.RowSpan='{3}' VerticalAlignment='{5}' HorizontalAlignment='{6}'
>
            <Image Source='{4}' Stretch='None'></Image>
        </Button>
";
            xaml = string.Format(xaml, GridRow, GridColumn, GridColumnSpan, GridRowSpan, ImageSource, VerticalAlignment, HorizontalAlignment);
            Button bt = (Button)Ultis.LoadXamlFromString(xaml);
            bt.Click += new RoutedEventHandler(bt_Click);
            bt.Style = App.Current.Resources["customButtonNoStyle"] as Style;

            return bt;
        }

        public void bt_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("bt_Click " + CurrentLvl.ToString() + " " + currentIndex.ToString());
            if (this.Parent == null)
            {
                //if (this.Name != null) MessageBox.Show(this.Name);
                BookLoader.Instance().OnClickedTile(CurrentLvl, -1, currentIndex);

            }
            else
            {
                //if (this.Name != null) MessageBox.Show(this.Name);
                BookLoader.Instance().OnClickedTile(CurrentLvl, this.Parent.currentIndex, currentIndex);
            }
        }
    }

    public class TileArticle : Tile
    {//tile that click open new article
        public string ArticleID;
        public string Name;

        public override void FromXml(XmlReader reader, int CurrentLevel, int CurrentIndex)
        {
            for (int i = 0; i < reader.AttributeCount - 1; i++)
            {
                this.FromXmlBasicAttribute(reader.Name, reader.Value);

                switch (reader.Name)
                {
                    case "Name":
                        {
                            this.Name = reader.Value;
                            break;
                        }
                    case "ArticleID":
                        {
                            this.ArticleID = reader.Value;
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
                reader.MoveToNextAttribute();
            }
        }

        public override UIElement generate()
        {
            string xaml = @"
<Button 
xmlns='http://schemas.microsoft.com/client/2007'
xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'
Grid.Row='{0}' Grid.Column='{1}' Grid.ColumnSpan='{2}' Grid.RowSpan='{3}' VerticalAlignment='{5}' HorizontalAlignment='{6}'
>
            <Image Source='{4}' Stretch='None'></Image>
        </Button>
";
            xaml = string.Format(xaml, GridRow, GridColumn, GridColumnSpan, GridRowSpan, ImageSource, VerticalAlignment, HorizontalAlignment);
            Button bt = (Button)Ultis.LoadXamlFromString(xaml);
            bt.Click += new RoutedEventHandler(bt_Click);
            bt.Style = App.Current.Resources["customButtonNoStyle"] as Style;

            return bt;
        }

        public void bt_Click(object sender, RoutedEventArgs e)
        {
            BookLoader.Instance().OnClickToArticle(ArticleID);
        }
    }


    public class TileClickableImage : Tile
    {//tile that click open new article
        Image _img;

        public override UIElement generate()
        {
            string xaml = @"
<Button 
xmlns='http://schemas.microsoft.com/client/2007'
xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'
Grid.Row='{0}' Grid.Column='{1}' Grid.ColumnSpan='{2}' Grid.RowSpan='{3}'  VerticalAlignment='{5}' HorizontalAlignment='{6}'
>
            <Image Source='{4}' Stretch='None'></Image>
        </Button>
";
            xaml = string.Format(xaml, GridRow, GridColumn, GridColumnSpan, GridRowSpan, ImageSource, VerticalAlignment, HorizontalAlignment);
            Button bt = (Button)Ultis.LoadXamlFromString(xaml);
            bt.Click += new RoutedEventHandler(bt_Click);
            bt.Style = App.Current.Resources["customButtonNoStyle"] as Style;

            _img = (Image)bt.Content;
            return bt;
        }

        public void bt_Click(object sender, RoutedEventArgs e)
        {
            //BookLoader.Instance().OnClickToArticle(ArticleID);
            //goi 1 ham ben masterpage
            BookLoader.Instance().OnClickedImage(_img);
        }
    }

    public class TileScrollUI : Tile
    {
        PageFlip.Animation.UISlider.UIScroll curScroll;
        public override UIElement generate()
        {
            string xaml = @"
                        <UIScroll:UIScroll 
                        xmlns='http://schemas.microsoft.com/client/2007'
                        xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'
                         xmlns:UIScroll='clr-namespace:PageFlip.Animation.UISlider;assembly=PageFlip'  
                        x:Name='abc' ScrollingTime ='300' UIWidth='102' UIHeight='144' 
                        VisibleImages='3' 
  Grid.Row='{0}' Grid.Column='{1}' Grid.ColumnSpan='{2}' Grid.RowSpan='{3}'  VerticalAlignment='{4}' HorizontalAlignment='{5}'
                        >                    
                        </UIScroll:UIScroll>
                        ";
            xaml = string.Format(xaml, GridRow, GridColumn, GridColumnSpan, GridRowSpan, VerticalAlignment, HorizontalAlignment);
            PageFlip.Animation.UISlider.UIScroll uiScroll = (PageFlip.Animation.UISlider.UIScroll)(System.Windows.Markup.XamlReader.Load(xaml) as UIElement);
            uiScroll.ButtonHeight = 20.0;
            curScroll = uiScroll;
            uiScroll.LeftButton.Opacity = 0;
            uiScroll.RightButton.Opacity = 0;
            uiScroll.MouseEnter += new MouseEventHandler(uiScroll_MouseEnter);
            uiScroll.MouseLeave += new MouseEventHandler(uiScroll_MouseLeave);
            double W = uiScroll.UIWidth;
            double H = uiScroll.UIHeight;

            for (int i = 1; i <= 9; i++)
            {
                Image img = new Image();
                img.Name = "image" + i.ToString();
                img.Source = new BitmapImage(new Uri("/PageFlip;component/Images/Demo1/recommended_content_0" + i.ToString() + ".png", UriKind.Relative));
                img.Width = W;
                img.Height = H;
                img.MouseLeftButtonDown += new MouseButtonEventHandler(bt_Click);

                uiScroll.AddUI(img);
            }

            uiScroll.DisabledImageLeft = new BitmapImage(new Uri("/PageFlip;component/Images/Slider/BtnDown_dis.png", UriKind.Relative));
            uiScroll.EnabledImageLeft = new BitmapImage(new Uri("/PageFlip;component/Images/Slider/BtnDown.png", UriKind.Relative));
            uiScroll.DisabledImageRight = new BitmapImage(new Uri("/PageFlip;component/Images/Slider/BtnUp_dis.png", UriKind.Relative));
            uiScroll.EnabledImageRight = new BitmapImage(new Uri("/PageFlip;component/Images/Slider/BtnUp.png", UriKind.Relative));

            return uiScroll;
        }

        void uiScroll_MouseLeave(object sender, MouseEventArgs e)
        {
            curScroll.LeftButton.Opacity = 0;
            curScroll.RightButton.Opacity = 0;
        }

        void uiScroll_MouseEnter(object sender, MouseEventArgs e)
        {
            curScroll.LeftButton.Opacity = 1;
            curScroll.RightButton.Opacity = 1;
        }


        public void bt_Click(object sender, RoutedEventArgs e)
        {

            MessageBox.Show((sender as Image).Name);
            //BookLoader.Instance().OnClickToArticle(ArticleID);
            //goi 1 ham ben masterpage
            //  BookLoader.Instance().OnClickedImage(_img);
        }
    }

    public class TileScrollUI_LR : Tile
    {
        public string strListImageSource;
        public string strListArticleID;

        string[] listImageSource;
        string[] listArticleID;

        PageFlip.Animation.UISliderR.UIScroll curScroll;

        public TileScrollUI_LR()
        {
            this.strListImageSource = string.Empty;
            this.strListArticleID = string.Empty;
        }

        public override void FromXml(XmlReader reader, int CurrentLevel, int CurrentIndex)
        {
            for (int i = 0; i < reader.AttributeCount - 1; i++)
            {
                this.FromXmlBasicAttribute(reader.Name, reader.Value);

                switch (reader.Name)
                {
                    case "strListImageSource": { this.strListImageSource = reader.Value; break; }
                    case "strListArticleID": { this.strListArticleID = reader.Value; break; }
                    default: { break; }
                }
                reader.MoveToNextAttribute();
            }
        }

        public override UIElement generate()
        {
            string xaml = @"
                        <UIScroll:UIScroll 
                        xmlns='http://schemas.microsoft.com/client/2007'
                        xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'
                        xmlns:UIScroll='clr-namespace:PageFlip.Animation.UISliderR;assembly=PageFlip'  
                        x:Name='abc' ScrollingTime ='300' UIWidth='430' UIHeight='480' 
                        VisibleImages='1' Background='White'
  Grid.Row='{0}' Grid.Column='{1}' Grid.ColumnSpan='{2}' Grid.RowSpan='{3}'  VerticalAlignment='{4}' HorizontalAlignment='{5}'
                        >                    
                        </UIScroll:UIScroll>
                        ";
            xaml = string.Format(xaml, GridRow, GridColumn, GridColumnSpan, GridRowSpan, VerticalAlignment, HorizontalAlignment);
            PageFlip.Animation.UISliderR.UIScroll uiScroll = (PageFlip.Animation.UISliderR.UIScroll)(System.Windows.Markup.XamlReader.Load(xaml) as UIElement);
            uiScroll.ButtonWidth = 20.0;
            curScroll = uiScroll;
            uiScroll.LeftButton.Opacity = 1;
            uiScroll.RightButton.Opacity = 1;
            uiScroll.MouseEnter += new MouseEventHandler(uiScroll_MouseEnter);
            uiScroll.MouseLeave += new MouseEventHandler(uiScroll_MouseLeave);
            double W = uiScroll.UIWidth;
            double H = uiScroll.UIHeight;

            listImageSource = strListImageSource.Split(new char[] { ' ' });
            listArticleID = strListArticleID.Split(new char[] { ' ' });

            for (int i = 0; i < listImageSource.Length; i++)
            {
                Button bt = new Button();
                bt.Style = App.Current.Resources["customButtonNoStyle"] as Style;

                Image img = new Image();
                img.Source = new BitmapImage(new Uri(listImageSource[i], UriKind.Relative));
                img.Stretch = Stretch.None;
                img.Width = W;
                img.Height = H;
                //img.MouseLeftButtonDown += new MouseButtonEventHandler(bt_Click);
                bt.Tag = listArticleID[i];

                bt.Content = img;
                bt.Click +=new RoutedEventHandler(bt_Click);

                uiScroll.AddUI(bt);
            }

            uiScroll.DisabledImageLeft = new BitmapImage(new Uri("/PageFlip;component/Images/Slider/BtnLeft_dis.png", UriKind.Relative));
            uiScroll.EnabledImageLeft = new BitmapImage(new Uri("/PageFlip;component/Images/Slider/BtnLeft.png", UriKind.Relative));
            uiScroll.DisabledImageRight = new BitmapImage(new Uri("/PageFlip;component/Images/Slider/BtnRight_dis.png", UriKind.Relative));
            uiScroll.EnabledImageRight = new BitmapImage(new Uri("/PageFlip;component/Images/Slider/BtnRight.png", UriKind.Relative));

           return uiScroll;
        }

        void uiScroll_MouseLeave(object sender, MouseEventArgs e)
        {
            curScroll.LeftButton.Opacity = 0;
            curScroll.RightButton.Opacity = 0;
        }

        void uiScroll_MouseEnter(object sender, MouseEventArgs e)
        {
            curScroll.LeftButton.Opacity = 1;
            curScroll.RightButton.Opacity = 1;
        }


        public void bt_Click(object sender, RoutedEventArgs e)
        {
           BookLoader.Instance().OnClickToArticle((sender as Button).Tag.ToString());
        }
    }
}
