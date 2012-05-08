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

namespace PageFlip
{
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();

            this.cbbModel.ImageSelected += new ImageSelectedEventHandler(cbbModel_ImageSelected);
            
            for (int i = 0; i < 5; i++)
            {
                cbbModel.AddImage("img" + i.ToString(), new WriteableBitmap(0, 0).FromResource("PrototypeControl/CoverFlow/Images/" + "blank_facemodel.jpg"));
            }
            
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            App.GoToPage(this, this.LayoutRoot, new ContentPage() { ParentView = this });
        }



        void cbbModel_ImageSelected(object sender, ImageSelectedEventArgs e)
        {
            //SetTarget((BaseModel)e.SelectedItem);
            MessageBox.Show(e.SelectedIndex.ToString());
        }
    }
}
