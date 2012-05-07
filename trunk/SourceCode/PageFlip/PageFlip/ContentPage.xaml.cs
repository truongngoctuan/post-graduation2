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

namespace PageFlip
{
    public partial class ContentPage : Page
    {
        public ContentPage()
        {
            InitializeComponent();
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        public UserControl ParentView { get; set; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            App.GoToPage(this, this.LayoutRoot, this.ParentView);
        }

    }
}
