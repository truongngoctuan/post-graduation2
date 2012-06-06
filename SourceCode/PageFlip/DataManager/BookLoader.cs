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
//using PageFlipUltis;
using System.IO;
using System.Xml;
namespace DataManager
{
	public class BookLoader:Subject
    {
        TileMenu MenuPage;//global menu, unchange, keep all menu
        TileMenu CurrentMenuPage;//only keep current lvl menu

        
        public UIElement PreviousPageLeftPart = null;
        public UIElement PreviousPageRightPart = null;

        public UIElement CurrentPageLeftPage = null;
        public UIElement CurrentPageRightPage;

        public UIElement NextPageLeftPart;
        public UIElement NextPageRightPart;

        //three these pages will be update two times:
        //1st: before transition page: update PrecurrentPage to nextpage
        //2nd: after transition page: update to 3 current page
        //public UIElement PreCurrentPage; PreCurrentPage == NextPageLeftPart == NextPageRightPart
        public UIElement PreNextPageLeftPart;
        public UIElement PreNextPageRightPart;

        public bool IsRightToLeftTransition;
        private static BookLoader _instance;
        public static BookLoader Instance()
        {
            // Uses lazy initialization.
            // Note: this is not thread safe.
            if (_instance == null)
            {
                _instance = new BookLoader();
            }

            return _instance;
        }

        protected BookLoader()
		{
            MenuPage = new TileMenu();
            CurrentMenuPage = new TileMenu();

            //load data from menudata.xml
            Uri url = new Uri("menudata3.xml", UriKind.Relative);
            WebClient client = new WebClient();
            client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(client_DownloadStringCompleted);
            client.DownloadStringAsync(url);
		}

