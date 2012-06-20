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

namespace PageFlip.Animation.UISlider
{
    public class ActionButton : Canvas
    {
        private Image enabledImage;
        private Image disabledImage;
        private bool isDisabled = false;

        public bool IsDisabled
        {
            get { return isDisabled; }
            set
            {
                isDisabled = value;
                if (value)
                {
                    disabledImage.Visibility = Visibility.Visible;
                    enabledImage.Visibility = Visibility.Collapsed;
                }
                else
                {
                    enabledImage.Visibility = Visibility.Visible;
                    disabledImage.Visibility = Visibility.Collapsed;
                }
            }
        }
        public ImageSource EnabledImageSource
        {
            get { return enabledImage.Source; }
            set
            {
                enabledImage.Source = value;
            }
        }
        public ImageSource DisabledImageSource
        {
            get { return disabledImage.Source; }
            set
            {
                disabledImage.Source = value;
            }
        }
        public event MouseEventHandler Click;
        public ActionButton()
        {

            enabledImage = new Image();
            disabledImage = new Image();
            enabledImage.Stretch = Stretch.Fill;
            disabledImage.Stretch = Stretch.Fill;

            this.Children.Add(enabledImage);
            this.Children.Add(disabledImage);

            disabledImage.Visibility = Visibility.Collapsed;
            enabledImage.MouseLeftButtonUp += new MouseButtonEventHandler(OnClick);
        }

        private void OnClick(object sender, MouseEventArgs e)
        {
            this.Click(sender, e);
        }


        public double ImageWidth
        {
            get { return disabledImage.Width > enabledImage.Height ? disabledImage.Width : enabledImage.Height; }
        }

        public double ImageHeight
        {
            get { return disabledImage.Height > enabledImage.Width ? disabledImage.Height : enabledImage.Width; }
        }

        public new double Height
        {
            get { return base.Height; }
            set
            {
                base.Height = value;
                disabledImage.Height = value;
                enabledImage.Height = value;
            }
        }
        public new double Width
        {
            get { return base.Width; }
            set
            {
                base.Width = value;
                disabledImage.Width = value;
                enabledImage.Width = value;
            }
        }
    }
}
