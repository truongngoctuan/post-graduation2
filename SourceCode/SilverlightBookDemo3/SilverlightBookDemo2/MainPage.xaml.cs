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
using System.ServiceModel;
using System.Collections.ObjectModel;
using Liquid;

namespace SilverlightBookDemo2
{
    public partial class MainPage : UserControl
    {

       
        public MainPage()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
        }

        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            //ImageSource ism = new ImageSource();
            //imgCover.Source = new BitmapImage(new Uri(@"http://www.rajneeshnoonia.com/blog/uploads/images/header.png", UriKind.Absolute));

            //Button bt = new Button();
            //bt.Content = "asdasd";
            //bt.Width = 100;
            //bt.Height = 25;
            //bt.Click += new RoutedEventHandler(bt_Click);
            //slbook1.Items.Add(bt);


           

            ServiceReference1.ServiceDNN3Client ws = new ServiceReference1.ServiceDNN3Client();
          
            ws.Get_AllArticlesCompleted += new EventHandler<ServiceReference1.Get_AllArticlesCompletedEventArgs>(ws_Get_AllArticlesCompleted);
            ws.Get_AllArticlesAsync();
           
        }

        void ws_Get_AllArticlesCompleted(object sender, ServiceReference1.Get_AllArticlesCompletedEventArgs e)
        {
           //  rta.SetDefaultStyles();
           //  rta.Load("<h1>"+e.Result[1].Title+"</h1>"+"<img src=  />"+"<p>"+e.Result[1].Summary+"</p><br/>"+e.Result[1].Content);
            
                
            ArticleLargeIcon.ArticleImage articleControl = new ArticleLargeIcon.ArticleImage(this,e.Result);
            // articleControl.Width = 200; 
            //articleControl.Height = 200; 
            slbook1.Items.Clear();
            slbook1.Items.Add(articleControl);
        }
       
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                WebClient wc = new WebClient();
                wc.OpenReadCompleted += wc_OpenReadCompleted;
                wc.OpenReadAsync(new Uri(@"\Data.xml", UriKind.RelativeOrAbsolute));
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        private void wc_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            try
            {
                //Kiem tra thuoc tinh Error cho cac loi 
                if (e.Error != null)
                {
                    return;
                }
                //Neu khong co loi, Lay du lieu stream ve va phan tich chung toi XDocument thong qua phuong thuc Load 
                using (System.IO.Stream s = e.Result)
                {
                    //UIElement eles = Utils.LoadXamlFromStream(s);
                    //slbook1.Items.Clear();

                    //slbook1.Items.Add(eles);

                    //Button bt = new Button();
                    //bt.Content = "asdasd";
                    //bt.Width = 100;
                    //bt.Height = 25;
                    //bt.Click += new RoutedEventHandler(bt_Click);
                    //slbook1.Items.Add(bt);

                    //slbook1.Items.Clear();
                    List<UIElement> list = Utils.LoadXamlFromStream(s);
                    for (int i = 0; i < list.Count; i++)
                    {
                        slbook1.Items.Add(list[i]);
                    }
                    //slbook1.InvalidateArrange();
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

    }
}
