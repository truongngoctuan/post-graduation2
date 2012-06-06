using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using DataManager;

namespace SLMitsuControls
{
    public partial class UCBook : UserControl, IBasicTurnPageEffect, IObserver
    {
        public UCBook()
        {
            InitializeComponent();
            leftPage.Attach(this);
            rightPage.Attach(this);

            BookLoader.Instance();//force init data, and download menudata.xml
            BookLoader.Instance().Attach(this);
        }

        public void UpdateInterface()
        {
            //throw new NotImplementedException();
            //MessageBox.Show("MainPage2 - UpdateInterface");
            //rightPagePage0Content = BookLoader.Instance().pa
            UpdateLeftPage(BookLoader.Instance().CurrentPageLeftPage,
                BookLoader.Instance().PreviousPageLeftPart,
                BookLoader.Instance().PreviousPageRightPart);

            UpdateRightPage(BookLoader.Instance().CurrentPageRightPage,
                BookLoader.Instance().NextPageLeftPart,
                BookLoader.Instance().NextPageRightPart);

            this.AnimateToNextPage(500);
        }

        public void OnCompleteAnimation()
        {
            try
            {
                BookLoader.Instance().UpdatePrePageToCurrentPage();

                UpdateLeftPage(BookLoader.Instance().CurrentPageLeftPage,
                    BookLoader.Instance().PreviousPageLeftPart,
                    BookLoader.Instance().PreviousPageRightPart);

                UpdateRightPage(BookLoader.Instance().CurrentPageRightPage,
                    BookLoader.Instance().NextPageLeftPart,
                    BookLoader.Instance().NextPageRightPart);
            }
            catch (Exception ex)
            {
                MessageBox.Show("OnCompleteAnimation: " + ex.Message);
            }
        }

        //private IDataProvider dataProvider;

        //public void SetData(IDataProvider dataProvider)
        //{
        //    //if (this.dataProvider != dataProvider)
        //    {
        //        this.dataProvider = dataProvider;
        //        CurrentSheetIndex = 0;
        //        RefreshSheetsContent();
        //    }
        //}

        private void leftPage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Canvas.SetZIndex(leftPage, 1);
            Canvas.SetZIndex(rightPage, 0);
        }

