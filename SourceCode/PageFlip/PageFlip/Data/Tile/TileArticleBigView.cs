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
using DataManager;
using System.Xml;
using PageFlip;

namespace DataManager
{
    public class TileArticleBigView: Tile
    {
        string BigImageSource;
        string BigImageVerticalAlignment, BigImageHorizontalAlignment;
        string TitleImageSource;
        string TitleImageVerticalAlignment, TitleImageHorizontalAlignment;

        string Content;
        double ContentWidth, ContentHeight;
        string ContentVerticalAlignment, ContentHorizontalAlignment;

        public string ArticleID;

        public override void FromXml(XmlReader reader, int CurrentLevel, int CurrentIndex)
        {
            for (int i = 0; i < reader.AttributeCount - 1; i++)
            {
                this.FromXmlBasicAttribute(reader.Name, reader.Value);

                switch (reader.Name)
                {
                    case "BigImageSource": { this.BigImageSource = reader.Value; break; }
                    case "BigImageVerticalAlignment": { this.BigImageVerticalAlignment = reader.Value; break; }
                    case "BigImageHorizontalAlignment": { this.BigImageHorizontalAlignment = reader.Value; break; }

                    case "TitleImageSource": { this.TitleImageSource = reader.Value; break; }
                    case "TitleImageVerticalAlignment": { this.TitleImageVerticalAlignment = reader.Value; break; }
                    case "TitleImageHorizontalAlignment": { this.TitleImageHorizontalAlignment = reader.Value; break; }

                    case "Content": { this.Content = reader.Value; break; }
                    case "ContentWidth": { this.ContentWidth = double.Parse(reader.Value); break; }
                    case "ContentHeight": { this.ContentHeight = double.Parse(reader.Value); break; }
                    case "ContentVerticalAlignment": { this.ContentVerticalAlignment = reader.Value; break; }
                    case "ContentHorizontalAlignment": { this.ContentHorizontalAlignment = reader.Value; break; }
                    case "ArticleID": { this.ArticleID = reader.Value; break; }

                    default: { break; }
                }
                reader.MoveToNextAttribute();
            }
        }

        public override UIElement generate()
        {
            string xaml = string.Format(@"
<Grid 
    xmlns='http://schemas.microsoft.com/client/2007'
    xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'
ShowGridLines='True' VerticalAlignment='{0}' HorizontalAlignment='{1}'
>
            <Grid.ColumnDefinitions>
                <ColumnDefinition Width='*'></ColumnDefinition>\r
            </Grid.ColumnDefinitions>
            <Grid.RowDefinitions>
                <RowDefinition Height='*'></RowDefinition>
            </Grid.RowDefinitions>
    </Grid>
",VerticalAlignment, HorizontalAlignment);
            Grid grdRoot = (Grid)Ultis.LoadXamlFromString(xaml);

            #region Title
            string xamlTitle = @"
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

            #endregion


            return grdRoot;

        }

        public void bt_Click(object sender, RoutedEventArgs e)
        {
            BookLoader.Instance().OnClickToArticle(ArticleID);
        }
    }
}
