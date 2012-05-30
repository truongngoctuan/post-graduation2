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
using PageFlip.DataLoader;

namespace PageFlip
{
    public enum UpdatePageTransition
    {
        Default,
        NextPage,
        PreviousPage
    }
    public partial class MainPage : UserControl, IObserver
    {
        private double angleSB2RF; //Angle Spine Bottom two Raw Flow
        private double angleST2C; //Angle Spine Top two Centre

        private double tangentToCornerAngle;
        private double tanAngle;

        private GeneralTransform transform;

        private Point corner;
        //private DispatcherTimer dtTransationTimer = new DispatcherTimer();
        private double distanceToFollow;
        private double dx;
        private double dy;
        //private int iCurrentPageContent = 0;
        private int transMaxCount = 60;
        private int transCurCount = 0;
        private bool IsTransitionStarted = false;
        private Point maskSize = new Point(0.0, 0.0);
        //private int iNextPageContent;
        private double pageHalfHeight;
        private double pageHeight;
        private double pageDiagonal;

        private double pageWidth;
        private double pageHalfWidth;

        //fixed point ofpages such as topleft, topbottom, cornerposition...
        private Point FixedPoint_CornerBottomRight;

        private Point spineBottom;
        private double sbScale = 1.0;

        private Point spineTop;
        //private List<BitmapImage> PageContents = new List<BitmapImage>();
        //private List<string> PageContents = new List<string>();
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

        //BookLoader BookData;
        // Methods
        public MainPage()
        {
            this.InitializeComponent();
            base.Loaded += new RoutedEventHandler(this.MainPage_Loaded);

            //ServiceReference1.Service1Client ws = new ServiceReference1.Service1Client();

            //ws.Get_AllArticlesCompleted += new EventHandler<ServiceReference1.Get_AllArticlesCompletedEventArgs>(ws_Get_AllArticlesCompleted);
            //ws.Get_AllArticlesAsync();
        }

        void ws_Get_AllArticlesCompleted(object sender, ServiceReference1.Get_AllArticlesCompletedEventArgs e)
        {
            //MessageBox.Show(e.Result.Count.ToString());
        }

        private void checkTransition()
        {
            if (this.IsTransitionStarted && (this.transCurCount++ == this.transMaxCount))
            {
                //this.endTransition();
                this.updateImages();
                this.IsTransitionStarted = false;
            }
        }

        private void setupUI()
        {
            mainMask.Visibility = Visibility.Collapsed;
            Page1Sheet.curlShadow.Visibility = Visibility.Collapsed;
            //BookData = new BookLoader();
            BookLoader.Instance();//force init data, and download menudata.xml
            BookLoader.Instance().Attach(this);

            this.pageWidth = 1200;// 900;
            this.pageHeight = 600;

            this.pageHalfWidth = this.pageWidth * 0.5;
            this.pageHalfHeight = this.pageHeight * 0.5;

            BookSizes bs = new BookSizes()
            {
                layoutRootW = pageWidth,
                layoutRootH = pageHeight,
                layoutRootHalfH = pageHeight / 2,
                layoutRootHalfW = pageHalfWidth
            };

            this.LayoutRoot.DataContext = bs;

            this.rutieow.SetValue(Canvas.LeftProperty, this.pageHalfWidth);
            this.rutieow.SetValue(Canvas.TopProperty, this.pageHalfHeight);

            this.PageCorner.SetValue(Canvas.LeftProperty, this.pageHalfWidth - 100.0);
            this.PageCorner.SetValue(Canvas.TopProperty, this.pageHalfHeight - 100.0);

            this.spineTop = new Point(0.0, -this.pageHalfHeight);


            this.spineBottom = new Point(0.0, this.pageHalfHeight);
            this.fixedRadius = this.pageHalfWidth;

            this.maskSize.X = this.pageHalfWidth;
            this.maskSize.Y = this.pageHeight * 1.6;
            this.mainMask.Width = this.maskSize.X;
            this.mainMask.Height = this.maskSize.Y;
            this.mainMask.SetValue(Canvas.TopProperty, this.pageHeight - (this.maskSize.Y * 0.8));

            this.mouse.X = this.pageHalfWidth - 1.0;
            this.mouse.Y = this.pageHalfHeight - 1.0;

            this.follow.X = this.mouse.X;
            this.follow.Y = this.mouse.Y;
            this.corner.X = this.mouse.X;
            this.corner.Y = this.mouse.Y;

            this.PageCorner.MouseLeftButtonDown += new MouseButtonEventHandler(this.PageCorner_MouseLeftButtonDown);
            this.PageCorner.MouseMove += new MouseEventHandler(this.PageCorner_MouseMove);
            this.PageCorner.MouseLeftButtonUp += new MouseButtonEventHandler(this.PageCorner_MouseLeftButtonUp);
            this.PageCorner.MouseLeave += new MouseEventHandler(this.PageCorner_MouseLeave);

            this.Page1Sheet.Effect = new CustomPixelShader.Effects.GutterBookEffect();
            this.Page2SheetSection2.Effect = new CustomPixelShader.Effects.GutterBookEffect();
            this.Page1TraceSheet.Effect = new CustomPixelShader.Effects.GutterBookEffect();
        }


