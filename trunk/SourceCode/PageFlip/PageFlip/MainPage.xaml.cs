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
using System.Windows.Threading;
using System.Windows.Media.Imaging;
using PageFlipUltis;

namespace PageFlip
{
    public partial class MainPage : UserControl
    {
        private double angleSB2RF; //Angle Spine Bottom two Raw Flow
        private double angleST2C; //Angle Spine Top two Centre

        private double tangentToCornerAngle;
        private double tanAngle;

        private GeneralTransform transform;

        private Point corner;
        private DispatcherTimer dtTransationTimer = new DispatcherTimer();
        private double distanceToFollow;
        private double dx;
        private double dy;
        private int nextImageIndex = 0;
        private int transMaxCount = 60;
        private int transCurCount = 0;
        private bool IsTransitionStarted = false;
        private Point maskSize = new Point(0.0, 0.0);
        private int PageContentCurrentIndex;
        private double pageHalfHeight;
        private double pageHeight;
        private double pageDiagonal;
        private double pageWidth;
        private Point spineBottom;
        private double sbScale = 1.0;

        private Point spineTop;
        //private List<BitmapImage> PageContents = new List<BitmapImage>();
        private List<string> PageContents = new List<string>();
        private Point bisector;
        private double fixedRadius;
        private Point follow;
        private Point radius1;
        private Point mouseMovePosition;
        private double bisectorTanget;
        private Point mouse;
        private Point tangentBottom;
        private DateTime doubleClickDuration;
        private double bisectorAngle;
        private int ImageMaxCount = 5;
        // Methods
        public MainPage()
        {
            this.InitializeComponent();
            base.Loaded += new RoutedEventHandler(this.MainPage_Loaded);
        }

        private void checkTransition()
        {
            if (this.IsTransitionStarted && (this.transCurCount++ == this.transMaxCount))
            {
                this.endTransition();
            }
        }

        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            this.update();
        }

        private void dt_Tick(object sender, EventArgs e)
        {
            try
            {
                this.IsTransitionStarted = false;
                //this.Page2SheetSection2.sheetImage.Source = this.PageContents[this.PageContentCurrentIndex];
                //this.Page1TraceSheet.sheetImage.Source = this.PageContents[this.PageContentCurrentIndex];

                //this.Page2SheetSection2.sheetImage = null;
                //this.Page1TraceSheet.sheetImage = null;

                //this.Page2SheetSection2.sheetImage = (Image)this.PageContents[this.PageContentCurrentIndex];
                //this.Page1TraceSheet.sheetImage = (Image)this.PageContents[this.PageContentCurrentIndex];

                this.Page2SheetSection2.sheetImage.Children.Clear();
                this.Page1TraceSheet.sheetImage.Children.Clear();

                this.Page2SheetSection2.sheetImage.Children.Add(Ultis.LoadXamlFromString(this.PageContents[this.PageContentCurrentIndex]));
                this.Page1TraceSheet.sheetImage.Children.Add(Ultis.LoadXamlFromString(this.PageContents[this.PageContentCurrentIndex]));

                this.mouse = new Point(this.pageWidth - 1.0, this.pageHalfHeight - 1.0);
                this.follow = new Point(this.pageWidth - 1.0, this.pageHalfHeight - 1.0);
                this.dtTransationTimer.Stop();
            }
            catch (Exception ex)
            {
                MessageBox.Show("dt_Tick: " + ex.Message);
            }
        }

        private void endTransition()
        {
            this.updateImages();
        }

