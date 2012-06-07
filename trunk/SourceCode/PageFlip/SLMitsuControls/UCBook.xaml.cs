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

        public void UpdateInterface(UpdateInterfaceParams pars)
        {
            UpdateLeftRightPage(BookLoader.Instance().PreviousPageLeftPart, BookLoader.Instance().PreviousPageRightPart, BookLoader.Instance().CurrentPageLeftPage, BookLoader.Instance().CurrentPageRightPage, BookLoader.Instance().NextPageLeftPart, BookLoader.Instance().NextPageRightPart);

            switch (pars.Type)
            {
                case TurnType.TurnFromRight:
                    {
                        this.AnimateToNextPage(500);
                        break;
                    }
                case TurnType.TurnFromLeft:
                    {
                        this.AnimateToPreviousPage(500);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        public void OnCompleteAnimation()
        {
            try
            {
                BookLoader.Instance().UpdatePrePageToCurrentPage();

                UpdateLeftRightPage(BookLoader.Instance().PreviousPageLeftPart, BookLoader.Instance().PreviousPageRightPart, BookLoader.Instance().CurrentPageLeftPage, BookLoader.Instance().CurrentPageRightPage, BookLoader.Instance().NextPageLeftPart, BookLoader.Instance().NextPageRightPart);
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
        public void UpdateLeftRightPage(UIElement previousLeft, UIElement previousRight, UIElement currentLeft, UIElement currentRight, UIElement nextLeft, UIElement nextRight)
        {
            leftPage.Page0.Children.Clear();// = null;
            leftPage.Page1.Children.Clear();// = null;
            leftPage.Page2.Children.Clear();// = null;

            rightPage.Page0.Children.Clear();// = null;
            rightPage.Page1.Children.Clear();// = null;
            rightPage.Page2.Children.Clear();// = null;

            if (currentLeft == null) currentLeft = createBlankPage(this.Width / 2, this.Height);
            if (previousLeft == null) previousLeft = createBlankPage(this.Width / 2, this.Height);
            if (previousRight == null) previousRight = createBlankPage(this.Width / 2, this.Height);

            if (currentRight == null) currentRight = createBlankPage(this.Width / 2, this.Height);
            if (nextLeft == null) nextLeft = createBlankPage(this.Width / 2, this.Height);
            if (nextRight == null) nextRight = createBlankPage(this.Width / 2, this.Height);

            leftPage.Page0.Children.Add(currentLeft);

            leftPage.Page1.Children.Add(previousRight);
            leftPage.Page2.Children.Add(previousLeft);

            rightPage.Page0.Children.Add(currentRight);

            rightPage.Page1.Children.Add(nextLeft);
            rightPage.Page2.Children.Add(nextRight);
        }
        
    }
    //public interface IDataProvider
    //{
    //    object GetItem(int index);
    //    int GetCount();
    //}
    //public delegate void PageTurnedEventHandler(int leftPageIndex, int rightPageIndex);
}
