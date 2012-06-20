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

namespace PageFlip.Animation.UISlider
{
    public partial class UIScroll : UserControl
    {
        private Rect clippingRect;
        private RectangleGeometry clippingGeometry;
        //  private AnimationRectangle selectionFrame;
        private int selectedUIIndex;

        public ActionButton RightButton { get; set; }
        public ActionButton LeftButton { get; set; }

        private Double uiHeight;
        private Double uiWidth;
        private double margin = 2;
        public double Distance
        {
            get { return margin; }
            set { margin = value; RefreshSize(); }
        }


        private int visibleUIsTargetCount;
        private int animatedUIsCount;
        private int readyAnimationsCount;
        private int offset = 0;
        private bool isReloading;

        private List<AnimationUI> animationsList;
        private List<UIElement> uisList;//BitmapImage
        private List<UIElement> locationsList;//Uri location

        public event EventHandler<SelectedItemChanged> SelectionChanged;

        /*     public Brush SelectionColor
             {
                 get { return this.selectionFrame.Fill; }
                 set { this.selectionFrame.Fill = value; }
             }
         */
        public new Brush Background
        {
            get { return this.dataCanvas.Background; }
            set { this.dataCanvas.Background = value; }
        }
        public double ButtonWidth
        {
            get { return LeftButton.ImageWidth; }
            set
            {
                LeftButton.Width = value;
                RightButton.Width = value;
                UIWidth = UIWidth;

            }
        }

        public double ButtonHeight
        {
            get { return LeftButton.ImageHeight; }
            set
            {
                LeftButton.Height = value;
                RightButton.Height = value;
                UIHeight = UIHeight;

            }
        }


        public double UIHeight
        {
            get { return uiHeight; }
            set
            {
                uiHeight = value;
                /*     Height = value + 2 * Distance;
                     LeftButton.Height = Height;
                     RightButton.Height = Height;*/
                //   selectionFrame.Height = Height;

            }
        }
        public ImageSource DisabledImageLeft
        {
            set
            {
                LeftButton.DisabledImageSource = value;
            }
        }
        public ImageSource EnabledImageLeft
        {
            set
            {
                LeftButton.EnabledImageSource = value;
            }
        }
        public ImageSource DisabledImageRight
        {
            set
            {
                RightButton.DisabledImageSource = value;
            }
        }
        public ImageSource EnabledImageRight
        {
            set
            {
                RightButton.EnabledImageSource = value;
            }
        }

        public Double UIWidth
        {
            get { return uiWidth; }
            set
            {
                uiWidth = value;
                ////
                Width = value;// +2 * Distance;
                LeftButton.Width = Width;
                RightButton.Width = Width;
                ////
                RefreshSize();
                RefreshVisibleImagesCount();

            }
        }

        public int VisibleImages
        {
            get { return animatedUIsCount - 2; }
            set
            {
                animatedUIsCount = value + 2;
                visibleUIsTargetCount = value + 2;
                if (animatedUIsCount > locationsList.Count)
                    animatedUIsCount = locationsList.Count;
                RefreshSize();
                RefreshVisibleImagesCount();
            }
        }

        public UIScroll()
        {
            InitializeComponent();

            locationsList = new List<UIElement>();
            uisList = new List<UIElement>();
            animationsList = new List<AnimationUI>();

            LeftButton = new ActionButton();
            RightButton = new ActionButton();

            clippingGeometry = new RectangleGeometry();
            clippingRect = new Rect();
            LayoutRoot.Children.Add(LeftButton);
            LayoutRoot.Children.Add(RightButton);
            clippingGeometry.Rect = clippingRect;
            dataCanvas.Clip = clippingGeometry;

            /*    selectionFrame = new AnimationRectangle();
                selectionFrame.RadiusX = 10;
                selectionFrame.RadiusY = 10;
                selectionFrame.Fill = new SolidColorBrush(Color.FromArgb(200, 200, 200, 200));
                dataCanvas.Children.Add(selectionFrame);*/
            //Need to be defined in the construction.
            // LeftButton.Width = 0;
            //   RightButton.Width = 0;
            LeftButton.Height = 0;
            RightButton.Height = 0;

            LeftButton.Click += new MouseEventHandler(MoveToLeft);
            RightButton.Click += new MouseEventHandler(MoveToRight);

            locationsList.Add(new Canvas());
            uisList.Add(new Canvas());
            AddVisible(new Canvas());
            locationsList.Add(new Canvas());
            uisList.Add(new Canvas());

            Canvas.SetLeft(LayoutRoot, 0);
            Canvas.SetLeft(dataCanvas, 0);
            Canvas.SetZIndex(LeftButton, 5);
            Canvas.SetZIndex(RightButton, 5);

            isReloading = true;
            selectedUIIndex = 0;
            VisibleImages = 0;
            readyAnimationsCount = 2;
            LeftButton.IsDisabled = true;
            RightButton.IsDisabled = true;

            //  LeftButton.SetValue(Canvas.BackgroundProperty, new SolidColorBrush(Colors.Black));
            //  RightButton.SetValue(Canvas.BackgroundProperty, new SolidColorBrush(Colors.Black));
        }
        private double scrollingTime;
        public double ScrollingTime
        {
            get { return scrollingTime; }
            set
            {
                scrollingTime = value;
                //   selectionFrame.AnimationDuration = new Duration(TimeSpan.FromMilliseconds(value));

                foreach (AnimationUI i in animationsList)
                {
                    i.AnimationDuration = new Duration(TimeSpan.FromMilliseconds(value));
                }
            }
        }

