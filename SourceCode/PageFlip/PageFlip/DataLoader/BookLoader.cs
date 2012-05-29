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
        TilePageMenu MenuPage;//global menu, unchange, keep all menu
        TilePageMenu CurrentMenuPage;//only keep current lvl menu

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
            MenuPage = new TilePageMenu();
            CurrentMenuPage = new TilePageMenu();

            //load data from menudata.xml
            Uri url = new Uri("menudata.xml", UriKind.Relative);
            WebClient client = new WebClient();
            client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(client_DownloadStringCompleted);
            client.DownloadStringAsync(url);
		}

        public void client_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                StringReader stream = new StringReader(e.Result);
                MenuPage.Tiles = TileMenu.Load(stream);
                CurrentMenuPage.Tiles = MenuPage.Tiles;

                IsRightToLeftTransition = true;
                LoadMenu(0);
                Notify();
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
            if (CurrentMenuPage.Tiles.Count == 0)
            {
                CurrentPage = null;
                NextPageRightPart = null;
                NextPageLeftPart = null;
                return;
            }
            iCurrentMenuPage = idx;

            NextPageRightPart = CurrentMenuPage.generatePage(idx);
            NextPageLeftPart = CurrentMenuPage.generatePage(idx);

            PreNextPageRightPart = CurrentMenuPage.generatePage(idx + 1);
            PreNextPageLeftPart = CurrentMenuPage.generatePage(idx + 1);
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
        public void OnClickedTile(int Lvl, int Idx)
        {
            IsRightToLeftTransition = true;
            listMenuIdx.Add(Idx);

            //get currentTiles
            List<Tile> tiles = MenuPage.Tiles;
            for (int i = 0; i < listMenuIdx.Count; i++)
            {
                tiles = tiles[listMenuIdx[i]].listSubMenu;
            }

            CurrentMenuPage.Tiles = tiles;

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
                List<Tile> tiles = MenuPage.Tiles;
                for (int i = 0; i < listMenuIdx.Count - 1; i++)
                {
                    tiles = tiles[listMenuIdx[i]].listSubMenu;
                }

                listMenuIdx.RemoveAt(listMenuIdx.Count - 1);
                CurrentMenuPage.Tiles = tiles;

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