        public void client_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                StringReader stream = new StringReader(e.Result);
                MenuPage = (TileMenu)MenuItem.Load(stream)[0];
                CurrentMenuPage = MenuPage;
                CurrentMenuPage.bt_Click(new object(), new RoutedEventArgs());
            }

        }

        public void UpdatePrePageToCurrentPage()
        {//finished transition, after this function is called, update interface 2nd times.
            CurrentPageLeftPage = NextPageLeftPart;
            CurrentPageRightPage = NextPageRightPart;

            NextPageLeftPart = PreNextPageLeftPart;
            NextPageRightPart = PreNextPageRightPart;

            PreNextPageLeftPart = null;
            PreNextPageRightPart = null;
        }
        #region MainMenuPage
        int iCurrentMenuPage = 0;

        void LoadMenu(int idx)
        {
            iCurrentMenuPage = idx;
            #region Previous Effect
            #endregion
            #region Next Effect
            if (iCurrentMenuPage - 2 >= 0)
            {
                PreviousPageLeftPart = ((TilePage)CurrentMenuPage.listSubMenu[iCurrentMenuPage - 2]).generatePage();
                PreviousPageRightPart = ((TilePage)CurrentMenuPage.listSubMenu[iCurrentMenuPage - 1]).generatePage();
            }
            else
            {
                PreviousPageLeftPart = null;
                PreviousPageRightPart = null;
            }

            if (iCurrentMenuPage + 1 < CurrentMenuPage.listSubMenu.Count)
            {//have 2 left, right pages
                NextPageLeftPart = ((TilePage)CurrentMenuPage.listSubMenu[iCurrentMenuPage]).generatePage();
                NextPageRightPart = ((TilePage)CurrentMenuPage.listSubMenu[iCurrentMenuPage + 1]).generatePage();
                
            }
            else
            {
                CurrentPageRightPage = null;
                NextPageRightPart = null;
                NextPageLeftPart = null;
            }

            if (iCurrentMenuPage + 3 < CurrentMenuPage.listSubMenu.Count)
            {
                PreNextPageLeftPart = ((TilePage)CurrentMenuPage.listSubMenu[iCurrentMenuPage + 2]).generatePage();
                PreNextPageRightPart = ((TilePage)CurrentMenuPage.listSubMenu[iCurrentMenuPage + 3]).generatePage();
            }
            else
            {
                PreNextPageRightPart = null;
                PreNextPageLeftPart = null;
            }
            #endregion

            //iCurrentMenuPage = idx;

            //if (iCurrentMenuPage < CurrentMenuPage.listSubMenu.Count)
            //{

            //    NextPageRightPart = ((TilePage)CurrentMenuPage.listSubMenu[iCurrentMenuPage]).generatePage();
            //    NextPageLeftPart = ((TilePage)CurrentMenuPage.listSubMenu[iCurrentMenuPage]).generatePage();
            //}
            //else
            //{
            //    CurrentPageRightPage = null;
            //    NextPageRightPart = null;
            //    NextPageLeftPart = null;
            //}

            //if (iCurrentMenuPage + 1 < CurrentMenuPage.listSubMenu.Count)
            //{
            //    PreNextPageRightPart = ((TilePage)CurrentMenuPage.listSubMenu[iCurrentMenuPage + 1]).generatePage();
            //    PreNextPageLeftPart = ((TilePage)CurrentMenuPage.listSubMenu[iCurrentMenuPage + 1]).generatePage();
            //}
            //else
            //{
            //    PreNextPageRightPart = null;
            //    PreNextPageLeftPart = null;
            //}
            
        }
        #endregion

        public bool IsCanTransitionRight()
        {
            if (NextPageRightPart == null && NextPageLeftPart == null) 
                return false;
            return true;
        }

        #region Control Events From Outside
        //observer tamplate for control tile signal: to submenu page, to list tiles of article and article content
        List<int> listMenuIdx = new List<int>();//current lvl in here too
        List<int> listMenuPage = new List<int>();
        public void OnClickedTile(int Lvl, int Idx)
        {
            if (Lvl != -1 && ((MenuItem)(CurrentMenuPage.listSubMenu[iCurrentMenuPage].listSubMenu[Idx])).listSubMenu.Count == 0) return;
            IsRightToLeftTransition = true;
            if (Lvl == -1)
            {

            }
            else
            {
                listMenuIdx.Add(Idx);
                listMenuPage.Add(iCurrentMenuPage);
            }
            
            //get currentTiles
            CurrentMenuPage = MenuPage;
            for (int i = 0; i < listMenuIdx.Count; i++)
            {
                CurrentMenuPage = (TileMenu)(CurrentMenuPage.listSubMenu[listMenuPage[i]].listSubMenu[listMenuIdx[i]]);
            }

            LoadMenu(0);
            Notify();
        }

        public void OnNextpage()
        {
            IsRightToLeftTransition = true;
            LoadMenu(iCurrentMenuPage + 1);
            Notify();
        }

        public void OnBackMenu()
        {
            try
            {
                if (listMenuIdx.Count == 0) return;
                IsRightToLeftTransition = false;
                //get currentTiles
                CurrentMenuPage = MenuPage;
                for (int i = 0; i < listMenuIdx.Count - 1; i++)
                {
                    CurrentMenuPage = (TileMenu)(CurrentMenuPage.listSubMenu[listMenuPage[i]].listSubMenu[listMenuIdx[i]]);
                }
                listMenuIdx.RemoveAt(listMenuIdx.Count - 1);
                listMenuPage.RemoveAt(listMenuPage.Count - 1);

                LoadMenu(0);
                Notify();
            }
            catch (Exception ex)
            {
                MessageBox.Show("OnBackMenu: "+ ex.Message);
            }
            
        }


        #endregion 
        #region Control Events From Article
        MenuItem CurrentArticle;
        int CurrentArticlePage;
        public void OnClickToArticle(string ArticleID)
        {
            //load new article with ID
            //apply to interface
            CurrentArticle = new TileArticle();
            Uri url = new Uri("Articles/" + ArticleID + ".xml", UriKind.Relative);
            WebClient client = new WebClient();
            client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(Article_DownloadStringCompleted);
            client.DownloadStringAsync(url);
        }

        void Article_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                StringReader stream = new StringReader(e.Result);
                CurrentArticle = (MenuItem)MenuItem.Load(stream)[0];
                CurrentArticlePage = 0;
                LoadArticlePage(0);
                Notify();
            }
        }

        void LoadArticlePage(int iPage)
        {
            CurrentArticlePage = iPage;

            if (CurrentArticlePage < CurrentArticle.listSubMenu.Count)
            {

                NextPageRightPart = ((TilePage)CurrentArticle.listSubMenu[CurrentArticlePage]).generatePage();
                NextPageLeftPart = ((TilePage)CurrentArticle.listSubMenu[CurrentArticlePage]).generatePage();
            }
            else
            {
                CurrentPageRightPage = null;
                NextPageRightPart = null;
                NextPageLeftPart = null;
            }

            if (CurrentArticlePage + 1 < CurrentArticle.listSubMenu.Count)
            {
                PreNextPageRightPart = ((TilePage)CurrentArticle.listSubMenu[CurrentArticlePage + 1]).generatePage();
                PreNextPageLeftPart = ((TilePage)CurrentArticle.listSubMenu[CurrentArticlePage + 1]).generatePage();
            }
            else
            {
                PreNextPageRightPart = null;
                PreNextPageLeftPart = null;
            }
        }

        public void OnBackArticle()
        {
        }

        public void OnNextpageArticle()
        {
        }
        #endregion

    }
}