        private void rightPage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Canvas.SetZIndex(leftPage, 0);
            Canvas.SetZIndex(rightPage, 1);
        }

        private void rightPage_PageTurned(object sender, RoutedEventArgs e)
        {
            //CurrentSheetIndex++;
        }

        private void leftPage_PageTurned(object sender, RoutedEventArgs e)
        {
            //CurrentSheetIndex--;
        }

        //internal object GetPage(int index)
        //{
        //    if ((index >= 0) && (index < dataProvider.GetCount()))
        //        return dataProvider.GetItem(index);

        //    Canvas c = new Canvas();

        //    return c;
        //}

        //private PageStatus _status = PageStatus.None;

        //private int _currentSheetIndex = 0;
        
        //public int CurrentSheetIndex
        //{
        //    get { return _currentSheetIndex; }
        //    set
        //    {
        //        if (_status != PageStatus.None) return;

        //        if (_currentSheetIndex != value)
        //        {
        //            if ((value >= 0) && (value <= dataProvider.GetCount() / 2))
        //            {
        //                _currentSheetIndex = value;
        //                RefreshSheetsContent();
        //            }
        //            else
        //                throw new Exception("Index out of bounds");
        //        }
        //    }
        //}
        //private void RefreshSheetsContent()
        //{
        //    if (dataProvider == null)
        //        return;

        //    object leftPagePage0Content = null;
        //    object leftPagePage1Content = null;
        //    object leftPagePage2Content = null;
        //    object rightPagePage0Content = null;
        //    object rightPagePage1Content = null;
        //    object rightPagePage2Content = null;

        //    int count = dataProvider.GetCount();
        //    int sheetCount = count / 2;
        //    bool isOdd = (count % 2) == 1;

        //    rightPage.IsTopRightCornerEnabled = true;
        //    rightPage.IsBottomRightCornerEnabled = true;

        //    if (_currentSheetIndex == sheetCount)
        //    {
        //        if (isOdd)
        //        {
        //            rightPage.IsTopRightCornerEnabled = false;
        //            rightPage.IsBottomRightCornerEnabled = false;
        //        }
        //    }

        //    if (_currentSheetIndex == 0)
        //    {
        //        leftPagePage0Content = null;
        //        leftPagePage1Content = null;
        //        leftPagePage2Content = null;

        //        Canvas.SetZIndex(leftPage, 1);
        //        Canvas.SetZIndex(rightPage, 0);
        //    }
        //    else
        //    {
        //        leftPagePage0Content = GetPage(2 * (CurrentSheetIndex - 1) + 1);
        //        leftPagePage1Content = GetPage(2 * (CurrentSheetIndex - 1));
        //        leftPagePage2Content = GetPage(2 * (CurrentSheetIndex - 1) - 1);
        //    }

        //    rightPagePage0Content = GetPage(2 * CurrentSheetIndex);
        //    rightPagePage1Content = GetPage(2 * CurrentSheetIndex + 1);
        //    rightPagePage2Content = GetPage(2 * CurrentSheetIndex + 2);

        //    leftPage.Page0.Content = null;
        //    leftPage.Page1.Content = null;
        //    leftPage.Page2.Content = null;
        //    rightPage.Page0.Content = null;
        //    rightPage.Page1.Content = null;
        //    rightPage.Page2.Content = null;

        //    leftPage.Page2.Content = leftPagePage2Content;
        //    leftPage.Page1.Content = leftPagePage1Content;
        //    leftPage.Page0.Content = leftPagePage0Content;
        //    rightPage.Page2.Content = rightPagePage2Content;
        //    rightPage.Page1.Content = rightPagePage1Content;
        //    rightPage.Page0.Content = rightPagePage0Content;

        //    if (OnPageTurned != null)
        //    {
        //        int leftPageIndex = 2 * _currentSheetIndex;
        //        int rightPageIndex = leftPageIndex + 1;

        //        if (_currentSheetIndex == 0)
        //            leftPageIndex = -1;
        //        if ((_currentSheetIndex == count / 2) && !isOdd)
        //            rightPageIndex = -1;

        //        OnPageTurned(leftPageIndex, rightPageIndex);
        //    }
        //}

        //public event PageTurnedEventHandler OnPageTurned;

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //RefreshSheetsContent();
        }

        public void AnimateToNextPage(int duration)
        {
            //if (CurrentSheetIndex + 1 <=  dataProvider.GetCount() / 2)
            {
                Canvas.SetZIndex(leftPage, 0);
                Canvas.SetZIndex(rightPage, 1);
                rightPage.AutoTurnPage(CornerOrigin.BottomRight, duration);
            }
        }

        public void AnimateToPreviousPage(int duration)
        {
            //if (CurrentSheetIndex > 0)
            {
                Canvas.SetZIndex(rightPage, 0);
                Canvas.SetZIndex(leftPage, 1);
                leftPage.AutoTurnPage(CornerOrigin.BottomLeft, duration);
            }
        }

        static UIElement createBlankPage(double Width, double Height)
        {
            Canvas cv = new Canvas() { Width = Width, Height = Height, Background = new SolidColorBrush(Colors.White) };
            return cv;
        }
        public void UpdateLeftPage(UIElement P0, UIElement P1, UIElement P2)
        {
            leftPage.Page0.Content = null;
            leftPage.Page1.Content = null;
            leftPage.Page2.Content = null;

            if (P0 == null) P0 = createBlankPage(this.ActualWidth / 2, this.ActualHeight);
            if (P1 == null) P1 = createBlankPage(this.ActualWidth / 2, this.ActualHeight);
            if (P2 == null) P2 = createBlankPage(this.ActualWidth / 2, this.ActualHeight);

            leftPage.Page0.Content = P0;
            leftPage.Page1.Content = P1;
            leftPage.Page2.Content = P2;
        }
        public void UpdateRightPage(UIElement P0, UIElement P1, UIElement P2)
        {
            rightPage.Page0.Content = null;
            rightPage.Page1.Content = null;
            rightPage.Page2.Content = null;

            if (P0 == null) P0 = createBlankPage(this.Width / 2, this.Height);
            if (P1 == null) P1 = createBlankPage(this.Width / 2, this.Height);
            if (P2 == null) P2 = createBlankPage(this.Width / 2, this.Height);

            rightPage.Page0.Content = P0;
            rightPage.Page1.Content = P1;
            rightPage.Page2.Content = P2;

        }
    }
    //public interface IDataProvider
    //{
    //    object GetItem(int index);
    //    int GetCount();
    //}
    //public delegate void PageTurnedEventHandler(int leftPageIndex, int rightPageIndex);
}
