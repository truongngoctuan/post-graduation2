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
    public partial class App : Application
    {

        public App()
        {
            this.Startup += this.Application_Startup;
            this.Exit += this.Application_Exit;
            this.UnhandledException += this.Application_UnhandledException;

            InitializeComponent();
        }

       

        #region app_event
        private void Application_Exit(object sender, EventArgs e)
        {

        }

        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            // If the app is running outside of the debugger then report the exception using
            // the browser's exception mechanism. On IE this will display it a yellow alert 
            // icon in the status bar and Firefox will display a script error.
            if (!System.Diagnostics.Debugger.IsAttached)
            {

                // NOTE: This will allow the application to continue running after an exception has been thrown
                // but not handled. 
                // For production applications this error handling should be replaced with something that will 
                // report the error to the website and stop the application.
                e.Handled = true;
                Deployment.Current.Dispatcher.BeginInvoke(delegate { ReportErrorToDOM(e); });
            }
        }

        private void ReportErrorToDOM(ApplicationUnhandledExceptionEventArgs e)
        {
            try
            {
                string errorMsg = e.ExceptionObject.Message + e.ExceptionObject.StackTrace;
                errorMsg = errorMsg.Replace('"', '\'').Replace("\r\n", @"\n");

                System.Windows.Browser.HtmlPage.Window.Eval("throw new Error(\"Unhandled Error in Silverlight Application " + errorMsg + "\");");
            }
            catch (Exception)
            {
            }
        }
        #endregion

        #region page transition controler
        Grid mainUI = new Grid();
        public static UserControl CurrentPage = null;
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            //this.RootVisual = new MainPage();
            this.RootVisual = mainUI;
            App.GoToPage(new HomePage());
        }
        
        public static void GoToPage(UserControl nextPg)
        {
            if (nextPg == null)
                return;
            App app = (App)Application.Current;

            if (app.mainUI.Children.Contains(nextPg) == false)
            {
                app.mainUI.Children.Add(nextPg);
            }

            if (CurrentPage != null)
            {
                _3DPresentation.Utils.TransitionEffectHelper.BeginAnimation(CurrentPage, nextPg);
            }

            // Show only nextPg
            foreach (UserControl page in app.mainUI.Children)
            {
                if (page == nextPg)
                {
                    page.Visibility = Visibility.Visible;
                    page.IsEnabled = true;
                }
                else
                {
                    page.Visibility = Visibility.Collapsed;
                    page.IsEnabled = false;
                }
            }
            CurrentPage = nextPg;
        }

        public static void RemovePage(UserControl page)
        {
            if (page == null)
                return;
            App app = (App)Application.Current;
            if (app.mainUI.Children.Contains(page))
            {
                app.mainUI.Children.Remove(page);
                page = null;
            }
        }

        public static void GoToPage(UserControl nextPg, UIElement rootLayout)
        {
            if (nextPg == null)
                return;
            App app = (App)Application.Current;

            if (app.mainUI.Children.Contains(nextPg) == false)
            {
                app.mainUI.Children.Add(nextPg);
            }

            if (CurrentPage != null)
            {
                _3DPresentation.Utils.TransitionEffectHelper.BeginAnimation(CurrentPage, nextPg);
            }

            // Show only nextPg
            foreach (UserControl page in app.mainUI.Children)
            {
                if (page == nextPg)
                {
                    page.Visibility = Visibility.Visible;
                    page.IsEnabled = true;
                }
                else
                {
                    page.Visibility = Visibility.Collapsed;
                    page.IsEnabled = false;
                }
            }
            CurrentPage = nextPg;
        }
        #endregion
    }
}
