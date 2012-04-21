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
using System.Threading;
using System.ComponentModel;
using System.Globalization;
using System.Diagnostics.CodeAnalysis;
using Paris.Controls.FoldManager;

namespace Paris.Controls
{
    [TemplateVisualState(Name = "MouseOver", GroupName = "CommonStates"), 
    TemplateVisualState(Name = "Normal", GroupName = "CommonStates"), 
    TemplateVisualState(Name = "Disabled", GroupName = "CommonStates"), 
    StyleTypedProperty(Property = "ItemContainerStyle", StyleTargetType = typeof(BookItem))]

    public class Book : ItemsControl
    {
        // Fields
        private BookItem _dragged;
        private BookItem _fold;
        private bool _ignorePageChange;
        internal bool _isLoaded;
        private DateTime _lastUpdate;
        private bool _rendering;
        private Size _size;
        private bool _throwCurrentPageChanged;
        private bool _throwCurrentZoneChanged;
        private bool _throwIsMouseOverChanged;
        public static readonly DependencyProperty ApplyPageTemplateProperty = DependencyProperty.RegisterAttached("ApplyPageTemplate", typeof(ApplyPageTemplate), typeof(Book), new PropertyMetadata( OnApplyPageTemplatePropertyChanged));
        //private EventHandler<PropertyChangedEventArgs<int>> CurrentPageChanged;
        public static readonly DependencyProperty CurrentPageProperty = DependencyProperty.Register("CurrentPage", typeof(int), typeof(Book), new PropertyMetadata(OnCurrentPagePropertyChanged));
        //private EventHandler<PropertyChangedEventArgs<BookZone>> CurrentZoneChanged;
        public static readonly DependencyProperty CurrentZoneProperty = DependencyProperty.Register("CurrentZone", typeof(BookZone), typeof(Book), new PropertyMetadata(OnCurrentZonePropertyChanged));
        //private EventHandler<BookDragPageFinishedEventArgs> DragPageFinished;
        //private EventHandler DragPageStarted;
        public static readonly DependencyProperty FoldSizeProperty = DependencyProperty.Register("FoldSize", typeof(double), typeof(Book), new PropertyMetadata(40.0, OnFoldSizePropertyChanged));
        public static readonly DependencyProperty IsFirstPageOnTheRightProperty = DependencyProperty.Register("IsFirstPageOnTheRight", typeof(bool), typeof(Book), new PropertyMetadata(OnIsFirstPageOnTheRightPropertyChanged));
        //private EventHandler<PropertyChangedEventArgs<bool>> IsMouseOverChanged;
        public static readonly DependencyProperty IsMouseOverProperty = DependencyProperty.Register("IsMouseOver", typeof(bool), typeof(Book), new PropertyMetadata(OnIsMouseOverPropertyChanged));
        public static readonly DependencyProperty ItemContainerStyleProperty = DependencyProperty.Register("ItemContainerStyle", typeof(Style), typeof(Book), new PropertyMetadata(OnItemContainerStylePropertyChanged));
        public static readonly DependencyProperty LeftPageTemplateProperty = DependencyProperty.Register("LeftPageTemplate", typeof(ControlTemplate), typeof(Book), null);
        public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register("Orientation", typeof(Orientation), typeof(Book), new PropertyMetadata((Orientation)1, OnOrientationPropertyChanged));
        public static readonly DependencyProperty PageFoldActionProperty = DependencyProperty.Register("PageFoldAction", typeof(PageFoldAction), typeof(Book), new PropertyMetadata(PageFoldAction.TurnPageOnClick));
        public static readonly DependencyProperty RightPageTemplateProperty = DependencyProperty.Register("RightPageTemplate", typeof(ControlTemplate), typeof(Book), null);
        public static readonly DependencyProperty ShowInnerShadowsProperty = DependencyProperty.Register("ShowInnerShadows", typeof(bool), typeof(Book), new PropertyMetadata(true, OnShowInnerShadowsPropertyChanged));
        public static readonly DependencyProperty ShowOuterShadowsProperty = DependencyProperty.Register("ShowOuterShadows", typeof(bool), typeof(Book), new PropertyMetadata(true, OnShowOuterShadowsPropertyChanged));
        public static readonly DependencyProperty ShowPageFoldProperty = DependencyProperty.Register("ShowPageFold", typeof(PageFoldVisibility), typeof(Book), new PropertyMetadata(PageFoldVisibility.OnMouseOver, OnShowPageFoldPropertyChanged));
        internal static readonly DependencyProperty TransitionProperty = DependencyProperty.Register("Transition", typeof(BookTransition), typeof(Book), null);
        public static readonly DependencyProperty TurnIntervalProperty = DependencyProperty.Register("TurnInterval", typeof(int), typeof(Book), new PropertyMetadata(300));


        public Book()
        {
            this.DefaultStyleKey = typeof(Book);
            this._throwCurrentPageChanged = true;
            this._throwCurrentZoneChanged = true;
            this._throwIsMouseOverChanged = true;
            this.IsEnabledChanged+=new DependencyPropertyChangedEventHandler(Book_IsEnabledChanged);
            this.MouseEnter += new MouseEventHandler(Book_MouseEnter);
            this.MouseLeave += new MouseEventHandler(Book_MouseLeave);
            this.SetCustomDefaultValues();

            this.Loaded += new RoutedEventHandler(Book_Loaded);
        }