        private void RefreshVisibleImagesCount()
        {
            int last = animationsList.Count - 1;
            while (animationsList.Count > animatedUIsCount)
            {
                animationsList[last].Visibility = Visibility.Collapsed;
                animationsList.RemoveAt(last);
                last--;
            }
            readyAnimationsCount = animationsList.Count;

            int i = 0;
            foreach (AnimationUI animation in animationsList)
            {
                animation.ContentSource = null;
            }

            foreach (AnimationUI animation in animationsList)
            {
                animation.ContentSource = uisList[i++];
            }
            i = animationsList.Count;
            while (i < visibleUIsTargetCount && locationsList.Count > i)
            {
                AddVisible(uisList[i++]);
            }

            //   Canvas.SetLeft(LeftButton, 0);
            //   Canvas.SetLeft(RightButton, base.Width - RightButton.Width);
            Canvas.SetTop(RightButton, RightButton.Height);
            Canvas.SetTop(LeftButton, base.Height - 2 * RightButton.Height);
        }

        public void AddUI(UIElement newUI)
        {
            locationsList[locationsList.Count - 1] = newUI;
            locationsList.Add(new Canvas());

            uisList[uisList.Count - 1] = newUI;
            uisList.Add(new Canvas());

            if (locationsList.Count > visibleUIsTargetCount)
                RightButton.IsDisabled = false;
            if (visibleUIsTargetCount >= locationsList.Count)
            {
                animatedUIsCount++;
                /*Width = (animatedUIsCount - 2) * (uiWidth + (2 * Distance)) + LeftButton.Width + RightButton.Width;
                LayoutRoot.Width = Width;
                dataCanvas.Width = Width - LeftButton.Width - RightButton.Width;
                clippingRect.Width = Width - LeftButton.Width - RightButton.Width;*/

                Height = (animatedUIsCount - 2) * (uiHeight + (2 * Distance)) + LeftButton.Height + RightButton.Height;
                LayoutRoot.Height = Height;
                dataCanvas.Height = Height - LeftButton.Height - RightButton.Height;
                clippingRect.Height = Height - LeftButton.Height - RightButton.Height;

                RefreshSize();
                RefreshVisibleImagesCount();
                clippingGeometry.Rect = clippingRect;
            }

            animationsList[animationsList.Count - 1].ContentSource = uisList[animationsList.Count - 1];
        }

        private void ReloadVisibleImages()
        {
            while (offset + animationsList.Count > uisList.Count)
            {
                uisList[uisList.Count - 1] = locationsList[uisList.Count - 1];

                uisList.Add(new Canvas());
            }
            uisList[uisList.Count - 1] = locationsList[uisList.Count - 1];


            for (int i = 0; i < animationsList.Count; i++)
            {
                animationsList[i].ContentSource = null;
            }
            for (int i = 0; i < animationsList.Count; i++)
            {
                animationsList[i].ContentSource = uisList[i + offset];
            }
        }

        private void AddVisible(UIElement newUI)
        {
            AnimationUI newImage = new AnimationUI();
            dataCanvas.Children.Add(newImage);
            newImage.ContentSource = newUI;
            newImage.Height = this.UIHeight;
            newImage.Width = this.UIWidth;
            //   newImage.SetValue(Canvas.TopProperty, Distance);
            //   newImage.SetValue(Canvas.LeftProperty, Distance + (this.animationsList.Count - 1) * (this.UIWidth + 2 * Distance));

            newImage.SetValue(Canvas.LeftProperty, Distance);
            newImage.SetValue(Canvas.TopProperty, Distance + (this.animationsList.Count - 1) * (this.UIHeight + 2 * Distance));

            //newImage.AnimateBy = uiWidth + 2 * Distance;
            newImage.AnimateBy = uiHeight + 2 * Distance;

            newImage.AnimationDuration = new Duration(TimeSpan.FromMilliseconds(scrollingTime));
            newImage.Click += new EventHandler<MouseEventArgs>(UIClicked);
            newImage.AnimationCompleted += new EventHandler<AnimationUIEventArgs>(UIAnimationCompleted);
            animationsList.Add(newImage);
            readyAnimationsCount = animationsList.Count;
        }

