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
using PageFlipUltis;
using System.IO;
using System.Xml;
namespace PageFlip.DataLoader
{
	public class BookLoader:Subject
    {
        TileMenu MenuPage;//global menu, unchange, keep all menu
        TileMenu CurrentMenuPage;//only keep current lvl menu

        public UIElement CurrentPage;
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
            Uri url = new Uri("menudata2.xml", UriKind.Relative);
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
                //CurrentMenuPage.InitParams(new List<object>(){
                //    ((TileMenu)MenuPage.Tiles[0]).NGridRows,
                //    ((TileMenu)MenuPage.Tiles[0]).NGridColumns
                //});
                CurrentMenuPage = MenuPage;

                //IsRightToLeftTransition = true;
                //LoadMenu(0);
                //OnClickedTile(0, 0);
                CurrentMenuPage.bt_Click(new object(), new RoutedEventArgs());
                //Notify();
            }

        }

        public void UpdatePrePageToCurrentPage()
        {//finished transition, after this function is called, update interface 2nd times.
            CurrentPage = NextPageLeftPart;
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

            if (iCurrentMenuPage < CurrentMenuPage.listSubMenu.Count)
            {
                
                NextPageRightPart = ((TilePageMenu)CurrentMenuPage.listSubMenu[iCurrentMenuPage]).generatePage();
                NextPageLeftPart = ((TilePageMenu)CurrentMenuPage.listSubMenu[iCurrentMenuPage]).generatePage();
            }
            else
            {
                CurrentPage = null;
                NextPageRightPart = null;
                NextPageLeftPart = null;
            }

            if (iCurrentMenuPage + 1 < CurrentMenuPage.listSubMenu.Count)
            {
                PreNextPageRightPart = ((TilePageMenu)CurrentMenuPage.listSubMenu[iCurrentMenuPage + 1]).generatePage();
                PreNextPageLeftPart = ((TilePageMenu)CurrentMenuPage.listSubMenu[iCurrentMenuPage + 1]).generatePage();
            }
            else
            {
                PreNextPageRightPart = null;
                PreNextPageLeftPart = null;
            }
            
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
    }
}
