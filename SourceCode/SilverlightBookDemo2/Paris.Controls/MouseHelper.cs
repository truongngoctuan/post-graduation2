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
using System.Collections;
using System.Collections.Generic;
using System.Threading;
namespace Paris.Controls
{
    public class MouseHelper : IDisposable
    {
        // Fields
        private UIElement _captureElement;
        private bool _contextMenuInitialized;
        private MouseEventHandler _contextMenuInvoked;
        private UIElement _element;
        private bool _isMouseOver;
        private Point _lastPos;
        private DateTime _lastTime;
        private List<object> _weakHandlers;
        private EventHandler<MouseWheelEventArgs> _wheel;
        private bool _wheelInitialized;
        private bool capture;
        private bool dragStarted;
        private const string ERR_MUSTBEWINDOWLESS = "To use this event the Silverlight plug-in must have its 'windowless' parameter set to true.";
        private Point lastMouseDownPos;
        private Point lastPos;
       // private MouseEventHandler MouseClick;
        //private MouseEventHandler MouseDoubleClick;
        //private EventHandler<MouseDragEventArgs> MouseDragEnd;
        //private EventHandler<MouseDragEventArgs> MouseDragMove;
        //private EventHandler<MouseDragEventArgs> MouseDragStart;

        // Events
        public event MouseEventHandler ContextMenuInvoked
        {
            add
            {
                this._contextMenuInvoked = (MouseEventHandler)Delegate.Combine(this._contextMenuInvoked, value);
                this.InitializeRightButton();
            }
            remove
            {
                this._contextMenuInvoked = (MouseEventHandler)Delegate.Remove(this._contextMenuInvoked, value);
            }
        }

        public event MouseEventHandler MouseClick;
       

        public event MouseEventHandler MouseDoubleClick;
       
        public event EventHandler<MouseDragEventArgs> MouseDragEnd;
       
        public event EventHandler<MouseDragEventArgs> MouseDragMove;

        public event EventHandler<MouseDragEventArgs> MouseDragStart;
       
        public event EventHandler<MouseWheelEventArgs> MouseWheel
        {
            add
            {
                this._wheel = (EventHandler<MouseWheelEventArgs>)Delegate.Combine(this._wheel, value);
                this.InitializeWheel();
            }
            remove
            {
                this._wheel = (EventHandler<MouseWheelEventArgs>)Delegate.Remove(this._wheel, value);
            }
        }

        // Methods
        public MouseHelper(UIElement element)
            : this(element, element)
        {
        }

        internal MouseHelper(UIElement element, UIElement captureElement)
        {
            this._weakHandlers = new List<object>();
            this.lastPos = new Point();
            this.lastMouseDownPos = new Point();
            this._element = element;
            this._captureElement = captureElement;
            this._element.MouseEnter += new MouseEventHandler(_element_MouseEnter);
            this._element.MouseLeave += new MouseEventHandler(_element_MouseLeave);
            this.InitializeClick();
            this.InitializeDoubleClick();
            this.InitializeRightButton();
            this.InitializeDrag();
        }

        private void _element_MouseEnter(object sender, MouseEventArgs e)
        {
            this._isMouseOver = true;
        }

        private void _element_MouseLeave(object sender, MouseEventArgs e)
        {
            this._isMouseOver = false;
        }

        internal static void AddEventHandler(UIElement element, string eventName, Delegate handler)
        {
            element.GetType().GetEvent(eventName).AddEventHandler(element, handler);
        }

        private void ClickMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (this.MouseClick != null)
            {
                this._lastPos = e.GetPosition(this._element);
            }
        }

