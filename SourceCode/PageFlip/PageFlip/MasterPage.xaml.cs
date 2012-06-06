﻿using System;
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
using System.Windows.Navigation;
using DataManager;
//using PageFlip.DataLoader;

namespace PageFlip
{
    public partial class MasterPage : Page
    {
        public MasterPage()
        {
            InitializeComponent();
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void btBack_Click(object sender, RoutedEventArgs e)
        {
            BookLoader.Instance().OnBackMenu();
        }

    }
}