        private void RefreshSize()
        {
            //  Height = uiHeight + 2 * Distance;
            Width = uiWidth;// +2 * Distance;
            //   LeftButton.Height = Height;
            //   RightButton.Height = Height;

            LeftButton.Width = Width;
            RightButton.Width = Width;

            //Width = (animatedUIsCount - 2) * (uiWidth + (2 * Distance)) + LeftButton.Width + RightButton.Width;
            Height = (animatedUIsCount - 2) * (uiHeight + (2 * Distance)) + LeftButton.Height + RightButton.Height;

            /*LayoutRoot.Width = Width;
            dataCanvas.Width = Width - LeftButton.Width - RightButton.Width;
            clippingRect.Width = Width - LeftButton.Width - RightButton.Width;*/

            LayoutRoot.Height = Height;
            dataCanvas.Height = Height - LeftButton.Height - RightButton.Height;
            clippingRect.Height = Height - LeftButton.Height - RightButton.Height;

            clippingGeometry.Rect = clippingRect;

            for (int i = 0; i < animationsList.Count; i++)
            {
                AnimationUI current = animationsList[i];
                current.Height = this.UIHeight;
                current.Width = this.UIWidth;
                // current.AnimateBy = uiWidth + 2 * Distance;
                current.AnimateBy = uiHeight + 2 * Distance;
                // current.SetValue(Canvas.TopProperty, Distance);
                //  current.SetValue(Canvas.LeftProperty, Distance + (i - 1) * (this.UIWidth + 2 * Distance));
                current.SetValue(Canvas.LeftProperty, Distance);
                current.SetValue(Canvas.TopProperty, Distance + (i - 1) * (this.UIHeight + 2 * Distance));
            }

            //dataCanvas.SetValue(Canvas.LeftProperty, RightButton.Width);
            dataCanvas.SetValue(Canvas.TopProperty, RightButton.Height);
        }

        private void MoveToLeft(object sender, MouseEventArgs e)
        {
            // current animanition is not finished
            if (readyAnimationsCount != animationsList.Count)
                return;
            // animate
            if (offset > 0)
            {
                offset--;
                SelectedUI += 1;
                /*   if (SelectedUI < animatedUIsCount && SelectedUI > -1)
                       selectionFrame.AnimateRight();
                 */

                readyAnimationsCount = 0;
                isReloading = true;
                foreach (AnimationUI i in animationsList)
                    i.AnimateRight();
                RightButton.IsDisabled = false;
            }
            if (offset == 0) // disable when selection is in the end
                LeftButton.IsDisabled = true;

            return;
        }

        private void MoveToRight(object sender, MouseEventArgs e)
        {
            // current animanition is not finished
            if (readyAnimationsCount != animationsList.Count)
                return;
            if (offset + animatedUIsCount < locationsList.Count)
            {
                offset++;
                SelectedUI -= 1;
                /*  if (SelectedUI >= -1 && SelectedUI < animatedUIsCount - 1)
                      selectionFrame.AnimateLeft();// slide the selection if it is visible
                  */
                readyAnimationsCount = 0;
                isReloading = true;
                foreach (AnimationUI i in animationsList)
                    i.AnimateLeft();
                LeftButton.IsDisabled = false;
            }
            if (visibleUIsTargetCount + offset == locationsList.Count)
                RightButton.IsDisabled = true;
        }

        void UIClicked(object sender, MouseEventArgs e)
        {
            // SelectedUI = (int)(e.GetPosition(dataCanvas).X / (uiWidth + 2 * Distance));
            /*  selectionFrame.Reset();
              Canvas.SetLeft(selectionFrame, (int)(SelectedUI * (uiWidth + 2 * Distance)));*/
        }

        private int SelectedUI
        {
            get { return selectedUIIndex; }
            set
            {
                selectedUIIndex = value;
                if (this.SelectionChanged != null)
                    this.SelectionChanged(this, new SelectedItemChanged(offset + selectedUIIndex));
            }
        }

        private void UIAnimationCompleted(object sender, AnimationUIEventArgs e)
        {
            animationsList[readyAnimationsCount++].Reset();
            if (isReloading)
            {
                isReloading = false;
                ReloadVisibleImages();
            }
        }


        private new double Height
        {
            get { return base.Height; }
            set
            {
                base.Height = value;
                /*    LayoutRoot.Height = value;
                    dataCanvas.Height = value;
                    clippingRect.Height = value;*/
            }
        }

        private new double Width
        {
            get { return base.Width; }
            set
            {
                base.Width = value;
                LayoutRoot.Width = value;
                dataCanvas.Width = value;
                clippingRect.Width = value;
            }
        }

    }
}
