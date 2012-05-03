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

namespace FishEyeSLTest
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
            Button bt = new Button();
            bt.Content = new FishEyeButtonContent();
            bt.Height = 50;
            bt.Width = 50;
            fePanel.Children.Add(bt);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("asd");
        }
    }
}
