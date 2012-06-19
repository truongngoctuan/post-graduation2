﻿using System;
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
using DataManager.DataEventController;
using PageFlip.Data.DataEventController;
namespace DataManager
{
    public enum TurnType
    {
        NoTurn,
        TurnFromLeft,
        TurnFromRight,
        ClickedImage
    }

    public enum BookLoaderState
    {
        CoverPage, 
        MenuPage,
        ArticlePage
    }

	public class BookLoader:Subject
    {
        #region Data
        BookData Data;

        public UIElement PreviousPageLeftPart
        {
            get
            {
                return Data._previousPageLeftPart;
            }
        }
        public UIElement PreviousPageRightPart
        {
            get
            {
                return Data._previousPageRightPart;
            }
        }

        public UIElement CurrentPageLeftPage
        {
            get
            {
                return Data._currentPageLeftPage;
            }
        }
        public UIElement CurrentPageRightPage
        {
            get
            {
                return Data._currentPageRightPage;
            }
        }

        public UIElement NextPageLeftPart
        {
            get
            {
                return Data._nextPageLeftPart;
            }
        }
        public UIElement NextPageRightPart
        {
            get
            {
                return Data._nextPageRightPart;
            }
        }
        #endregion
        
        //public bool IsRightToLeftTransition;
        public BookLoaderState BookState;

        IDataEvent DataEventState;
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
            Data.MenuPage = new TileMenu();
            Data.CurrentMenuPage = new TileMenu();

            //load data from menudata.xml
            Uri url = new Uri("demo1_menu.xml", UriKind.Relative);
            //Uri url = new Uri("menudata3.xml", UriKind.Relative);
            WebClient client = new WebClient();
            client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(client_DownloadStringCompleted);
            client.DownloadStringAsync(url);
		}

