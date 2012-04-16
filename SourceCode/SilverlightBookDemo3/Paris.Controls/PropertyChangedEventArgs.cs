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
using System.Globalization;

namespace Paris.Controls
{
    public class PropertyChangedEventArgs<T> : EventArgs
    {
        // Properties
        public T NewValue
        { get; set; }

        public T OldValue
        { get; set; }
    }

    public class BookDragPageFinishedEventArgs : EventArgs
    {
        // Methods
        internal BookDragPageFinishedEventArgs(int targetPage)
        {
            this.TargetPage = targetPage;
        }

        // Properties
        public int TargetPage { get; private set; }
    }

    internal static class VisualStateHelper
    {
        // Methods
        public static void GoToState(Control control, string stateName, bool useTransitions)
        {
            try
            {
                VisualStateManager.GoToState(control, stateName, useTransitions);
            }
            catch (Exception exception)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "Cannot go to state {0} in {1}.\nPlease verify you have this state in the Style and all the storyboards reference existing elements in the template.", new object[] { stateName, control.GetType().Name }), exception);
            }
        }
    }

    public class MouseWheelEventArgs : EventArgs
    {
        // Fields
        private double _delta;
        private Point _position;

        // Methods
        public MouseWheelEventArgs(double delta, Point position)
        {
            this._delta = delta;
            this._position = position;
        }

        // Properties
        public double Delta
        {
            get
            {
                return this._delta;
            }
        }

        public bool Handled { get; set; }

        public Point Position
        {
            get
            {
                return this._position;
            }
        }
    }




}
