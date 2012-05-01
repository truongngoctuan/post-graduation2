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

namespace PageFlip
{
    public partial class Sheet : UserControl
    {
        
        // Methods
        public Sheet()
        {
            this.InitializeComponent();
        }


        // Properties
        public double X
        {
            get
            {
                return (double)base.GetValue(Canvas.LeftProperty);
            }
            set
            {
                base.SetValue(Canvas.LeftProperty, value);
            }
        }

        public double Y
        {
            get
            {
                return (double)base.GetValue(Canvas.TopProperty);
            }
            set
            {
                base.SetValue(Canvas.TopProperty, value);
            }
        }
    }

}
