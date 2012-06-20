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

namespace PageFlip.Animation.UISliderR
{
    public partial class AnimationUI : UserControl
    {
        public event EventHandler<AnimationUIEventArgs> AnimationCompleted;
        public event EventHandler<MouseEventArgs> Click;
        public AnimationUI()
        {
            InitializeComponent();
            LayoutRoot.Background = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));
            content.MouseLeftButtonUp += new MouseButtonEventHandler(ContentMouseLeftButtonUp);
            leftStoryboard.Completed += new EventHandler(AnimationLeftCompleted);
            rightStoryboard.Completed += new EventHandler(AnimationRightCompleted);
        }

        void ContentMouseLeftButtonUp(object sender, MouseEventArgs e)
        {
            if (this.Click != null)
                this.Click(sender, e);
        }

        public UIElement ContentSource
        {
            set
            {
                if (value == null)
                {
                    this.content.Children.Clear();
                }
                else
                {

                    this.content.Children.Clear();
                    this.content.Children.Add(value);
                }
            }
        }

        public UIElement Content
        {
            get
            {
                return this.content;
            }
        }

        public void AnimateLeft()
        {
            leftStoryboard.Begin();
        }

        void AnimationLeftCompleted(object sender, EventArgs e)
        {
            if (this.AnimationCompleted != null)
                this.AnimationCompleted(this, new AnimationUIEventArgs(true));
            Storyboard st = sender as Storyboard;
            st.Seek(TimeSpan.FromSeconds(0));
            st.Stop();
        }

        public void AnimateRight()
        {
            rightStoryboard.Begin();
        }

        void AnimationRightCompleted(object sender, EventArgs e)
        {
            this.AnimationCompleted(this, new AnimationUIEventArgs(false));

        }
        public void Reset()
        {
            leftStoryboard.Seek(TimeSpan.FromSeconds(0));
            leftStoryboard.Stop();
            rightStoryboard.Seek(TimeSpan.FromSeconds(0));
            rightStoryboard.Stop();
        }

        public new double Height
        {
            get { return base.Height; }
            set
            {
                base.Height = value;
                LayoutRoot.Height = value;
                content.Height = value;
            }
        }
        public Duration AnimationDuration
        {
            get { return leftStoryboard.Duration; }
            set
            {
                translateLeft.Duration = value;
                translateRight.Duration = new Duration(value.TimeSpan);

            }
        }
        public double AnimateBy
        {
            get { return (double)translateLeft.By; }
            set
            {
                translateLeft.By = -value;
                translateRight.By = value;
            }
        }
        public new double Width
        {
            get { return base.Width; }
            set
            {
                base.Width = value;
                LayoutRoot.Width = value;
                content.Width = value;
            }
        }
    }
}
