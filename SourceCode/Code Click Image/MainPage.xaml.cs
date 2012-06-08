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

namespace BookDemo
{
    public partial class MainPage : UserControl
    {
        Storyboard _timer = new Storyboard();
        public MainPage()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);

            _timer.Duration = TimeSpan.FromMilliseconds(30);
            _timer.Completed += new EventHandler(_timer_Completed);
            _timer.Begin();
        }

        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
           
            //ImageSource ism = new ImageSource();
            //imgCover.Source = new BitmapImage(new Uri(@"http://www.rajneeshnoonia.com/blog/uploads/images/header.png", UriKind.Absolute));
        }

   

        void _timer_Completed(object sender, EventArgs e)
        {
            if (pbar.Value < pbar.Maximum)
            {
                pbar.Value++;
                _timer.Begin();
            }
            else
            {
                book.TurnPage(true);
                VisualStateManager.GoToState(this, "Right", true);
            }
        }

        private void ProgressBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        bool isFirst = true;

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ImageCover.Visibility = Visibility.Visible;
            ImageCover.SetValue(Canvas.ZIndexProperty, 5);
           
            img_copy.Source = img.Source;
            img_copy.Width = img.RenderSize.Width;
            img_copy.Height = img.RenderSize.Height;
            

            Point p = img.TransformToVisual(ImageCover).Transform(new Point());

            img_copy.SetValue(Canvas.LeftProperty, p.X);
            img_copy.SetValue(Canvas.TopProperty, p.Y);

            img_copy.RenderTransform = new CompositeTransform();

           // VisualState ImgCenter_state = new VisualState();
           // ImgCenter_state.SetValue(FrameworkElement.NameProperty,"ImageCenter");
            if (isFirst)
            {
              /*  Storyboard sb = new Storyboard();
                DoubleAnimation ani_x = new DoubleAnimation();
                DoubleAnimation ani_y = new DoubleAnimation();
                DoubleAnimation ani_sx = new DoubleAnimation();
                DoubleAnimation ani_sy = new DoubleAnimation();

                ani_x.SetValue(Storyboard.TargetNameProperty, "img_copy");
                ani_x.SetValue(Storyboard.TargetPropertyProperty, new PropertyPath("(UIElement.RenderTransform).(CompositeTransform.TranslateX)"));
                ani_x.To = 299.918;
                ani_x.Duration = new Duration(TimeSpan.Zero);

                ani_y.SetValue(Storyboard.TargetNameProperty, "img_copy");
                ani_y.SetValue(Storyboard.TargetPropertyProperty, new PropertyPath("(UIElement.RenderTransform).(CompositeTransform.TranslateY)"));
                ani_y.To = 150.75;
                ani_y.Duration = new Duration(TimeSpan.Zero);

                ani_sx.SetValue(Storyboard.TargetNameProperty, "img_copy");
                ani_sx.SetValue(Storyboard.TargetPropertyProperty, new PropertyPath("(UIElement.RenderTransform).(CompositeTransform.ScaleX)"));
                ani_sx.To = 1.939;
                ani_sx.Duration = new Duration(TimeSpan.Zero);

                ani_sy.SetValue(Storyboard.TargetNameProperty, "img_copy");
                ani_sy.SetValue(Storyboard.TargetPropertyProperty, new PropertyPath("(UIElement.RenderTransform).(CompositeTransform.ScaleY)"));
                ani_sy.To = 1.011;
                ani_sy.Duration = new Duration(TimeSpan.Zero);

                sb.Children.Add(ani_x);
                sb.Children.Add(ani_y);
                sb.Children.Add(ani_sx);
                sb.Children.Add(ani_sy);

                ImageCenter.Storyboard = sb;
                //VisualStateGroup1.States.Add(ImgCenter_state);
                isFirst = false;*/

            }
           
            //VisualStateManager.GoToState(this, "ImageCenter", true);
        }

        private void ImageCover_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            
            //VisualStateManager.GoToState(this, "ImageNormal", true);
            //ImageCenter.Storyboard.Stop();
        	// TODO: Add event handler implementation here.
            ImageCover.SetValue(Canvas.ZIndexProperty,0);
            ImageCover.Visibility = Visibility.Collapsed;

        }
    }
}
