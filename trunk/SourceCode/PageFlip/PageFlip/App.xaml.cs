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
using System.Windows.Threading;

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
        MasterPage mPage = new MasterPage();
        Canvas mainUI = new Canvas();
        public static UserControl CurrentPage = null;
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            mainUI = mPage.containerContent;
            //this.RootVisual = mainUI;
            this.RootVisual = mPage;
            //App.GoToPage(new HomePage(), null);

            //Page1 mainP = new Page1();

            //MainPage mainP = new MainPage()
            //{
            //    ParentView = null,
            //    ContentPageIndex = 0
            //};

            MainPage2 mainP = new MainPage2();

            App app = (App)Application.Current;

            if (app.mainUI.Children.Contains(mainP) == false)
            {
                app.mainUI.Children.Add(mainP);
                //System.Threading.Thread.Sleep(2000);
            }
        }

        public static void GoToPage(UserControl nextPg, System.Windows.Media.Imaging.WriteableBitmap rootLayout)
        {
            if (nextPg == null)
                return;
            App app = (App)Application.Current;

            if (app.mainUI.Children.Contains(nextPg) == false)
            {
                app.mainUI.Children.Add(nextPg);
                //System.Threading.Thread.Sleep(2000);
            }

            if (CurrentPage != null)
            {
                //_3DPresentation.Utils.TransitionEffectHelper.BeginAnimation(CurrentPage, nextPg);
                _3DPresentation.Utils.TransitionEffectHelper.BeginAnimation(rootLayout, nextPg);
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

        public static void GoToPage(UserControl parentPage, UIElement rootLayout,
            UserControl nextPg)
        {
            System.Windows.Media.Imaging.WriteableBitmap capture = new System.Windows.Media.Imaging.WriteableBitmap(rootLayout, new System.Windows.Media.ScaleTransform());
            App.RemovePage(parentPage);
            App.GoToPage(nextPg, capture);
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

        #endregion
    }
}
