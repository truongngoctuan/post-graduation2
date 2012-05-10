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
using _3DPresentation.Views.Editor;
using System.Windows.Media.Imaging;
using PageFlip.DataLoader;

namespace PageFlip
{
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();

            this.cbbModel.ImageSelected += new ImageSelectedEventHandler(cbbModel_ImageSelected);

            LoadDataCoverFlow();

            iSelectedMenuIndex = 0;
            ctnMenuDescriptor.Children.Clear();
            ctnMenuDescriptor.Children.Add(listDataCoverFlow[iSelectedMenuIndex]);
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void LayoutRoot_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            cbbModel.SetActualWidthAndHeight(LayoutRoot.ActualWidth, LayoutRoot.ActualHeight);
        }

        #region coverflow data
        List<UIElement> listDataCoverFlow = new List<UIElement>();
        int iSelectedMenuIndex = 0;
        void LoadDataCoverFlow()
        {
            List<object> listMenuItem = MenuLoader.LoadMenu(0);
            for (int i = 0; i < listMenuItem.Count; i++)
            {
                MenuItemLvl0 item = (MenuItemLvl0)listMenuItem[i];
                Image img = new Image();
                BitmapImage bmImg = new BitmapImage(new Uri(item.ImagePathDescription, UriKind.RelativeOrAbsolute));
                img.Source = bmImg;

                Button bt = new Button();
                bt.Width = 600;
                bt.Height = 307;
                bt.Margin = new Thickness(0, 0, 0, 100);
                bt.Content = img;
                bt.Style= App.Current.Resources["customButtonNoStyle"] as Style;
                listDataCoverFlow.Add(bt);
                bt.Click += new RoutedEventHandler(bt_Click);

                cbbModel.AddImage(listDataCoverFlow[i], new WriteableBitmap(0, 0).FromResource(item.ImagePath));
            }

        }

        void cbbModel_ImageSelected(object sender, ImageSelectedEventArgs e)
        {
            iSelectedMenuIndex = e.SelectedIndex;
            ctnMenuDescriptor.Children.Clear();
            ctnMenuDescriptor.Children.Add(listDataCoverFlow[iSelectedMenuIndex]);
        }

        void bt_Click(object sender, RoutedEventArgs e)
        {
            GoToContentPage(iSelectedMenuIndex);
        }

        

        void GoToContentPage(int idx)
        {
            App.GoToPage(this, this.LayoutRoot, 
                new MainPage() { 
                    ParentView = this,
                    ContentPageIndex = idx
                });
        }
        #endregion
    }
}
