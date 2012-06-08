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
using DataManager.DataEventController;
using DataManager;

namespace PageFlip.Data.DataEventController
{
    public class DataEventPreviousArticlePage : IDataEvent
    {
        public void BeforeAnimation(ref BookData Data)
        {
            Data.TurnTypeManager = TurnType.TurnFromLeft;

            Data._previousPageLeftPart = ((TilePage)Data.CurrentArticle.ListSubMenu[Data.CurrentArticlePage - 2]).generatePage();
            Data._previousPageRightPart = ((TilePage)Data.CurrentArticle.ListSubMenu[Data.CurrentArticlePage - 1]).generatePage();

            if (Data.CurrentArticlePage - 4 >= 0)
            {
                Data.PrePreviousPageLeftPart = ((TilePage)Data.CurrentArticle.ListSubMenu[Data.CurrentArticlePage - 4]).generatePage();
                Data.PrePreviousPageRightPart = ((TilePage)Data.CurrentArticle.ListSubMenu[Data.CurrentArticlePage - 3]).generatePage();
            }
            else
            {
                Data.PrePreviousPageLeftPart = null;
                Data.PrePreviousPageRightPart = null;
            }

            Data.CurrentArticlePage -= 2;
        }

        public void AfterAnimation(ref BookData Data)
        {
            Data._nextPageLeftPart = Data._currentPageLeftPage;
            Data._nextPageRightPart = Data._currentPageRightPage;

            Data._currentPageLeftPage = Data._previousPageLeftPart;
            Data._currentPageRightPage = Data._previousPageRightPart;

            Data._previousPageLeftPart = Data.PrePreviousPageLeftPart;
            Data._previousPageRightPart = Data.PrePreviousPageRightPart;

            Data.PrePreviousPageLeftPart = null;
            Data.PrePreviousPageRightPart = null;
        }
    }
}