        public void UpdateInterface()
        {//update book layout if something change in bookdata
            if (BookLoader.Instance().IsRightToLeftTransition)
            {
                Dispatcher.BeginInvoke(() => ChangePageBeforeTransition(this, new EventArgs()));
                //TypeTransition = UpdatePageTransition.NextPage;
                this.startTransition();
                this.mouse = new Point(-this.pageHalfWidth, this.pageHalfHeight);
            }
            else
            {
                Dispatcher.BeginInvoke(() => ChangePageBeforeTransition(this, new EventArgs()));
                //TypeTransition = UpdatePageTransition.PreviousPage;
                this.startTransition();
                this.mouse = new Point(-this.pageHalfWidth, this.pageHalfHeight);
            }
            
        }

        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {//this is the updater

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
            this.pageDiagonal = Math.Sqrt((this.pageHalfWidth * this.pageHalfWidth) + (this.pageHeight * this.pageHeight));
            if (this.distanceToFollow > this.pageDiagonal)
            {
                this.angleST2C = Math.Atan2(this.dy, this.dx);
                this.corner.X = -Math.Cos(this.angleST2C) * this.pageDiagonal;
                this.corner.Y = this.spineTop.Y + (Math.Sin(this.angleST2C) * this.pageDiagonal);
            }

            // CALCULATE THE BISECTOR AND CREATE THE CRITICAL TRIANGLE
            // DETERMINE THE MIDSECTION POINT

            this.bisector.X = this.corner.X + (0.5 * (this.pageHalfWidth - this.corner.X));
            this.bisector.Y = this.corner.Y + (0.5 * (this.pageHalfHeight - this.corner.Y));
            this.bisectorAngle = Math.Atan2(this.pageHalfHeight - this.bisector.Y, this.pageHalfWidth - this.bisector.X);
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
            this.mainMask.SetValue(Canvas.LeftProperty, this.pageHalfWidth + (this.bisectorTanget - this.maskSize.X));

            this.Page2SheetSection2.X = this.pageHalfWidth + this.corner.X;
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
            //this.baclzz.Point = this.transform.Transform(new Point(600.0, 0.0));
            //this.baclzyy.Point = this.transform.Transform(new Point(600.0, this.pageHeight));
            this.baclzz.Point = this.transform.Transform(new Point(this.pageWidth, 0.0));
            this.baclzyy.Point = this.transform.Transform(new Point(this.pageWidth, this.pageHeight));
            this.baclzyrq.Point = this.transform.Transform(new Point(-2.0, this.pageHeight));
            this.checkTransition();

        }


