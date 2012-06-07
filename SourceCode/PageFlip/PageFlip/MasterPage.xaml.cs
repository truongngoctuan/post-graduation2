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
using System.Windows.Navigation;
using DataManager;
using SLMitsuControls;
//using PageFlip.DataLoader;

namespace PageFlip
{
    public partial class MasterPage : Page
    {
        Storyboard _timer = new Storyboard();
    
        public MasterPage()
        {
            InitializeComponent();
            _timer.Duration = TimeSpan.FromMilliseconds(30);
            _timer.Completed += new EventHandler(_timer_Completed);
            _timer.Begin();
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
                containerContent.RenderTransform = new CompositeTransform();
                VisualStateManager.GoToState(this, "Right", true);
                pbar.Opacity = 0;
                /*MessageBox.Show("asdf");
                MainPage2 page = FindControl<MainPage2>(containerContent, typeof(MainPage2),"");
                page.MoveRight();*/
                BookLoader.Instance().GoToFirstMenuPage();
            }
        }


        public T FindControl<T>(UIElement parent, Type targetType, string ControlName) where T : FrameworkElement
        {

            if (parent == null) return null;

            if (parent.GetType() == targetType)// && ((T)parent).Name == ControlName)
            {
                return (T)parent;
            }
            T result = null;
            int count = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < count; i++)
            {
                UIElement child = (UIElement)VisualTreeHelper.GetChild(parent, i);

                if (FindControl<T>(child, targetType, ControlName) != null)
                {
                    result = FindControl<T>(child, targetType, ControlName);
                    break;
                }
            }
            return result;
        }


        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void btBack_Click(object sender, RoutedEventArgs e)
        {
            BookLoader.Instance().OnBackMenu();
        }

        private void LayoutRoot_Loaded(object sender, RoutedEventArgs e)
        {

        }

    }
}