        private void PageCorner_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!this.IsTransitionStarted)
            {
                this.mouse = new Point(this.pageWidth - 1.0, this.pageHalfHeight - 1.0);
            }
        }

        private void PageCorner_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!this.IsTransitionStarted)
            {
                this.doubleClickDuration = DateTime.Now.AddSeconds(2.0);
                this.mouse = e.GetPosition(this.rutieow);
                this.PageCorner.CaptureMouse();
            }
        }

        private void PageCorner_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!this.IsTransitionStarted)
            {
                this.PageCorner.ReleaseMouseCapture();
                if ((DateTime.Now < this.doubleClickDuration) && ((this.mouse.X > 0.0) && (this.mouse.Y > 0.0)))
                {
                    this.startTransition();
                    this.mouse = new Point(-this.pageWidth, this.pageHalfHeight);
                }
                else if (this.mouse.X < 0.0)
                {
                    this.startTransition();
                    this.mouse = new Point(-this.pageWidth, this.pageHalfHeight);
                }
                else
                {
                    this.mouse = new Point(this.pageWidth - 1.0, this.pageHalfHeight - 1.0);
                }
            }
        }

        private void PageCorner_MouseMove(object sender, MouseEventArgs e)
        {
            if (!this.IsTransitionStarted)
            {
                this.mouseMovePosition = e.GetPosition(this.rutieow);
                if ((this.mouseMovePosition.X > this.pageWidth) && (this.mouseMovePosition.Y > this.pageHalfHeight))
                {
                    this.PageCorner.ReleaseMouseCapture();
                    this.mouse = new Point(this.pageWidth - 1.0, this.pageHalfHeight - 1.0);
                }
                else
                {
                    this.mouse = e.GetPosition(this.rutieow);
                }
            }
        }

        private void loadInitialImages()
        {
            for (int i = 0; i < this.ImageMaxCount; i++)
            {
                string uriString = "images/page" + i.ToString() + ".jpg";

                //this.PageContents.Add(new BitmapImage(new Uri(uriString, UriKind.RelativeOrAbsolute)));
                //Image img = new Image();
                //img.Source = new BitmapImage(new Uri(uriString, UriKind.RelativeOrAbsolute));
                //this.PageContents.Add(img);

                string xaml = string.Format(@"<Image x:Name='imgTest' xmlns='http://schemas.microsoft.com/client/2007'
     xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'

	 Width='592' 
	 Source='images/page{0}.jpg' />", i.ToString());
                this.PageContents.Add(xaml);
            }

            this.nextImageIndex = -1;
            this.dtTransationTimer.Interval = TimeSpan.FromMilliseconds(20.0);
            this.dtTransationTimer.Tick += new EventHandler(this.dt_Tick);
            this.updateImages();
            this.dtTransationTimer.Start();
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                this.updateImages();
                this.setupUI();
                CompositionTarget.Rendering += new EventHandler(this.CompositionTarget_Rendering);
            }
            catch (Exception ex)
            {
                MessageBox.Show("MainPage_Loaded: " + ex.Message);
            }
        }

        private void setupUI()
        {
            this.pageWidth = 300.0;
            this.pageHeight = 425.0;

            this.pageHalfHeight = this.pageHeight * 0.5;

            this.rutieow.SetValue(Canvas.LeftProperty, this.pageWidth);
            this.rutieow.SetValue(Canvas.TopProperty, this.pageHalfHeight);

            this.PageCorner.SetValue(Canvas.LeftProperty, this.pageWidth - 100.0);
            this.PageCorner.SetValue(Canvas.TopProperty, this.pageHalfHeight - 100.0);

            this.spineTop = new Point(0.0, -this.pageHalfHeight);


            this.spineBottom = new Point(0.0, this.pageHalfHeight);
            this.fixedRadius = this.pageWidth;

            this.maskSize.X = this.pageWidth;
            this.maskSize.Y = this.pageHeight * 1.6;
            this.mainMask.Width = this.maskSize.X;
            this.mainMask.Height = this.maskSize.Y;
            this.mainMask.SetValue(Canvas.TopProperty, this.pageHeight - (this.maskSize.Y * 0.8));

            this.mouse.X = this.pageWidth - 1.0;
            this.mouse.Y = this.pageHalfHeight - 1.0;

            this.follow.X = this.mouse.X;
            this.follow.Y = this.mouse.Y;
            this.corner.X = this.mouse.X;
            this.corner.Y = this.mouse.Y;

            this.PageCorner.MouseLeftButtonDown += new MouseButtonEventHandler(this.PageCorner_MouseLeftButtonDown);
            this.PageCorner.MouseMove += new MouseEventHandler(this.PageCorner_MouseMove);
            this.PageCorner.MouseLeftButtonUp += new MouseButtonEventHandler(this.PageCorner_MouseLeftButtonUp);
            this.PageCorner.MouseLeave += new MouseEventHandler(this.PageCorner_MouseLeave);
            this.loadInitialImages();
            this.updateImages();
        }

        private void startTransition()
        {
            this.IsTransitionStarted = true;
            this.transCurCount = 0;
        }

        private void update()
        {
            // THIS IS THE RAW FOLLOW
            this.follow.X += (this.mouse.X - this.follow.X) * 0.12;
            this.follow.Y += (this.mouse.Y - this.follow.Y) * 0.12;

            // DETERMINE ANGLE FROM SPINE BOTTOM TO RAW FOLLOW
            this.angleSB2RF = Math.Atan2(this.spineBottom.Y - this.follow.Y, this.follow.X);

            // PLOT THE FIXED RADIUS FOLLOW
            this.radius1.X = Math.Cos(this.angleSB2RF) * this.fixedRadius;
            this.radius1.Y = this.spineBottom.Y - (Math.Sin(this.angleSB2RF) * this.fixedRadius);

            // DETERMINE THE SHORTER OF THE TWO DISTANCES
            double distanceToFollow = Math.Sqrt(((this.spineBottom.Y - this.follow.Y) * (this.spineBottom.Y - this.follow.Y)) + (this.follow.X * this.follow.X));
            double distToRadius1 = Math.Sqrt(((this.spineBottom.Y - this.radius1.Y) * (this.spineBottom.Y - this.radius1.Y)) + (this.radius1.X * this.radius1.X));
            if (distToRadius1 < distanceToFollow)
            {
                this.corner.X = this.radius1.X;
                this.corner.Y = this.radius1.Y;
            }
            else
            {
                this.corner.X = this.follow.X;
                this.corner.Y = this.follow.Y;
            }

            // NOW CHECK FOR THE OTHER CONSTRAINT, FROM THE SPINE TOP TO THE RADIUS OF THE PAGE DIAMETER...
            this.dx = this.spineTop.X - this.corner.X;
            this.dy = this.corner.Y + this.pageHalfHeight;

            this.distanceToFollow = Math.Sqrt((this.dx * this.dx) + (this.dy * this.dy));
            this.pageDiagonal = Math.Sqrt((this.pageWidth * this.pageWidth) + (this.pageHeight * this.pageHeight));
            if (this.distanceToFollow > this.pageDiagonal)
            {
                this.angleST2C = Math.Atan2(this.dy, this.dx);
                this.corner.X = -Math.Cos(this.angleST2C) * this.pageDiagonal;
                this.corner.Y = this.spineTop.Y + (Math.Sin(this.angleST2C) * this.pageDiagonal);
            }

            // CALCULATE THE BISECTOR AND CREATE THE CRITICAL TRIANGLE
            // DETERMINE THE MIDSECTION POINT

            this.bisector.X = this.corner.X + (0.5 * (this.pageWidth - this.corner.X));
            this.bisector.Y = this.corner.Y + (0.5 * (this.pageHalfHeight - this.corner.Y));
            this.bisectorAngle = Math.Atan2(this.pageHalfHeight - this.bisector.Y, this.pageWidth - this.bisector.X);
            this.bisectorTanget = this.bisector.X - (Math.Tan(this.bisectorAngle) * (this.pageHalfHeight - this.bisector.Y));
            if (this.bisectorTanget < 0.0)
            {
                this.bisectorTanget = 0.0;
            }

            this.tangentBottom.X = this.bisectorTanget;
            this.tangentBottom.Y = this.pageHalfHeight;

            this.tanAngle = Math.Atan2(this.pageHalfHeight - this.bisector.Y, this.bisector.X - this.bisectorTanget);

            // DETERMINE THE tangentToCorner FOR THE ANGLE OF THE PAGE
            this.tangentToCornerAngle = Math.Atan2(this.tangentBottom.Y - this.corner.Y, this.tangentBottom.X - this.corner.X);

            // VISUALIZE THE CLIPPING RECTANGLE
            this.maskAngle.Angle = (90.0 * (this.tanAngle / Math.Abs(this.tanAngle))) - ((this.tanAngle * 180.0) / Math.PI);
            this.mainMask.SetValue(Canvas.LeftProperty, this.pageWidth + (this.bisectorTanget - this.maskSize.X));

            this.Page2SheetSection2.X = this.pageWidth + this.corner.X;
            this.Page2SheetSection2.Y = this.pageHalfHeight + this.corner.Y;
            this.Page2SheetSection2.Angle.Angle = (this.tangentToCornerAngle * 180.0) / Math.PI;

            this.transform = this.mainMask.TransformToVisual(this.cavBook);
            this.ClipPathCloseFigure.StartPoint = this.transform.Transform(new Point(0.0, -100.0));
            this.cpp20.Point = this.transform.Transform(new Point(this.mainMask.Width, -100.0));
            this.cpp02.Point = this.transform.Transform(new Point(this.mainMask.Width, this.mainMask.Height));
            this.cpp101.Point = this.transform.Transform(new Point(0.0, this.mainMask.Height));
            this.UpdatePage();

            this.transform = this.dropShadow.TransformToVisual(this.cavBook);
            this.baclpz.StartPoint = this.transform.Transform(new Point(-2.0, 0.0));
            this.baclzz.Point = this.transform.Transform(new Point(600.0, 0.0));
            this.baclzyy.Point = this.transform.Transform(new Point(600.0, this.pageHeight));
            this.baclzyrq.Point = this.transform.Transform(new Point(-2.0, this.pageHeight));
            this.checkTransition();
        }

        private void updateImages()
        {
            if (this.PageContents.Count != 0)
            {
                this.nextImageIndex++;
                if (this.nextImageIndex >= this.PageContents.Count)
                {
                    this.nextImageIndex = 0;
                }
                this.PageContentCurrentIndex = this.nextImageIndex + 1;
                if (this.PageContentCurrentIndex >= this.PageContents.Count)
                {
                    this.PageContentCurrentIndex = 0;
                }
                //this.Page1Sheet.sheetImage.Source = this.PageContents[this.nextImageIndex];
                this.Page1Sheet.sheetImage.Children.Clear();
                this.Page1Sheet.sheetImage.Children.Add(Ultis.LoadXamlFromString(this.PageContents[this.nextImageIndex]));
                this.dtTransationTimer.Start();
            }
        }

        private void UpdatePage()
        {
            this.shadowspineAngle.Angle = this.maskAngle.Angle;
            this.sbScale = (this.pageWidth - this.corner.X) / this.pageWidth;
            this.sbScale = Math.Min(1.0, Math.Max(this.sbScale, 0.02));
            this.shadowspineImage.Opacity = 0.9 - (this.sbScale * 0.5);
            this.shadowspineScale.ScaleX = 0.8 * this.sbScale;
            this.shadowspineImage.SetValue(Canvas.LeftProperty, this.bisectorTanget);
            this.dropShadow.SetValue(Canvas.LeftProperty, this.bisectorTanget + this.pageWidth);
            this.dropShadowAngle.Angle = this.shadowspineAngle.Angle;
            this.dropShadow.Opacity = 1.0 + (this.corner.X / this.pageWidth);
            this.Page2SheetSection2.curlShadowRotate.Angle = this.maskAngle.Angle - this.Page2SheetSection2.Angle.Angle;
            this.Page2SheetSection2.curlShadow.SetValue(Canvas.LeftProperty, -this.bisectorTanget - 5.0);
            this.Page2SheetSection2.curlShadow.Opacity = this.dropShadow.Opacity * 2.0;
        }
    }

}