        private void PageCorner_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!this.IsTransitionStarted)
            {
                if (!bCanTransitionRight) return;
                this.mouse = new Point(this.pageHalfWidth - 1.0, this.pageHalfHeight - 1.0);
            }
        }

        private void PageCorner_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!this.IsTransitionStarted)
            {
                if (!bCanTransitionRight) return;
                this.doubleClickDuration = DateTime.Now.AddSeconds(2.0);
                this.mouse = e.GetPosition(this.rutieow);
                this.PageCorner.CaptureMouse();
            }
        }

        private void PageCorner_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!this.IsTransitionStarted)
            {
                if (!bCanTransitionRight) return;
                this.PageCorner.ReleaseMouseCapture();
                if ((DateTime.Now < this.doubleClickDuration) && ((this.mouse.X > 0.0) && (this.mouse.Y > 0.0)))
                {
                    BookLoader.Instance().OnNextpage();
                    //this.TypeTransition = UpdatePageTransition.NextPage;
                    this.startTransition();
                    this.mouse = new Point(-this.pageHalfWidth, this.pageHalfHeight);
                }
                else if (this.mouse.X < 0.0)
                {
                    BookLoader.Instance().OnNextpage();
                    //this.TypeTransition = UpdatePageTransition.NextPage;
                    this.startTransition();
                    this.mouse = new Point(-this.pageHalfWidth, this.pageHalfHeight);
                }
                else
                {
                    mainMask.Visibility = Visibility.Collapsed;
                    Page1Sheet.curlShadow.Visibility = Visibility.Collapsed;
                    this.mouse = new Point(this.pageHalfWidth - 1.0, this.pageHalfHeight - 1.0);
                }
            }
        }

        private void PageCorner_MouseMove(object sender, MouseEventArgs e)
        {
            mainMask.Visibility = Visibility.Visible;
            Page1Sheet.curlShadow.Visibility = Visibility.Visible;
            if (!this.IsTransitionStarted)
            {
                if (!bCanTransitionRight) return;
                this.mouseMovePosition = e.GetPosition(this.rutieow);
                if ((this.mouseMovePosition.X > this.pageHalfWidth) && (this.mouseMovePosition.Y > this.pageHalfHeight))
                {
                    this.PageCorner.ReleaseMouseCapture();
                    this.mouse = new Point(this.pageHalfWidth - 1.0, this.pageHalfHeight - 1.0);
                }
                else
                {
                    this.mouse = e.GetPosition(this.rutieow);
                }
            }
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                this.setupUI();
                //this.iCurrentPageContent = -1;

                //BookData.UpdateAfterChangeChapter(0);


                //BookData.LoadMenu(0);
//                ChangePageAfterTransition(this, new EventArgs());

                CompositionTarget.Rendering += new EventHandler(this.CompositionTarget_Rendering);
            }
            catch (Exception ex)
            {
                MessageBox.Show("MainPage_Loaded: " + ex.Message);
            }
        }



        private void startTransition()
        {
            this.IsTransitionStarted = true;
            this.transCurCount = 0;
        }

        //UpdatePageTransition TypeTransition = UpdatePageTransition.Default;
        bool bCanTransitionRight = true;

        private void updateImages()
        {
            ////decide what page will be loaded next or previous
            //switch (TypeTransition)
            //{
            //    case UpdatePageTransition.NextPage:
            //        {
            //            //BookLoader.Instance().NextPage();

            //            bCanTransitionRight = BookLoader.Instance().IsCanTransitionRight();
            //            //http://www.silverlightshow.net/items/Tip-Asynchronous-Silverlight-Execute-on-the-UI-thread.aspx
            //            Dispatcher.BeginInvoke(() => ChangePageAfterTransition(this, new EventArgs()));
            //            break;
            //        }
            //    case UpdatePageTransition.PreviousPage:
            //        {
            //            //http://www.silverlightshow.net/items/Tip-Asynchronous-Silverlight-Execute-on-the-UI-thread.aspx
            //            Dispatcher.BeginInvoke(() => ChangePageAfterTransition(this, new EventArgs()));
            //            break;
            //        }
            //    default://tuong duong nextpage
            //        {
            //            break;
            //        }
            //}

            
            //http://www.silverlightshow.net/items/Tip-Asynchronous-Silverlight-Execute-on-the-UI-thread.aspx
            Dispatcher.BeginInvoke(() => ChangePageAfterTransition(this, new EventArgs()));
        }

        private void UpdatePage()
        {
            this.shadowspineAngle.Angle = this.maskAngle.Angle;
            this.sbScale = (this.pageHalfWidth - this.corner.X) / this.pageHalfWidth;
            this.sbScale = Math.Min(1.0, Math.Max(this.sbScale, 0.02));
            this.mainMask.Opacity = 0.9 - (this.sbScale * 0.5);
            this.shadowspineScale.ScaleX = 0.8 * this.sbScale;
            this.shadowspineImage.SetValue(Canvas.LeftProperty, this.bisectorTanget);
            this.dropShadow.SetValue(Canvas.LeftProperty, this.bisectorTanget + this.pageHalfWidth);
            this.dropShadowAngle.Angle = this.shadowspineAngle.Angle;
            this.dropShadow.Opacity = 1.0 + (this.corner.X / this.pageHalfWidth);
            this.Page2SheetSection2.curlShadowRotate.Angle = this.maskAngle.Angle - this.Page2SheetSection2.Angle.Angle;
            this.Page2SheetSection2.curlShadow.SetValue(Canvas.LeftProperty, -this.bisectorTanget - 5.0);
            this.Page2SheetSection2.curlShadow.Opacity = this.dropShadow.Opacity * 2.0;
        }

        private void btNextPage_Click(object sender, RoutedEventArgs e)
        {
            //this.TypeTransition = UpdatePageTransition.NextPage;
            //CurrentArticlePageIndex++;

            this.startTransition();
            this.mouse = new Point(-this.pageHalfWidth, this.pageHalfHeight);
        }

        public UserControl ParentView { get; set; }
        public int ContentPageIndex { get; set; }

        private void btBack_Click(object sender, RoutedEventArgs e)
        {
            CompositionTarget.Rendering -= this.CompositionTarget_Rendering;
            App.GoToPage(this, this.LayoutRoot, this.ParentView);
        }

        private void LayoutRoot_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //StillWait = false;
            //MessageBox.Show(LayoutRoot.ActualWidth.ToString() + " " + LayoutRoot.ActualHeight.ToString());
        }

        private void ChangePageBeforeTransition(object sender, EventArgs e)
        {
            try
            {
                this.Page2SheetSection2.sheetImage.Children.Clear();
                this.Page1TraceSheet.sheetImage.Children.Clear();

                if (BookLoader.Instance().NextPageLeftPart != null)
                    this.Page2SheetSection2.sheetImage.Children.Add(BookLoader.Instance().NextPageLeftPart);
                if (BookLoader.Instance().NextPageRightPart != null)
                    this.Page1TraceSheet.sheetImage.Children.Add(BookLoader.Instance().NextPageRightPart);
            }
            catch (Exception ex)
            {
                MessageBox.Show("ChangePageBeforeTransition: " + ex.Message);
            }
        }

        private void ChangePageAfterTransition(object sender, EventArgs e)
        {
            try
            {
                BookLoader.Instance().UpdatePrePageToCurrentPage();
                bCanTransitionRight = BookLoader.Instance().IsCanTransitionRight();

                this.Page1Sheet.sheetImage.Children.Clear();
                this.Page2SheetSection2.sheetImage.Children.Clear();
                this.Page1TraceSheet.sheetImage.Children.Clear();

                if (BookLoader.Instance().CurrentPage != null)
                    this.Page1Sheet.sheetImage.Children.Add(BookLoader.Instance().CurrentPage);
                if (BookLoader.Instance().NextPageLeftPart != null)
                    this.Page2SheetSection2.sheetImage.Children.Add(BookLoader.Instance().NextPageLeftPart);
                if (BookLoader.Instance().NextPageRightPart != null)
                    this.Page1TraceSheet.sheetImage.Children.Add(BookLoader.Instance().NextPageRightPart);

                //TypeTransition = UpdatePageTransition.Default;

                this.mouse = new Point(this.pageHalfWidth - 1.0, this.pageHalfHeight - 1.0);
                this.follow = new Point(this.pageHalfWidth - 1.0, this.pageHalfHeight - 1.0);

                mainMask.Visibility = Visibility.Collapsed;
                Page1Sheet.curlShadow.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ChangePageAfterTransition: " + ex.Message);
            }
        }

        //int CurrentChapterIndex = 0;
        //int CurrentArticleIndex = 0;
        //int CurrentArticlePageIndex = -1;

        //#region ChangeChapter
        //private void NextChapter(int iIndex)
        //{
        //    BookData.UpdateBeforeChangeChapter(iIndex);
        //    ChangePageAfterTransition(this, new EventArgs());

        //    TypeTransition = UpdatePageTransition.NextMenuPage;
        //    CurrentChapterIndex = iIndex;
        //    CurrentArticleIndex = 0;
        //    CurrentArticlePageIndex = -1;

        //    this.startTransition();
        //    this.mouse = new Point(-this.pageHalfWidth, this.pageHalfHeight);
        //}

        //private void btChapter0_Click(object sender, RoutedEventArgs e)
        //{
        //    NextChapter(0);
        //}

        //private void btChapter1_Click(object sender, RoutedEventArgs e)
        //{
        //    NextChapter(1);
        //}

        //private void btChapter2_Click(object sender, RoutedEventArgs e)
        //{
        //    NextChapter(2);
        //}

        //#endregion

        private void btnFontPlus_Click(object sender, RoutedEventArgs e)
        {
            Grid grd = Ultis.FindVisualChildByName<Grid>(this.LayoutRoot, "pageRoot");

            if (grd != null)
            {
                RichTextBlock rtb = Ultis.FindVisualChildByName<RichTextBlock>(grd, "rtb_01");
                rtb.FontSize++;
            }
        }

        private void btnFontMinus_Click(object sender, RoutedEventArgs e)
        {
            Grid grd = Ultis.FindVisualChildByName<Grid>(this.LayoutRoot, "pageRoot");

            if (grd != null)
            {
                RichTextBlock rtb = Ultis.FindVisualChildByName<RichTextBlock>(grd, "rtb_01");
                rtb.FontSize--;
            }
        }
    }

}
