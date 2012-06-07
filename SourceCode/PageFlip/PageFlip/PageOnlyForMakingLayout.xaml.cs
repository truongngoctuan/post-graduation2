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
using System.Windows.Media.Imaging;

namespace PageFlip
{
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
            BitmapImage bi = new BitmapImage(new Uri(@"/PageFlip;component/Images/HomeMenuPage/home_02.jpg", UriKind.RelativeOrAbsolute));
            Image image = new Image();
            image.Source = bi;
            image.Width = 300;
            image.Height = 300;
            image.MouseEnter += new MouseEventHandler(image_MouseEnter);
            //InlineUIContainer container = new InlineUIContainer(image);
            Canvas cv = new Canvas();
            //cv.Width = 400;
            //cv.Height = 400;
            cv.Background = new SolidColorBrush(Colors.White);
            cv.Children.Add(image);

            InlineUIContainer container = new InlineUIContainer();
            container.Child = cv;
            Paragraph paragraph = new Paragraph();
            paragraph.Inlines.Add(container);
            asd.Blocks.Add(paragraph);


        }

        void image_MouseEnter(object sender, MouseEventArgs e)
        {
            MessageBox.Show("image_MouseEnter");
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

    }
}
