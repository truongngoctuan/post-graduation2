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

namespace SilverlightBookDemo2
{
    public partial class CommentForm : UserControl
    {
        public CommentForm()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(CommentForm_Loaded);
        }

        void CommentForm_Loaded(object sender, RoutedEventArgs e)
        {
            //throw new NotImplementedException();
            tbName.Text = "Guest";
            tbComment.Text = "This Demo looks good";
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Your name: " + tbName.Text + "\r\n" + "Your comment: " + tbComment.Text);
        }
    }
}
