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

namespace DataManager.DataEventController
{
    public class DataEventNextArticlePage : IDataEvent
    {
        public void BeforeAnimation(ref BookData Data)
        {
            Data.TurnTypeManager = TurnType.TurnFromRight;

            Data._nextPageLeftPart = ((TilePage)Data.CurrentArticle.ListSubMenu[Data.CurrentArticlePage + 2]).generatePage();
            Data._nextPageRightPart = ((TilePage)Data.CurrentArticle.ListSubMenu[Data.CurrentArticlePage + 3]).generatePage();

            if (Data.CurrentArticlePage + 5 < Data.CurrentArticle.ListSubMenu.Count)
            {
                Data.PreNextPageLeftPart = ((TilePage)Data.CurrentArticle.ListSubMenu[Data.CurrentArticlePage + 4]).generatePage();
                Data.PreNextPageRightPart = ((TilePage)Data.CurrentArticle.ListSubMenu[Data.CurrentArticlePage + 5]).generatePage();
            }
            else
            {
                Data.PreNextPageLeftPart = null;
                Data.PreNextPageRightPart = null;
            }

            Data.CurrentArticlePage += 2;
        }

        public void AfterAnimation(ref BookData Data)
        {
            Data._previousPageLeftPart = Data._currentPageLeftPage;
            Data._previousPageRightPart = Data._currentPageRightPage;

            Data._currentPageLeftPage = Data._nextPageLeftPart;
            Data._currentPageRightPage = Data._nextPageRightPart;

            Data._nextPageLeftPart = Data.PreNextPageLeftPart;
            Data._nextPageRightPart = Data.PreNextPageRightPart;

            Data.PreNextPageLeftPart = null;
            Data.PreNextPageRightPart = null;
        }
    }
}