        void Book_Loaded(object sender, RoutedEventArgs e)
        {
            //throw new NotImplementedException();

            //problem: why element == null ??
            //for (int i = 0; i < base.Items.Count; i++)
            //{
            //    BookItem element = BookItemFromIndex(i);
            //    element.index = i;
            //}

        }

        private void SetCustomDefaultValues()
        {
            this.IsTabStop= true;
            this.Transition = new BookFoldTransition();
            MouseHelper helper = new MouseHelper(this);
            helper.MouseDragStart += new EventHandler<MouseDragEventArgs>(this.OnMouseDragStart);
            helper.MouseDragMove += new EventHandler<MouseDragEventArgs>(this.OnMouseDragMove);
            helper.MouseDragEnd += new EventHandler<MouseDragEventArgs>(this.OnMouseDragEnd);
            helper.MouseClick += new MouseEventHandler(this.OnMouseClick);
            helper.MouseDoubleClick += new MouseEventHandler(this.OnMouseDoubleClick);
            this.LayoutUpdated += new EventHandler(OnLayoutUpdated);
            CompositionTarget.Rendering += new EventHandler(CompositionTarget_Rendering); 
        }

        void Book_MouseLeave(object sender, MouseEventArgs e)
        {
            this.IsMouseOver = false;

        }

        void Book_MouseEnter(object sender, MouseEventArgs e)
        {
            this.IsMouseOver = true;

        }

        void Book_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            this.ChangeVisualStateCommon(true);
        }

        private BookItem Back(BookItem item)
        {
            return this.Relative(item, -1, 1);
        }

        private void BeginRendering()
        {
            if (!this._rendering)
            {
                this._lastUpdate = DateTime.Now;
                this._rendering = true;
                //CompositionTarget.Rendering+=new EventHandler(CompositionTarget_Rendering); 
            }
        }

        private BookItem Bottom(BookItem item)
        {
            return this.Relative(item, -2, 2);
        }


  private bool CastShadow(BookItem element)
        {
            BookItem item = this.Top(element);
            if (this.IsVisible(item) && !this.IsTransitioning(item))
            {
                return false;
            }
            if ((this.Bottom(element) != null) && this.IsTransitioning(element))
            {
                return element.TransitionFront;
            }
            return true;
        }

        

        private void CircleTransition(BookItem item, BookZone zone)
    {
        Vector2 center;
        Vector2 start;
        Vector2 target;
        double radius;
        BookItem item2 = this.Back(item);
        if (item2 != null)
        {
            //<>c__DisplayClass6 class2;
            center = new Vector2();
            if (this.Orientation == System.Windows.Controls.Orientation.Horizontal)
            {
                center = ((zone == BookZone.TopLeft) || (zone == BookZone.TopRight)) ? new Vector2(0.5, -0.5) : new Vector2(0.5, 1.5);
            }
            else
            {
                center = ((zone == BookZone.TopLeft) || (zone == BookZone.BottomLeft)) ? new Vector2(-0.5, 0.5) : new Vector2(1.5, 0.5);
            }
            start = new Vector2();
            switch (zone)
            {
                case BookZone.BottomLeft:
                    start = new Vector2(0.0, 1.0);
                    break;

                case BookZone.TopRight:
                    start = new Vector2(1.0, 0.0);
                    break;

                case BookZone.BottomRight:
                    start = new Vector2(1.0, 1.0);
                    break;
            }
            target = VectorHelper.Flip(new Size(1.0, 1.0), item.Direction, start);
            Vector2 vector = center - start;
            radius = vector.Length;
            item2.DragCurrent = item.DragCurrent = item2.DragStart = item.DragStart = start;
            item2.TransitionFront = true;
            item.TransitionFront = false;
            item2.Time = item.Time = 0.0;
            item2.Path = item.Path = (double t) =>
            {
                if (t != 1.0)
                {
                    Vector2 vector2 = ((Vector2)((target * t) + (start * (1.0 - t)))) - center;
                    return (((Vector2)(vector2.Normalized * radius)) + center);
                }
                return target;
            };
            
            this.BeginRendering();
        }
    }

        #region Updater