        private void ClickMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (this.MouseClick != null)
            {
                DateTime now = DateTime.Now;
                Point position = e.GetPosition(this._element);
                double num = this._lastPos.X - position.X;
                double num2 = this._lastPos.Y - position.Y;
                if (((num * num) + (num2 * num2)) <= 4.0)
                {
                    this.MouseClick.Invoke(this._element, e);
                }
            }
        }

        public void Dispose()
        {
            if (this._element != null)
            {
                this._element.MouseEnter -= new MouseEventHandler(_element_MouseEnter);
                this._element.MouseLeave -= new MouseEventHandler(_element_MouseLeave); 
                this.FinalizeClick();
                this.FinalizeDoubleClick();
                this.FinalizeRightButton();
                this.FinalizeDrag();
            }
        }

        private void DoubleClickMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (this.MouseDoubleClick != null)
            {
                DateTime now = DateTime.Now;
                Point position = e.GetPosition(this._element);
                if (now.Subtract(this._lastTime).TotalMilliseconds <= 400.0)
                {
                    double num = this._lastPos.X - position.X;
                    double num2 = this._lastPos.Y - position.Y;
                    if (((num * num) + (num2 * num2)) <= 4.0)
                    {
                        this.MouseDoubleClick.Invoke(this._element, e);
                        e.Handled = (true);
                        return;
                    }
                }
                this._lastTime = now;
                this._lastPos = position;
            }
        }

        private void DragEnd(MouseEventArgs e, bool isLostMouseCapture)
        {
            this.capture = false;
            RemoveEventHandler(this._captureElement, "MouseMove", new MouseEventHandler( this.DragMouseMove));
            RemoveEventHandler(this._captureElement, "MouseLeftButtonUp", new MouseButtonEventHandler(this.DragMouseLeftButtonUp));
            RemoveEventHandler(this._captureElement, "LostMouseCapture", new MouseEventHandler(this.DragLostMouseCapture));
            this._captureElement.ReleaseMouseCapture();
            if (this.dragStarted && (this.MouseDragEnd != null))
            {
                this.MouseDragEnd(this._element, new MouseDragEventArgs(this, this.lastMouseDownPos, this.lastPos, this.lastPos) { MouseEventArgs = e, IsLostMouseCapture = isLostMouseCapture });
            }
            this.dragStarted = false;
        }

        private void DragLostMouseCapture(object sender, MouseEventArgs e)
        {
            this.DragEnd(e, true);
        }

        private void DragMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (((this.MouseDragStart != null) || (this.MouseDragMove != null)) || (this.MouseDragEnd != null))
            {
                this.lastPos = e.GetPosition(null);
                this.lastMouseDownPos = this.lastPos;
                if (this._captureElement.CaptureMouse())
                {
                    this.capture = true;
                    AddEventHandler(this._captureElement, "MouseMove", new MouseEventHandler(this.DragMouseMove));
                    AddEventHandler(this._captureElement, "MouseLeftButtonUp", new MouseButtonEventHandler(DragMouseLeftButtonUp));
                    AddEventHandler(this._captureElement, "LostMouseCapture", new MouseEventHandler(DragLostMouseCapture));
                }
            }
        }

        private void DragMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.DragEnd(e, false);
        }

        private void DragMouseMove(object sender, MouseEventArgs e)
        {
            if (((this.MouseDragStart != null) || (this.MouseDragMove != null)) || (this.MouseDragEnd != null))
            {
                if (!this.dragStarted)
                {
                    this.dragStarted = true;
                    if (this.MouseDragStart != null)
                    {
                        this.MouseDragStart(this._element, new MouseDragEventArgs(this, this.lastMouseDownPos, this.lastPos, this.lastPos));
                    }
                }
                Point position = e.GetPosition(null);
                if (this.MouseDragMove != null)
                {
                    this.MouseDragMove(this._element, new MouseDragEventArgs(this, this.lastMouseDownPos, this.lastPos, position) { MouseEventArgs = e });
                }
                this.lastPos = position;
            }
        }

        private void FinalizeClick()
        {
            RemoveEventHandler(this._element, "MouseLeftButtonDown", new MouseButtonEventHandler(this.ClickMouseLeftButtonDown));
            RemoveEventHandler(this._element, "MouseLeftButtonUp", new MouseButtonEventHandler(this.ClickMouseLeftButtonUp));
        }

        private void FinalizeDoubleClick()
        {
            RemoveEventHandler(this._element, "MouseLeftButtonDown", new MouseButtonEventHandler(this.DoubleClickMouseLeftButtonDown));
        }

        private void FinalizeDrag()
        {
            RemoveEventHandler(this._element, "MouseLeftButtonDown", new MouseButtonEventHandler(this.DragMouseLeftButtonDown));
            RemoveEventHandler(this._captureElement, "MouseMove", new MouseEventHandler(this.DragMouseMove));
            RemoveEventHandler(this._captureElement, "MouseLeftButtonUp", new MouseButtonEventHandler(this.DragMouseLeftButtonUp));
            RemoveEventHandler(this._captureElement, "LostMouseCapture", new MouseEventHandler(this.DragLostMouseCapture));
            if (this.capture)
            {
                this._captureElement.ReleaseMouseCapture();
            }
        }

        private void FinalizeRightButton()
        {
            this._element.MouseRightButtonDown -=(new MouseButtonEventHandler(this.OnContextMenuInvoked));
        }

        private void InitializeClick()
        {
            this._lastPos = new Point();
            AddEventHandler(this._element, "MouseLeftButtonDown", new MouseButtonEventHandler(this.ClickMouseLeftButtonDown));
            AddEventHandler(this._element, "MouseLeftButtonUp", new MouseButtonEventHandler(this.ClickMouseLeftButtonUp));
        }

        private void InitializeDoubleClick()
        {
            this._lastPos = new Point();
            this._lastTime = new DateTime();
            AddEventHandler(this._element, "MouseLeftButtonDown", new MouseButtonEventHandler(this.DoubleClickMouseLeftButtonDown));
        }

        private void InitializeDrag()
        {
            this.capture = false;
            this.dragStarted = false;
            this.lastPos = new Point();
            this.lastMouseDownPos = new Point();
            AddEventHandler(this._element, "MouseLeftButtonDown", new MouseButtonEventHandler(this.DragMouseLeftButtonDown));
        }

        private void InitializeRightButton()
        {
            this._element.MouseRightButtonDown +=(new MouseButtonEventHandler(this.OnContextMenuInvoked));
        }

        private void InitializeWheel()
        {
            this._element.MouseWheel += new MouseWheelEventHandler(_element_MouseWheel);
        }

        void _element_MouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            if (this._wheel != null)
            {
                this.OnWheel(new MouseWheelEventArgs((double)(e.Delta / 120), e.GetPosition(this._element.GetRootVisual())));
            }
        }

        private void OnContextMenuInvoked(object sender, MouseButtonEventArgs e)
        {
            if (this._isMouseOver && (this._contextMenuInvoked != null))
            {
                this._contextMenuInvoked.Invoke(this._element, e);
                e.Handled=(true);
            }
        }

        private void OnWheel(MouseWheelEventArgs e)
        {
            if (this._isMouseOver && (this._wheel != null))
            {
                this._wheel(this._element, e);
            }
        }

        internal static void RemoveEventHandler(UIElement element, string eventName, Delegate handler)
        {
            element.GetType().GetEvent(eventName).RemoveEventHandler(element, handler);
        }

        private T WeakHandler<T>(T t)
        {
            this._weakHandlers.Add(t);
            return t;
        }
    }


}
