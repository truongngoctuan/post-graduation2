using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Media.Imaging;
using System.Collections.ObjectModel;

namespace SilverlightBookDemo2.ArticleLargeIcon
{
    public partial class ArticleImage : UserControl
    {
        List<Image> listImg;

        int iWidthImage = 150;
        int iHeightImage = 150;
        int iSpaceBetweenImages = 2;

        MainPage parent2;

        string[] strListImgUrl = new string[] { 
                 "../Images/economy.jpg",
                "../Images/fashion.jpg",
            "../Images/sport.jpg",
            "../Images/tech.jpg"};

        ObservableCollection<ServiceReference1.Article> lst_Arts;

        public ArticleImage(MainPage parent,ObservableCollection<ServiceReference1.Article> lst)
        {
            InitializeComponent();

            parent2 = parent;
            lst_Arts = lst;
            //iCurrentState = Article.ImageOnly;
            LoadStateImageOnly();
        }

        #region State Image Only
        void LoadStateImageOnly()
        {
            ///init 
            listImg = new List<Image>();
            string[] strListImgUrl = new string[] { 
                "../Images/economy.jpg",
                "../Images/fashion.jpg",
            "../Images/sport.jpg",
            "../Images/tech.jpg"};


            ///load
            int nArticle = strListImgUrl.Count();
            for (int i = 0; i < nArticle; i++)
            {
                BitmapImage img = new BitmapImage(new Uri(strListImgUrl[i], UriKind.RelativeOrAbsolute));
                Image img2 = new Image();
                img2.Source = img;
                img2.Width = iWidthImage;
                img2.Height = iHeightImage;
                listImg.Add(img2);
            }

            LayoutRoot.Width = (iWidthImage + iSpaceBetweenImages) * (3);
            LayoutRoot.Height = (iHeightImage + iSpaceBetweenImages) * (nArticle / 3 + 1);
            for (int i = 0; i < nArticle; i++)
            {

                LayoutRoot.Children.Add(listImg[i]);

                Canvas.SetTop(listImg[i], (i / 3) * (iHeightImage + iSpaceBetweenImages));
                Canvas.SetLeft(listImg[i], (i % 3) * (iWidthImage + iSpaceBetweenImages));
                Canvas.SetZIndex(listImg[i], 2);
            }

            this.MouseLeftButtonDown += new MouseButtonEventHandler(ArticleImage_MouseLeftButtonDown);
            this.MouseLeftButtonUp += new MouseButtonEventHandler(ArticleImage_MouseLeftButtonUp);
        }

        void RemoveStateImageOnly()
        {
            this.MouseLeftButtonDown -= ArticleImage_MouseLeftButtonDown;
            this.MouseLeftButtonUp -= ArticleImage_MouseLeftButtonUp;

            LayoutRoot.Children.Clear();
        }

        Point clickPosition;
        bool bIsClick = false;
        void ArticleImage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //throw new NotImplementedException();
            if (bIsClick)
            {
                OnImageOnlyClick();
                bIsClick = false;
            }

        }

        void ArticleImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //throw new NotImplementedException();
            clickPosition = e.GetPosition(LayoutRoot);
            bIsClick = true;
        }

        void OnImageOnlyClick()
        {
        
            ///find out what image was clicked
            ///
            int r = (int)(clickPosition.Y / (iHeightImage + iSpaceBetweenImages));
            int c = (int)(clickPosition.X / (iWidthImage + iSpaceBetweenImages));

            int iImg = r * 3 + c;
            //MessageBox.Show("X: " +clickPosition.X + 
            //    " Y: " + clickPosition.Y +
            //    " image was clicked: " + iImg);

            RemoveStateImageOnly();

            iLoadImageWihSummary = iImg;
            LoadStateImageWithSummary();

            if (iImg == 0)
            {
                for (int i = 0; i < 3; i++)
                {
                    Liquid.HtmlRichTextArea asd = new Liquid.HtmlRichTextArea();
                    asd.Name = "rta0" + i.ToString();
                    asd.Width = 600;
                    asd.Height = 550;
                    asd.SetDefaultStyles();
                    asd.Load("<p>" + "<h1>" + lst_Arts[i].Title + "</h1>" + "<img height=\"42\" width=\"42\" src=\"Images/" + lst_Arts[i].Image + "\"/>" + lst_Arts[i].Content + "</p>");
                    parent2.slbook1.Items.Add(asd);
                }
            
            }
            else
                if (iImg == 1)
                {
                    
                        ((Liquid.HtmlRichTextArea)parent2.slbook1.Items[1]).Load("<p>" + "<h1>" + lst_Arts[3].Title + "</h1>" + "<img height=\"42\" width=\"42\" src=\"Images/" + lst_Arts[3].Image + "\"/>" + lst_Arts[3].Content + "</p>");
                        ((Liquid.HtmlRichTextArea)parent2.slbook1.Items[2]).Load("<p>" + "<h1>" + lst_Arts[2].Title + "</h1>" + "<img height=\"42\" width=\"42\" src=\"Images/" + lst_Arts[2].Image + "\"/>" + lst_Arts[2].Content + "</p>");
                }
            
                      
        }

        #endregion
        //enum Article { ImageOnly, ImageWithSummary}
        //Article iCurrentState;

        #region State Image With Summary
        int iLoadImageWihSummary;
        void LoadStateImageWithSummary()
        {
            string strSummary = "summary of article!";
            StackPanel spnl = new StackPanel();
            string s = @"
            <StackPanel Orientation='Horizontal'
                xmlns='http://schemas.microsoft.com/client/2007'
                xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'>
            <Image Source='"
                + strListImgUrl[iLoadImageWihSummary] +
            @"' Width='150' Height='150'></Image>
            <TextBlock Text='"
            + strSummary +
                @"' VerticalAlignment='Center'></TextBlock>
        </StackPanel>
        ";
            LayoutRoot.Children.Add(System.Windows.Markup.XamlReader.Load(s) as UIElement);

            ///init button back
            Button btBack = new Button();
            btBack.Width = 150;
            btBack.Height = 25;
            btBack.Content = "Back";

            Canvas.SetTop(btBack, 300);
            Canvas.SetLeft(btBack, 10);

            btBack.Click += new RoutedEventHandler(btBack_Click);
            LayoutRoot.Children.Add(btBack);
        }

        void btBack_Click(object sender, RoutedEventArgs e)
        {
            RemoveStateImageWithSummary();
            LoadStateImageOnly();
        }
        void RemoveStateImageWithSummary()
        {
            LayoutRoot.Children.Clear();
        }
        #endregion

        #region add more page
        #endregion

    }
}
