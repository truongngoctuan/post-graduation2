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

namespace DataManager
{
    public struct BookData
    {
        public TileMenu MenuPage;//global menu, unchange, keep all menu
        public TileMenu CurrentMenuPage;//only keep current lvl menu


        public UIElement _previousPageLeftPart;
        public UIElement _previousPageRightPart;

        public UIElement _currentPageLeftPage;
        public UIElement _currentPageRightPage;

        public UIElement _nextPageLeftPart;
        public UIElement _nextPageRightPart;

        //three these pages will be update two times:
        //1st: before transition page: update PrecurrentPage to nextpage
        //2nd: after transition page: update to 3 current page
        //public UIElement PreCurrentPage; PreCurrentPage == NextPageLeftPart == NextPageRightPart
        public UIElement PreNextPageLeftPart;
        public UIElement PreNextPageRightPart;

        public UIElement PrePreviousPageLeftPart;
        public UIElement PrePreviousPageRightPart;

        //variables for menu
        public int iCurrentMenuPage;

        //variables for Article
        public MenuItem CurrentArticle;
        public int CurrentArticlePage;

        //turn type
        public TurnType TurnTypeManager;
    }
}
