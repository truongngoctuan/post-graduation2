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
using System.Collections.Generic;

namespace DataManager
{

    /// <summary>
    /// The 'Subject' abstract class
    /// </summary>
    public abstract class Subject
    {
        private List<IObserver> _observers = new List<IObserver>();
        protected UpdateInterfaceParams UpdateParams;

        public void Attach(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void Notify()
        {
            PrepareNotify();

            foreach (IObserver o in _observers)
            {
                o.UpdateInterface(UpdateParams);
            }
        }

        protected virtual void PrepareNotify()
        {

        }
    }

    /// <summary>
    /// The 'Observer' abstract class
    /// </summary>
    public interface IObserver
    {
        void UpdateInterface(UpdateInterfaceParams pars);
    }

    public struct UpdateInterfaceParams
    {
        public TurnType Type;
        public Image ClikedImage;

        public bool CanTurnLeft;
        public bool CanTurnRight;
    }
}