        /// <summary>
        /// general update function, has respondsibility for:
        ///  + update transition
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            if (this._rendering)
            {
                DateTime now = DateTime.Now;
                TimeSpan span = (TimeSpan)(now - this._lastUpdate);
                double totalMilliseconds = span.TotalMilliseconds;
                this._lastUpdate = now;
                bool bIsAllElementHavePath = false;
                bool bIsAllElementNoTransition = true;
                for (int i = 0; i < base.Items.Count; i++)
                {
                    BookItem element = base.ItemContainerGenerator.ContainerFromIndex(i) as BookItem;
                    if (element == null)
                    {
                        return;
                    }
                    bIsAllElementHavePath = bIsAllElementHavePath || (element.Path != null);
                    this.UpdatePosition(element, totalMilliseconds);
                    bIsAllElementNoTransition = bIsAllElementNoTransition && !this.IsTransitioning(element);
                }
                for (int j = 0; j < base.Items.Count; j++)
                {
                    BookItem item2 = base.ItemContainerGenerator.ContainerFromIndex(j) as BookItem;
                    this.UpdateTransition(item2);
                }
                if (!bIsAllElementHavePath)
                {
                    //this._rendering = false;
                    //CompositionTarget.Rendering -= (new EventHandler(this.CompositionTarget_Rendering));
                    if ((this.ShowPageFold == PageFoldVisibility.Always) && bIsAllElementNoTransition)
                    {
                        this.Fold(BookZone.BottomRight);
                    }
                }
            }
            else
            {
            }
        }

        #endregion

        public static ApplyPageTemplate GetApplyPageTemplate(DependencyObject element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            return (ApplyPageTemplate)element.GetValue(ApplyPageTemplateProperty);
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new BookItem();
        }


        private void InstantUndoFold()
        {
            if (this._fold != null)
            {
                BookItem item = this.Back(this._fold);
                if (item != null)
                {
                    item.TransitionFront = true;
                    this._fold.TransitionFront = false;
                    item.Time = this._fold.Time = 0.0;
                    this._fold.DragCurrent = item.DragCurrent = this._fold.DragStart;
                    item.Path = (Func<double, Vector2>)(this._fold.Path = null);
                    this.BeginRendering();
                }
                this._fold = null;
            }
        }

        private bool IsCurrent(BookItem item)
        {
            if (item == null)
            {
                return false;
            }
            int index = IndexFromBookItem(item);
            index -= this.PagePosition(index);//get index of the left page
            if (this.CurrentPage != index)
            {
                return (this.CurrentPage == (index + 1));
            }
            return true;
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return (item is BookItem);
        }

        private bool IsTransitioning(BookItem item)
        {
            if (item == null)
            {
                return false;
            }
            BookItem item2 = this.Back(item);
            if (item2 == null)
            {
                return false;
            }
            Dock direction = item.TransitionFront ? item.Direction : item2.Direction;
            Vector2 p = VectorHelper.Symmetry(new Size(1.0, 1.0), direction, item.DragStart);
            p = VectorHelper.Flip(new Size(1.0, 1.0), direction, p);
            if (item == null)
            {
                return false;
            }
            return ((item.Path != null) || ((item.DragCurrent != item.DragStart) && (item.DragCurrent != p)));
        }

        private bool IsVisible(BookItem element)
        {
            if (element == null)
            {
                return false;
            }
            BookItem item = this.Top(element);
            return (((this.IsTransitioning(element) || this.IsTransitioning(item)) || this.IsCurrent(element)) || (((item != null) && item.IsTransparent) && this.IsVisible(item)));
        }
        
        private static bool LinearTransition(BookItem item, BookItem backItem, double time, Vector2 target)
        {
            //BookItem backItem = this.Back(item);
            if (backItem != null)
            {
                //<>c__DisplayClass3 class2;
                backItem.TransitionFront = true;
                item.TransitionFront = false;
                backItem.Time = item.Time = time;
                Vector2 start = item.DragCurrent;
                //item2.Path = item.Path = new Func<double, Vector2>(class2, (IntPtr) this.<LinearTransition>b__0);
                backItem.Path = item.Path = (double t) =>
                {
                    return (Vector2)((target * t) + (start * (1.0 - t)));

                };
                return true;
                //this.BeginRendering();
            }
            return false;
        }
        
        private void OnAfterApplyTemplate()
        {
        }

        private static void OnApplyPageTemplatePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        ///2 ham trung nhau --> bo 1 ham, nhung chua biet ham nao tot hon
        //public override void OnApplyTemplate()
        //{
        //    string errors = string.Empty;
        //    base.OnApplyTemplate();
        //    this._elementContentControl = this.GetTemplateChild<ContentControl>("ContentControl", true, ref errors);
        //    if (this._elementContentControl != null)
        //    {
        //        this.InitializeContentControlPart();
        //    }
        //    if (!string.IsNullOrEmpty(errors) && !DesignerProperties.GetIsInDesignMode(this))
        //    {
        //        throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Template cannot be applied to BookItem.\nDetails: {0}", new object[] { errors }));
        //    }
        //}

        public override void OnApplyTemplate()
        {
            string str = string.Empty;
            base.OnApplyTemplate();
            this._isLoaded = true;
            if (!string.IsNullOrEmpty(str))
            {
                if (!DesignerProperties.GetIsInDesignMode(this))
                {
                    throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Template cannot be applied to Book.\nDetails: {0}", new object[] { str }));
                }
            }
            else
            {
                this.ChangeVisualStateCommon(true);
                this.OnAfterApplyTemplate();
            }
        }

        [SuppressMessage("Microsoft.Usage", "CA1801")]
        private void OnCurrentPageChanged(int oldValue)
        {
            if (!this._ignorePageChange)
            {
                this.CurrentPage = Math.Max(0, Math.Min(base.Items.Count, this.CurrentPage));
                this.Reset();
            }
        }

        [SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "newValue")]
        private static void OnCurrentPagePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Book sender = d as Book;
            int oldValue = (int)e.OldValue;
            sender.OnCurrentPageChanged(oldValue);
            if ((sender.CurrentPageChanged != null) && sender._throwCurrentPageChanged)
            {
                sender.CurrentPageChanged(sender, new PropertyChangedEventArgs<int> { OldValue = (int)e.OldValue, NewValue = (int)e.NewValue });
            }
        }

        [SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "newValue")]
        private static void OnCurrentZonePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Book sender = d as Book;
            if ((sender.CurrentZoneChanged != null) && sender._throwCurrentZoneChanged)
            {
                sender.CurrentZoneChanged(sender, new PropertyChangedEventArgs<BookZone> { OldValue = (BookZone)e.OldValue, NewValue = (BookZone)e.NewValue });
            }
        }

        [SuppressMessage("Microsoft.Usage", "CA1801")]
        private void OnFoldSizeChanged(double oldValue)
        {
            this.BeginRendering();
        }

        [SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "newValue")]
        private static void OnFoldSizePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Book book = d as Book;
            double oldValue = (double)e.OldValue;
            book.OnFoldSizeChanged(oldValue);
        }

        [SuppressMessage("Microsoft.Usage", "CA1801")]
        private void OnIsFirstPageOnTheRightChanged(bool oldValue)
        {
            this.Reset();
        }

        [SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "newValue")]
        private static void OnIsFirstPageOnTheRightPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Book book = d as Book;
            bool oldValue = (bool)e.OldValue;
            book.OnIsFirstPageOnTheRightChanged(oldValue);
        }

        [SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "newValue")]
        private static void OnIsMouseOverPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Book sender = d as Book;
            if ((sender.IsMouseOverChanged != null) && sender._throwIsMouseOverChanged)
            {
                sender.IsMouseOverChanged(sender, new PropertyChangedEventArgs<bool> { OldValue = (bool)e.OldValue, NewValue = (bool)e.NewValue });
            }
            sender.ChangeVisualStateCommon(true);
        }

        [SuppressMessage("Microsoft.Usage", "CA1801")]
        private void OnItemContainerStyleChanged(Style oldValue)
        {
            for (int i = 0; i < base.Items.Count; i++)
            {
                BookItem item = base.ItemContainerGenerator.ContainerFromIndex(i) as BookItem;
                if (item != null)
                {
                    item.Style=(this.ItemContainerStyle);
                }
            }
        }

        [SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "newValue")]
        private static void OnItemContainerStylePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Book book = d as Book;
            Style oldValue = (Style)e.OldValue;
            book.OnItemContainerStyleChanged(oldValue);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.PageDown)
            {
                this.TurnPage(false);
            }
            else if (e.Key == Key.PageUp)
            {
                this.TurnPage(true);
            }
        }

        private void OnLayoutUpdated(object sender, EventArgs e)
        {
            for (int i = 0; i < base.Items.Count; i++)
            {
                BookItem element = BookItemFromIndex(i);
                element.index = i;
            }

            if (base.RenderSize != this._size)
            {
                this._size = base.RenderSize;

                this.BeginRendering();
            }
        }

        private void OnMouseClick(object sender, MouseEventArgs e)
        {
            if (this.PageFoldAction == PageFoldAction.TurnPageOnClick)
            {
                this.TurnPageClick();
            }
        }

        private void OnMouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.PageFoldAction == PageFoldAction.TurnPageOnDoubleClick)
            {
                this.TurnPageClick();
            }
        }

        private void OnMouseDragEnd(object sender, MouseDragEventArgs e)
        {
            if (this._dragged != null)
            {
                BookItem item = this.Back(this._dragged);
                Vector2 dragStart = this._dragged.DragStart;
                Vector2 vector2 = new Vector2(e.GetPosition(this)) / base.RenderSize;
                Vector2 vector3 = new Vector2(0.5, 0.5);
                if (((this.Orientation == System.Windows.Controls.Orientation.Horizontal) && (Math.Sign((double)(vector2.X - vector3.X)) != Math.Sign((double)(this._dragged.DragStart.X - vector3.X)))) || ((this.Orientation == null) && (Math.Sign((double)(vector2.Y - vector3.Y)) != Math.Sign((double)(this._dragged.DragStart.Y - vector3.Y)))))
                {
                    dragStart = VectorHelper.Symmetry(new Size(1.0, 1.0), item.Direction, dragStart);
                    dragStart = VectorHelper.Flip(new Size(1.0, 1.0), this._dragged.Direction, dragStart);
                    this.CurrentPage = base.ItemContainerGenerator.IndexFromContainer(item);
                }
                BookItem backItem = this.Back(this._dragged);
                if (LinearTransition(this._dragged, backItem, (double)0.0, dragStart))
                {
                    this.BeginRendering();
                }
                this._dragged = null;
                if (this.DragPageFinished != null)
                {
                    this.DragPageFinished(this, new BookDragPageFinishedEventArgs(this.CurrentPage));
                }
            }
        }

        private void OnMouseDragMove(object sender, MouseDragEventArgs e)
        {
            if (this._dragged != null)
            {
                this.Back(this._dragged).DragCurrent = this._dragged.DragCurrent = new Vector2(e.GetPosition(this)) / base.RenderSize;
                this.BeginRendering();
            }
        }

        private void OnMouseDragStart(object sender, MouseDragEventArgs e)
        {
            Point position = e.GetPosition(this);
            int index = this.CurrentPage - this.PagePosition(this.CurrentPage);
            if (((this.Orientation == System.Windows.Controls.Orientation.Horizontal) && (position.X > (base.RenderSize.Width / 2.0))) || ((this.Orientation == null) && (position.Y > (base.RenderSize.Height / 2.0))))
            {
                index++;
            }
            this._dragged = base.ItemContainerGenerator.ContainerFromIndex(index) as BookItem;
            if (this._dragged != null)
            {
                BookItem item = this.Back(this._dragged);
                if (item == null)
                {
                    this._dragged = null;
                }
                else
                {
                    this.InstantUndoFold();
                    this._dragged.TransitionFront = false;
                    item.TransitionFront = true;
                    this._dragged.DragStart = item.DragStart = this._dragged.DragCurrent = item.DragCurrent = new Vector2(position) / base.RenderSize;
                    this.SortPages(this.PagePosition(index) == 1);
                    this.BeginRendering();
                    if (this.DragPageStarted != null)
                    {
                        this.DragPageStarted(this, EventArgs.Empty);
                    }
                }
            }
        }

        #region Fold

        public BookZone CurrentZone
        {
            get
            {
                return (BookZone)base.GetValue(CurrentZoneProperty);
            }
            private set
            {
                base.SetValue(CurrentZoneProperty, value);
            }
        }

        public double FoldSize
        {
            get
            {
                return (double)base.GetValue(FoldSizeProperty);
            }
            set
            {
                base.SetValue(FoldSizeProperty, value);
            }
        }
        
        void Fold(BookZone zone)
            //int index, BookZone zone, double FoldSize, Size RenderSize, System.Windows.Controls.Orientation Orientation)
        {
            int index = this.CurrentPage - this.PagePosition(this.CurrentPage);
            if (_bIsLayout1Page)
            {
                ///re-layout zindex and find fold page
                switch (zone)
                {
                    case BookZone.BottomLeft:
                        index += (Orientation == System.Windows.Controls.Orientation.Horizontal) ? 0 : 1;
                        break;

                    case BookZone.TopLeft:
                        break;

                    case BookZone.TopRight:
                        index += (Orientation == System.Windows.Controls.Orientation.Horizontal) ? 1 : 0;
                        break;

                    case BookZone.BottomRight:
                        index++;
                        break;
                }
                this.SortPages(this.PagePosition(index) == 1);
            }
            else
            {
                this.SortPages(zone == BookZone.BottomRight || zone == BookZone.TopRight);
            }
            //create fold page for animation
            this._fold = BookItemFromIndex(index);

            BookItem back_fold = null;
            if (_bIsLayout1Page)
            {
                back_fold = this.Back(this._fold);
            }
            else
            {//not tested
                int back_fold_index = -1;
                switch (zone)
                {
                    case BookZone.BottomLeft:
                    case BookZone.TopLeft:
                        back_fold_index = index - 1;
                        break;

                    case BookZone.TopRight:
                    case BookZone.BottomRight:
                        back_fold_index = index + 1;
                        break;
                }
                if ((index >= 0) && (index < base.Items.Count))
                {
                    back_fold = BookItemFromIndex(back_fold_index);
                }
            }

            ///calculate start and target point
            Vector2 start = new Vector2();
            Vector2 target = new Vector2();
            FoldAnimation_CalculateStartAndTargetPoint(zone, FoldSize, base.RenderSize, ref start, ref target);

            if (CreateFoldAnimation(ref this._fold, back_fold, start, target))
            {
                this.BeginRendering();
            }
        }

        private static void FoldAnimation_CalculateStartAndTargetPoint(BookZone zone, double FoldSize, Size RenderSize, ref Vector2 start, ref Vector2 target)
        {
            Vector2 vector3 = new Vector2(FoldSize / RenderSize.Width, FoldSize / RenderSize.Height);
            switch (zone)
            {
                case BookZone.BottomLeft:
                    start = new Vector2(0.0, 1.0);
                    target = new Vector2(vector3.X, 1.0 - vector3.Y);
                    break;

                case BookZone.TopLeft:
                    start = new Vector2();
                    target = vector3;
                    break;

                case BookZone.TopRight:
                    start = new Vector2(1.0, 0.0);
                    target = new Vector2(1.0 - vector3.X, vector3.Y);
                    break;

                case BookZone.BottomRight:
                    start = new Vector2(1.0, 1.0);
                    target = new Vector2(1.0, 1.0) - vector3;
                    break;
            }
        }

        static bool CreateFoldAnimation(ref BookItem fold, BookItem back_fold, Vector2 start, Vector2 target)
        {
            if (fold != null)
            {
                if (fold.DragStart != start)
                {
                    //BookItem back_fold = this.Back(fold);
                    if (back_fold != null)
                    {
                        back_fold.DragCurrent = fold.DragCurrent = back_fold.DragStart = fold.DragStart = start;
                        //BookItem backItem = this.Back(fold);
                        if (LinearTransition(fold, back_fold, (double)0.0, target))
                        {
                            //this.BeginRendering();
                            return true;
                        }
                    }
                    // this.LinearTransition(fold, start, target);
                }
                else
                {
                    if (fold.Time == 1.0)
                    {
                        fold.Time = 0.0;
                    }
                    //BookItem backItem = this.Back(fold);
                    if (LinearTransition(fold, back_fold, fold.Time, target))
                    {
                        //this.BeginRendering();
                        return true;
                    }
                }
            }
            return false;
        }

        private void UndoFold()
        {
            if (this._fold != null)
            {
                BookItem backItem = this.Back(this._fold);
                if (LinearTransition(this._fold, backItem, (double)(1.0 - this._fold.Time), this._fold.DragStart))
                {
                    this.BeginRendering();
                }
                this._fold = null;
            }
        }

        private void UpdateZone(BookZone zone)
        {
            if ((this.ShowPageFold == PageFoldVisibility.OnMouseOver) && ((this.CurrentZone != zone) || (this._fold == null)))
            {
                this.UndoFold();
                if (((this._dragged == null) && (zone != BookZone.Center)) && (zone != BookZone.Out))
                {
                    this.Fold(zone);
                }
            }
            this.CurrentZone = zone;
        }

        #endregion

        #region mouse event
        protected override void OnMouseEnter(MouseEventArgs e)
        {
            //this.UpdateZone(this.GetZone(e.GetPosition(this)));
            BookZone bz = ZoneDetector.GetZone(this.RenderSize, this.FoldSize, e.GetPosition(this), this.Orientation);
            this.UpdateZone(bz);
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            this.UpdateZone(BookZone.Out);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            BookZone bz = ZoneDetector.GetZone(this.RenderSize, this.FoldSize, e.GetPosition(this), this.Orientation);
            this.UpdateZone(bz);
        }
        #endregion

        [SuppressMessage("Microsoft.Usage", "CA1801")]
        private void OnOrientationChanged(Orientation old)
        {
            this.Reset();
        }

        [SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "newValue")]
        private static void OnOrientationPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Book book = d as Book;
            Orientation old = (Orientation)e.OldValue;
            book.OnOrientationChanged(old);
        }

        [SuppressMessage("Microsoft.Usage", "CA1801")]
        private void OnShowInnerShadowsChanged(bool oldValue)
        {
            this.BeginRendering();
        }

        [SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "newValue")]
        private static void OnShowInnerShadowsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Book book = d as Book;
            bool oldValue = (bool)e.OldValue;
            book.OnShowInnerShadowsChanged(oldValue);
        }

        [SuppressMessage("Microsoft.Usage", "CA1801")]
        private void OnShowOuterShadowsChanged(bool oldValue)
        {
            this.BeginRendering();
        }

        [SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "newValue")]
        private static void OnShowOuterShadowsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Book book = d as Book;
            bool oldValue = (bool)e.OldValue;
            book.OnShowOuterShadowsChanged(oldValue);
        }

        [SuppressMessage("Microsoft.Usage", "CA1801")]
        private void OnShowPageFoldChanged(PageFoldVisibility oldValue)
        {
            this.Reset();
        }

        [SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "newValue")]
        private static void OnShowPageFoldPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Book book = d as Book;
            PageFoldVisibility oldValue = (PageFoldVisibility)e.OldValue;
            book.OnShowPageFoldChanged(oldValue);
        }


        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            base.PrepareContainerForItemOverride(element, item);
            BookItem item2 = element as BookItem;
            item2.Style = (this.ItemContainerStyle);
            this.Reset(item2);
            this.SortPages(true);
            UIElement element2 = item as UIElement;
            if ((element2 == null) || (GetApplyPageTemplate(element2) == ApplyPageTemplate.True))
            {
                switch (item2.Direction)
                {
                    case Dock.Left:
                    case Dock.Top:
                        item2.ControlTemplate = PlatformIndependent.AdjustContentControlTemplate(this.RightPageTemplate);
                        break;

                    case Dock.Right:
                    case Dock.Bottom:
                        item2.ControlTemplate = PlatformIndependent.AdjustContentControlTemplate(this.LeftPageTemplate);
                        break;
                }
                item2.InitializeContentControlPart();
            }
        }

        private BookItem Relative(BookItem item, int back, int forward)
        {
            if (item != null)
            {
                int index = base.ItemContainerGenerator.IndexFromContainer(item);
                int num2 = this.PagePosition(index);
                index += (num2 == 0) ? back : forward;
                if ((index >= 0) && (index < base.Items.Count))
                {
                    return (BookItemFromIndex(index));
                }
            }
            return null;
        }

        private void Reset()
        {
            for (int i = 0; i < base.Items.Count; i++)
            {
                BookItem element = BookItemFromIndex(i);
                if (element != null)
                {
                    this.Reset(element);
                    //element.index = i;
                }
            }
            this.SortPages(true);
            this.BeginRendering();
        }

        #region BookItemLayout
        public bool _bIsLayout1Page = true;
        private int IndexFromBookItem(BookItem element)
        {
            return base.ItemContainerGenerator.IndexFromContainer(element);
        }

        private BookItem BookItemFromIndex(int index)
        {
            return base.ItemContainerGenerator.ContainerFromIndex(index) as BookItem;
        }

        private int PagePosition(int index)
        {
            if (_bIsLayout1Page)
            {
                if (this.IsFirstPageOnTheRight)
                {
                    index++;
                }
                return (index % 2);
            }
            else
            {
                //if (this.IsFirstPageOnTheRight)
                //    return 1;
                return 0;
            }
        }
        #endregion

        /// <summary>
        /// set the position of BOOKITEMS
        /// </summary>
        /// <param name="element"></param>
        private void Reset(BookItem element)
        {
            element.DragStart = element.DragCurrent = new Vector2();
            element.Path = null;
            element.TransitionFront = false;
            element.Time = 0.0;
            int index = IndexFromBookItem(element);
            int num2 = this.PagePosition(index);
            if (this.Orientation == System.Windows.Controls.Orientation.Horizontal)
            {
                if (_bIsLayout1Page)
                {
                    Grid.SetColumn(element, num2);
                    Grid.SetRowSpan(element, 2);
                    Grid.SetRow(element, 0);
                    Grid.SetColumnSpan(element, 1);
                    element.Direction = (num2 == 0) ? Dock.Right : Dock.Left;
                }
                else
                {
                    Grid.SetColumn(element, num2);
                    Grid.SetRowSpan(element, 2);
                    Grid.SetRow(element, 0);
                    Grid.SetColumnSpan(element, 2);
                    //element.Direction = (num2 == 0) ? Dock.Right : Dock.Left;
                    element.Direction = Dock.Left;
                }
            }
            else
            {
                Grid.SetRow(element, num2);
                Grid.SetColumnSpan(element, 2);
                Grid.SetColumn(element, 0);
                Grid.SetRowSpan(element, 1);
                element.Direction = (num2 == 0) ? Dock.Bottom : Dock.Top;
            }
        }

        public static void SetApplyPageTemplate(DependencyObject element, ApplyPageTemplate value)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            element.SetValue(ApplyPageTemplateProperty, value);
        }

        //private void SetCurrentPage(int value)
        //{
        //    this._ignorePageChange = true;
        //    this.CurrentPage = value;
        //    this._ignorePageChange = false;
        //}

       

        private void SortPages(bool forward)
        {
            if (_bIsLayout1Page)
            {
                int num = forward ? base.Items.Count : 0;
                int num2 = base.Items.Count - num;
                for (int i = 0; i < base.Items.Count; i++)
                {
                    BookItem item = BookItemFromIndex(i);
                    if (item == null)
                    {
                        return;
                    }
                    int num4 = this.PagePosition(i);
                    int zindex = (num4 == 0) ? (num + (i / 2)) : (num2 - (i / 2));
                    Canvas.SetZIndex(item, zindex);
                }
            }
            else
            {//not tested
                for (int i = 0; i < base.Items.Count; i++)
                {
                    BookItem item = BookItemFromIndex(i);
                    if (item == null) return;
                    int zindex = i;
                    Canvas.SetZIndex(item, zindex);
                }
            }
        }

        private BookItem Top(BookItem item)
        {
            return this.Relative(item, 2, -2);
        }

        #region Turn page
        private bool TurnPage(BookZone zone)
        {
            bool forward = ((this.Orientation == System.Windows.Controls.Orientation.Horizontal) && ((zone == BookZone.BottomRight) || (zone == BookZone.TopRight))) || ((this.Orientation == null) && ((zone == BookZone.BottomLeft) || (zone == BookZone.BottomRight)));
            int num = (this.CurrentPage - this.PagePosition(this.CurrentPage)) + (forward ? 1 : 0);
            BookItem item = base.ItemContainerGenerator.ContainerFromIndex(num) as BookItem;
            if ((this.Back(item) != null) && (this._dragged == null))
            {
                this.InstantUndoFold();
                this.SortPages(forward);
                this.CircleTransition(item, zone);
                this.CurrentPage = this.CurrentPage + (forward ? 2 : -2);
                return true;
            }
            return false;
        }

        public bool TurnPage(bool forward)
        {
            if (this.Orientation == null)
            {
                return this.TurnPage(forward ? BookZone.BottomLeft : BookZone.TopLeft);
            }
            return this.TurnPage(forward ? BookZone.TopRight : BookZone.TopLeft);
        }

        private void TurnPageClick()
        {
            if ((this.CurrentZone != BookZone.Center) && (this.CurrentZone != BookZone.Out))
            {
                this.TurnPage(this.CurrentZone);
            }
        }

        public void TurnNextPage()
        {
            this.CurrentZone = BookZone.BottomRight;
            this.TurnPage(this.CurrentZone);
        }
        public void TurnPreviousPage()
        {
            this.CurrentZone = BookZone.BottomLeft;
            this.TurnPage(this.CurrentZone);
        }
        #endregion


        private void UpdatePosition(BookItem element, double deltaTime)
        {
            if (element.Path != null)
            {
                element.Time = Math.Min((double)(element.Time + (deltaTime / ((double)this.TurnInterval))), (double)1.0);
                element.DragCurrent = element.Path.Invoke(element.Time);
                if (element.Time == 1.0)
                {
                    element.Path = null;
                }
            }
        }

        #region trainsition animation
        /// <summary>
        /// main function that update transition animation
        /// </summary>
        /// <param name="element"></param>
        private void UpdateTransition(BookItem element)
        {
            BookTransition transition = element.Transition ?? this.Transition;
            if (this.IsVisible(element))
            {
                element.Visibility=System.Windows.Visibility.Visible;
                element.UpdateLayout();
                transition.Turn(element, this.ShowOuterShadows && this.CastShadow(element), this.ShowInnerShadows, base.RenderSize);
            }
            else
            {
                element.Visibility=System.Windows.Visibility.Collapsed;
            }
        }
        #endregion

        // Properties
        public int CurrentPage
        {
            get
            {
                return (int)base.GetValue(CurrentPageProperty);
            }
            set
            {
                this._ignorePageChange = true;
                base.SetValue(CurrentPageProperty, value);
                this._ignorePageChange = false;
            }
        }

        public bool IsFirstPageOnTheRight
        {
            get
            {
                return (bool)base.GetValue(IsFirstPageOnTheRightProperty);
            }
            set
            {
                base.SetValue(IsFirstPageOnTheRightProperty, value);
            }
        }

        public bool IsMouseOver
        {
            get
            {
                return (bool)base.GetValue(IsMouseOverProperty);
            }
            internal set
            {
                base.SetValue(IsMouseOverProperty, value);
            }
        }

        public Style ItemContainerStyle
        {
            get
            {
                return (Style)base.GetValue(ItemContainerStyleProperty);
            }
            set
            {
                base.SetValue(ItemContainerStyleProperty, value);
            }
        }

        public ControlTemplate LeftPageTemplate
        {
            get
            {
                return (ControlTemplate)base.GetValue(LeftPageTemplateProperty);
            }
            set
            {
                base.SetValue(LeftPageTemplateProperty, value);
            }
        }

        /// <summary>
        /// this properties decide turn page forward or backward
        /// </summary>
        public Orientation Orientation
        {
            get
            {
                return (Orientation)base.GetValue(OrientationProperty);
            }
            set
            {
                base.SetValue(OrientationProperty, value);
            }
        }

        public PageFoldAction PageFoldAction
        {
            get
            {
                return (PageFoldAction)base.GetValue(PageFoldActionProperty);
            }
            set
            {
                base.SetValue(PageFoldActionProperty, value);
            }
        }

        public ControlTemplate RightPageTemplate
        {
            get
            {
                return (ControlTemplate)base.GetValue(RightPageTemplateProperty);
            }
            set
            {
                base.SetValue(RightPageTemplateProperty, value);
            }
        }

        public bool ShowInnerShadows
        {
            get
            {
                return (bool)base.GetValue(ShowInnerShadowsProperty);
            }
            set
            {
                base.SetValue(ShowInnerShadowsProperty, value);
            }
        }

        public bool ShowOuterShadows
        {
            get
            {
                return (bool)base.GetValue(ShowOuterShadowsProperty);
            }
            set
            {
                base.SetValue(ShowOuterShadowsProperty, value);
            }
        }

        public PageFoldVisibility ShowPageFold
        {
            get
            {
                return (PageFoldVisibility)base.GetValue(ShowPageFoldProperty);
            }
            set
            {
                base.SetValue(ShowPageFoldProperty, value);
            }
        }

        internal BookTransition Transition
        {
            get
            {
                return (BookTransition)base.GetValue(TransitionProperty);
            }
            set
            {
                base.SetValue(TransitionProperty, value);
            }
        }

        public int TurnInterval
        {
            get
            {
                return (int)base.GetValue(TurnIntervalProperty);
            }
            set
            {
                base.SetValue(TurnIntervalProperty, value);
            }
        }


        // Events
        public event EventHandler<PropertyChangedEventArgs<int>> CurrentPageChanged;
      
        public event EventHandler<PropertyChangedEventArgs<BookZone>> CurrentZoneChanged;
       

        public event EventHandler<BookDragPageFinishedEventArgs> DragPageFinished;
    
        public event EventHandler DragPageStarted;
     

        public event EventHandler<PropertyChangedEventArgs<bool>> IsMouseOverChanged;
       

        protected void ChangeVisualStateCommon(bool useTransitions)
        {
            if (base.IsEnabled && !this.IsMouseOver)
            {
                VisualStateHelper.GoToState(this, "Normal", useTransitions);
            }
            if (!base.IsEnabled)
            {
                VisualStateHelper.GoToState(this, "Disabled", useTransitions);
            }
            if (base.IsEnabled && this.IsMouseOver)
            {
                VisualStateHelper.GoToState(this, "MouseOver", useTransitions);
            }
        }



    }
}
