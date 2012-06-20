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
using PageFlip;

namespace DataManager
{
   
    public class TilePage:MenuItem
    {//generate a page from multiple tiles
        public int NGridRows;
        public int NGridColumns;

        public string Margin;

        public string ImageBrush;//background source of this pages
        

        public TilePage()
        {
            Margin = "0,0,0,0";
            ImageBrush = string.Empty;
            
        }

        public virtual UIElement generatePage()
        {
            

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

            //<!--<ImageBrush AlignmentX='Left' AlignmentY='Top' Stretch='None' ImageSource='/PageFlip;component/Images/Demo1/lifestyle_01_01.jpg'></ImageBrush>-->
                //<SolidColorBrush Color='Orange'></SolidColorBrush>
            string strBackgroundColor;
            if (this.ImageBrush != string.Empty)
            {
                strBackgroundColor = "<ImageBrush AlignmentX='Left' AlignmentY='Top' Stretch='None' ImageSource='" 
                    + this.ImageBrush + "'></ImageBrush>";
            }
            else
            {
                if (this.currentIndex % 2 == 0) strBackgroundColor = "<SolidColorBrush Color='White'></SolidColorBrush>";
                else strBackgroundColor = "<SolidColorBrush Color='White'></SolidColorBrush>";
            }
            
            string xaml = string.Format(@"
<Grid
    xmlns='http://schemas.microsoft.com/client/2007'
    xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'
>
            <Grid.Background>
                {3}
            </Grid.Background>

<Grid.ColumnDefinitions>
    <ColumnDefinition Width='*'></ColumnDefinition>
</Grid.ColumnDefinitions>

<Grid.RowDefinitions>
    <RowDefinition Height='*'></RowDefinition>
</Grid.RowDefinitions>

    <Grid ShowGridLines='True' Margin='{2}'>
            <Grid.ColumnDefinitions>
                {0}
            </Grid.ColumnDefinitions>
            <Grid.RowDefinitions>
                {1}
            </Grid.RowDefinitions>
    </Grid>
</Grid>
", xamlColumns, xamlRows, this.Margin, strBackgroundColor);

            int iCounter = 0;
            Grid grdRoot = (Grid)Ultis.LoadXamlFromString(xaml);
            Grid grd = (Grid)grdRoot.Children[0];
            //load item
            foreach (Tile item in ListSubMenu)
            {
                grd.Children.Add(item.generate());
                iCounter++;
            }

            //add stack-back button
            if (this.currentIndex % 2 == 0)
            {
                List<string> listBackImg = BookLoader.Instance().listMenuBackImageSource;
                //string strlistMenuBackImageSource = string.Empty;
                //foreach (string item in listBackImg)
                //{
                //    strlistMenuBackImageSource += item;
                //}
                //MessageBox.Show(strlistMenuBackImageSource + " " + listBackImg);

                //dang o dung cap cua no, nen chi can lay n-1 cap truoc no la dc
                if (listBackImg.Count >= 1)
                {
                    StackPanel StPnl = new StackPanel();
                    StPnl.Orientation = Orientation.Horizontal;
                    StPnl.VerticalAlignment = VerticalAlignment.Top;
                    StPnl.HorizontalAlignment = HorizontalAlignment.Left;

                    for (int i = 0; i < listBackImg.Count; i++)
                    {
                        string xamlBt = @"
<Button 
xmlns='http://schemas.microsoft.com/client/2007'
xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'
>
            <Image Source='{0}' Stretch='None'></Image>
        </Button>
";
                        xamlBt = string.Format(xamlBt, listBackImg[i]);
                        Button bt = (Button)Ultis.LoadXamlFromString(xamlBt);
                        bt.Click += new RoutedEventHandler(bt_Click);
                        bt.Style = App.Current.Resources["customButtonNoStyle"] as Style;
                        StPnl.Children.Add(bt);
                    }
                    grdRoot.Children.Add(StPnl);
                }

                
            }

            if (iCounter > 0)
                return grdRoot;
            else
                return null;
        }

        void bt_Click(object sender, RoutedEventArgs e)
        {
            BookLoader.Instance().OnBack();
        }

        public override void FromXml(XmlReader reader, int CurrentLevel, int CurrentIndex)
        {
            //TilePageMenu tile = new TilePageMenu();
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
                    case "Margin":
                        {
                            this.Margin = reader.Value;
                            break;
                        }
                    case "ImageBrush":
                        {
                            this.ImageBrush = reader.Value;
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

        //public virtual UIElement generatePage()
        //{
        //    return new Button() { Width = 100, Height = 25, Content = "Test Tile Page "};
        //}

        //public override void FromXml(XmlReader reader, int CurrentLevel, int CurrentIndex)
        //{
        //}

    }
    
//    public class TilePageMenu : TilePage
//    {
////        public int NGridRows;
////        public int NGridColumns;

////        public string Margin;
////        public TilePageMenu()
////        {
////            Margin = "0,0,0,0";
////        }

////        public override UIElement generatePage()
////        {
////            string xamlColumns = string.Empty;
////            for (int i = 0; i < NGridColumns; i++)
////            {
////                xamlColumns += "<ColumnDefinition Width='*'></ColumnDefinition>\r\n";
////            }

////            string xamlRows = string.Empty;
////            for (int i = 0; i < NGridRows; i++)
////            {
////                xamlRows += "<RowDefinition Height='*'></RowDefinition>\r\n";
////            }


////            string strBackgroundColor;
////            if (this.currentIndex % 2 == 0) strBackgroundColor = "Orange";
////            else strBackgroundColor = "Violet";
////            string xaml = string.Format(@"
////<Grid
////    xmlns='http://schemas.microsoft.com/client/2007'
////    xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'
////Background='{3}'
////>
////<Grid.ColumnDefinitions>
////    <ColumnDefinition Width='*'></ColumnDefinition>
////</Grid.ColumnDefinitions>
////
////<Grid.RowDefinitions>
////    <RowDefinition Height='*'></RowDefinition>
////</Grid.RowDefinitions>
////
////    <Grid Background='White' ShowGridLines='True' Margin='{2}'>
////            <Grid.ColumnDefinitions>
////                {0}
////            </Grid.ColumnDefinitions>
////            <Grid.RowDefinitions>
////                {1}
////            </Grid.RowDefinitions>
////    </Grid>
////</Grid>
////", xamlColumns, xamlRows, this.Margin, strBackgroundColor);

////            int iCounter = 0;
////            Grid grdRoot = (Grid)Ultis.LoadXamlFromString(xaml);
////            Grid grd = (Grid)grdRoot.Children[0];
////            //load item
////            foreach (Tile item in ListSubMenu)
////            {
////                grd.Children.Add(item.generate());
////                iCounter++;
////            }

////            if (iCounter > 0)
////                return grdRoot;
////            else
////                return null;
////        }
        
//        //public override void FromXml(XmlReader reader, int CurrentLevel, int CurrentIndex)
//        //{
//        //    //TilePageMenu tile = new TilePageMenu();
//        //    for (int i = 0; i < reader.AttributeCount - 1; i++)
//        //    {
//        //        switch (reader.Name)
//        //        {
//        //            case "NGridRows":
//        //                {
//        //                    this.NGridRows = int.Parse(reader.Value);
//        //                    break;
//        //                }
//        //            case "NGridColumns":
//        //                {
//        //                    this.NGridColumns = int.Parse(reader.Value);
//        //                    break;
//        //                }
//        //            case "Margin":
//        //                {
//        //                    this.Margin = reader.Value;
//        //                    break;
//        //                }
//        //            default:
//        //                {
//        //                    break;
//        //                }
//        //        }
//        //        reader.MoveToNextAttribute();
//        //    }
//        //}

        
//    }

//    public class TilePageArticle : TilePage
//    {
////        public int NGridRows;
////        public int NGridColumns;

////        public override UIElement generatePage()
////        {
////            string xamlColumns = string.Empty;
////            for (int i = 0; i < NGridColumns; i++)
////            {
////                xamlColumns += "<ColumnDefinition Width='*'></ColumnDefinition>\r\n";
////            }

////            string xamlRows = string.Empty;
////            for (int i = 0; i < NGridRows; i++)
////            {
////                xamlRows += "<RowDefinition Height='*'></RowDefinition>\r\n";
////            }

////            string xaml = string.Format(@"
////<Grid 
////xmlns='http://schemas.microsoft.com/client/2007'
////    xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'
////x:Name='tilepage'>
////        <Grid.ColumnDefinitions>
////            {0}
////        </Grid.ColumnDefinitions>
////        <Grid.RowDefinitions>
////            {1}
////        </Grid.RowDefinitions>
////    </Grid>
////", xamlColumns, xamlRows);

////            int iCounter = 0;
////            Grid grd = (Grid)Ultis.LoadXamlFromString(xaml);
////            //load item
////            foreach (Tile item in ListSubMenu)
////            {
////                grd.Children.Add(item.generate());
////                iCounter++;
////            }

////            if (iCounter > 0)
////                return grd;
////            else
////                return null;
////        }

//        //public override void FromXml(XmlReader reader, int CurrentLevel, int CurrentIndex)
//        //{
//        //    for (int i = 0; i < reader.AttributeCount - 1; i++)
//        //    {
//        //        switch (reader.Name)
//        //        {
//        //            case "NGridRows":
//        //                {
//        //                    this.NGridRows = int.Parse(reader.Value);
//        //                    break;
//        //                }
//        //            case "NGridColumns":
//        //                {
//        //                    this.NGridColumns = int.Parse(reader.Value);
//        //                    break;
//        //                }
//        //            default:
//        //                {
//        //                    break;
//        //                }
//        //        }
//        //        reader.MoveToNextAttribute();
//        //    }
//        //}
//    }

}