        public void client_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                StringReader stream = new StringReader(e.Result);
                Data.MenuPage = (TileMenu)MenuItem.Load(stream)[0];
                Data.CurrentMenuPage = Data.MenuPage;
                //Data.CurrentMenuPage.bt_Click(new object(), new RoutedEventArgs());
            }

        }

        public void UpdatePrePageToCurrentPage()
        {//finished transition, after this function is called, update interface 2nd times.
            DataEventState.AfterAnimation(ref Data);
        }

        protected override void PrepareNotify()
        {
            DataEventState.BeforeAnimation(ref Data);

            UpdateParams = new UpdateInterfaceParams() { Type = Data.TurnTypeManager, ClikedImage = Data.ClickedImage };
        }
        
        public bool IsCanTransitionRight()
        {
            if (Data._nextPageRightPart == null && Data._nextPageLeftPart == null) 
                return false;
            return true;
        }

        #region Control Events From Outside
        //observer tamplate for control tile signal: to submenu page, to list tiles of article and article content
        List<int> listMenuIdx = new List<int>();//current lvl in here too
        List<int> listMenuPage = new List<int>();
        public void OnClickedTile(int Lvl, int PageIdx, int Idx)
        {//OnForwadMenu
            try
            {
                //if (Lvl != -1 && ((MenuItem)(Data.CurrentMenuPage.ListSubMenu[Data.iCurrentMenuPage].ListSubMenu[Idx])).ListSubMenu.Count == 0) return;
                if (Lvl != -1 && ((MenuItem)(Data.CurrentMenuPage.ListSubMenu[PageIdx].ListSubMenu[Idx])).ListSubMenu.Count == 0)
                {
                    MessageBox.Show("OnClickedTile: no sub menu!!!");
                    return;
                }
                   

                //            IsRightToLeftTransition = true;

                if (Lvl == -1)
                {

                }
                else
                {
                    listMenuIdx.Add(Idx);
                    //listMenuPage.Add(Data.iCurrentMenuPage);
                    listMenuPage.Add(PageIdx);
                }

                //get currentTiles
                Data.CurrentMenuPage = Data.MenuPage;
                for (int i = 0; i < listMenuIdx.Count; i++)
                {
                    Data.CurrentMenuPage = (TileMenu)(Data.CurrentMenuPage.ListSubMenu[listMenuPage[i]].ListSubMenu[listMenuIdx[i]]);
                }

                Data.iCurrentMenuPage = 0;
                DataEventState = new DataEventLoadMenu();
                BookState = BookLoaderState.MenuPage;
                Notify();
            }
            catch (Exception ex)
            {
                MessageBox.Show("OnClicked [Menu] Tile: " + ex.Message);
            }
        }

        void OnNextMenu()
        {
            if (Data.iCurrentMenuPage + 3 < Data.CurrentMenuPage.ListSubMenu.Count)
            {
                //IsRightToLeftTransition = true;
                DataEventState = new DataEventNextMenu();
                BookState = BookLoaderState.MenuPage;
                Notify();
            }
        }

        void OnPreviousMenu()
        {
            //IsRightToLeftTransition = true;
            if (Data.iCurrentMenuPage - 2 >= 0)
            {
                
                DataEventState = new DataEventPreviousMenu();
                BookState = BookLoaderState.MenuPage;
                Notify();
            }
        }

        void OnBackMenu()
        {
            try
            {
                if (listMenuIdx.Count == 0) return;
                //IsRightToLeftTransition = false;
                //get currentTiles
                Data.CurrentMenuPage = Data.MenuPage;
                for (int i = 0; i < listMenuIdx.Count - 1; i++)
                {
                    Data.CurrentMenuPage = (TileMenu)(Data.CurrentMenuPage.ListSubMenu[listMenuPage[i]].ListSubMenu[listMenuIdx[i]]);
                }
                listMenuIdx.RemoveAt(listMenuIdx.Count - 1);
                listMenuPage.RemoveAt(listMenuPage.Count - 1);

                BookState = BookLoaderState.MenuPage;
                //LoadMenu(0);
                DataEventState = new DataEventLoadMenu();
                Notify();
            }
            catch (Exception ex)
            {
                MessageBox.Show("OnBackMenu: "+ ex.Message);
            }
            
        }
        #endregion 
        #region Control Events From Article
        public void OnClickToArticle(string ArticleID)
        {
            //load new article with ID
            //apply to interface
            Data.CurrentArticle = new TileArticle();
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
                Data.CurrentArticle = (MenuItem)MenuItem.Load(stream)[0];
                Data.CurrentArticlePage = 0;
                //LoadArticlePage(0);
                DataEventState = new DataEventLoadArticlePage();
                BookState = BookLoaderState.ArticlePage;
                Notify();
            }
        }

        void LoadArticlePage(int iPage)
        {
            BookState = BookLoaderState.ArticlePage;

            Data.CurrentArticlePage = iPage;

            if (Data.CurrentArticlePage < Data.CurrentArticle.ListSubMenu.Count)
            {

                Data._nextPageRightPart = ((TilePage)Data.CurrentArticle.ListSubMenu[Data.CurrentArticlePage]).generatePage();
                Data._nextPageLeftPart = ((TilePage)Data.CurrentArticle.ListSubMenu[Data.CurrentArticlePage]).generatePage();
            }
            else
            {
                Data._currentPageRightPage = null;
                Data._nextPageRightPart = null;
                Data._nextPageLeftPart = null;
            }

            if (Data.CurrentArticlePage + 1 < Data.CurrentArticle.ListSubMenu.Count)
            {
                Data.PreNextPageRightPart = ((TilePage)Data.CurrentArticle.ListSubMenu[Data.CurrentArticlePage + 1]).generatePage();
                Data.PreNextPageLeftPart = ((TilePage)Data.CurrentArticle.ListSubMenu[Data.CurrentArticlePage + 1]).generatePage();
            }
            else
            {
                Data.PreNextPageRightPart = null;
                Data.PreNextPageLeftPart = null;
            }
        }

        void OnNextArticlePage()
        {
            if (Data.CurrentArticlePage + 3 < Data.CurrentArticle.ListSubMenu.Count)
            {
                //IsRightToLeftTransition = true;
                DataEventState = new DataEventNextArticlePage();
                BookState = BookLoaderState.ArticlePage;
                Notify();
            }
        }

        void OnPreviousArticlePage()
        {
            if (Data.CurrentArticlePage - 2 >= 0)
            {

                DataEventState = new DataEventPreviousArticlePage();
                BookState = BookLoaderState.ArticlePage;
                Notify();
            }
        }

        void OnBackArticle()
        {
            //if (listMenuIdx.Count == 0) return;
            BookState = BookLoaderState.MenuPage;
            DataEventState = new DataEventLoadMenu();
            Notify();
        }
        #endregion

        public void FirstCover()
        {
            DataEventState = new DataEventInitFirstCoverPage();
            Notify();
        }

        //Data.CurrentMenuPage.bt_Click(new object(), new RoutedEventArgs());
        public void GoToFirstMenuPage()
        {
            Data.CurrentMenuPage.bt_Click(new object(), new RoutedEventArgs());
            //DataEventState = new DataEventInitFirstCoverPage();
            //Notify();
        }

        public void OnClickedImage(Image img)
        {
            //Data.CurrentMenuPage.bt_Click(new object(), new RoutedEventArgs());
            Data.ClickedImage = img;

            DataEventState = new DataEventClickOnArticleImage();
            Notify();
        }

        public void OnNextPage()
        {
            if (BookState == BookLoaderState.MenuPage)
            {
                OnNextMenu();
                return;
            }
            if (BookState == BookLoaderState.ArticlePage)
            {
                OnNextArticlePage();
                return;
            }
        }


        public void OnPreviousPage()
        {
            if (BookState == BookLoaderState.MenuPage)
            {
                OnPreviousMenu();
                return;
            }
            if (BookState == BookLoaderState.ArticlePage)
            {
                OnPreviousArticlePage();
                return;
            }
        }

        public void OnBack()
        {
            if (BookState == BookLoaderState.MenuPage)
            {
                OnBackMenu();
                return;
            }
            if (BookState == BookLoaderState.ArticlePage)
            {
                OnBackArticle();
                return;
            }
        }


    }
}