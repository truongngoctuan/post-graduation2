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
using System.ComponentModel;
using System.Globalization;

namespace Paris.Controls
{
    [TemplatePart(Name = "ContentControl", Type = typeof(ContentControl))]
    public class BookItem :  ContentControl
    {
        // Fields
        internal ContentControl _elementContentControl;
        internal const string ContentControlElementName = "ContentControl";
        internal ControlTemplate ControlTemplate;
        public static readonly DependencyProperty IsTransparentProperty = DependencyProperty.Register("IsTransparent", typeof(bool), typeof(BookItem), null);
        internal static readonly DependencyProperty TransitionProperty = DependencyProperty.Register("Transition", typeof(BookTransition), typeof(BookItem), null);


        public BookItem()
        {
            this.DefaultStyleKey = typeof(BookItem);
            //this.SetCustomDefaultValues();

        }



        private T GetTemplateChild<T>(string childName, bool required, ref string errors) where T : class
        {
            DependencyObject templateChild = base.GetTemplateChild(childName);
            ApplyTemplateHelper.VerifyTemplateChild(typeof(T), "template part", childName, templateChild, required, ref errors);
            return (templateChild as T);
        }

        private static T GetTemplateChildResource<T>(FrameworkElement root, string resourceName, bool required, ref string errors) where T : class
        {
            object child = root.Resources[resourceName];
            ApplyTemplateHelper.VerifyTemplateChild(typeof(T), "resource", resourceName, child, required, ref errors);
            return (child as T);
        }

        private static Storyboard GetTemplateChildResource(FrameworkElement root, string resourceName, bool required, ref string errors)
        {
            return GetTemplateChildResource<Storyboard>(root, resourceName, required, ref errors);
        }

        internal void InitializeContentControlPart()
        {
            if ((this._elementContentControl != null) && (this.ControlTemplate != null))
            {
                this._elementContentControl.Template=(this.ControlTemplate);
            }
        }

        public override void OnApplyTemplate()
        {
            string errors = string.Empty;
            base.OnApplyTemplate();
            this._elementContentControl = this.GetTemplateChild<ContentControl>("ContentControl", true, ref errors);
            if (this._elementContentControl != null)
            {
                this.InitializeContentControlPart();
            }
            if (!string.IsNullOrEmpty(errors) && !DesignerProperties.GetIsInDesignMode(this))
            {
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Template cannot be applied to BookItem.\nDetails: {0}", new object[] { errors }));
            }
        }

        private void SetCustomDefaultValues()
        {
        }

        // Properties
        internal Dock Direction { get; set; }

        internal Vector2 DragCurrent { get; set; }

        internal Vector2 DragStart { get; set; }

        public bool IsTransparent
        {
            get
            {
                return (bool)base.GetValue(IsTransparentProperty);
            }
            set
            {
                base.SetValue(IsTransparentProperty, value);
            }
        }

        internal Func<double, Vector2> Path { get; set; }

        internal double Time { get; set; }

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

        internal bool TransitionFront { get; set; }

    }
}
