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
    public class DataEventLoadArticlePage : DataEventLoadBasic
    {//chua chinh xac, can kiem tra lai sau
        public override void BeforeAnimation(ref BookData Data)
        {
            Data.TurnTypeManager = TurnType.TurnFromRight;
            if (Data.CurrentArticlePage < Data.CurrentArticle.ListSubMenu.Count)
            {

                Data._nextPageRightPart = ((TilePage)Data.CurrentArticle.ListSubMenu[Data.CurrentArticlePage]).generatePage();
                Data._nextPageLeftPart = ((TilePage)Data.CurrentArticle.ListSubMenu[Data.CurrentArticlePage]).generatePage();
            }


            Data.TurnTypeManager = TurnType.TurnFromRight;
            #region Next Effect

            if (Data.CurrentArticlePage - 2 >= 0)
            {
                Data._previousPageLeftPart = ((TilePage)Data.CurrentArticle.ListSubMenu[Data.CurrentArticlePage - 2]).generatePage();
                Data._previousPageRightPart = ((TilePage)Data.CurrentArticle.ListSubMenu[Data.CurrentArticlePage - 1]).generatePage();
            }
            else
            {
                Data._previousPageLeftPart = null;
                Data._previousPageRightPart = null;
            }

            if (Data.CurrentArticlePage + 1 < Data.CurrentArticle.ListSubMenu.Count)
            {//have 2 left, right pages
                Data._nextPageLeftPart = ((TilePage)Data.CurrentArticle.ListSubMenu[Data.CurrentArticlePage]).generatePage();
                Data._nextPageRightPart = ((TilePage)Data.CurrentArticle.ListSubMenu[Data.CurrentArticlePage + 1]).generatePage();

            }
            else
            {
                Data._currentPageLeftPage = null;
                Data._currentPageRightPage = null;
                Data._nextPageLeftPart = null;
                Data._nextPageRightPart = null;

            }

            if (Data.CurrentArticlePage + 3 < Data.CurrentArticle.ListSubMenu.Count)
            {
                Data.PreNextPageLeftPart = ((TilePage)Data.CurrentArticle.ListSubMenu[Data.CurrentArticlePage + 2]).generatePage();
                Data.PreNextPageRightPart = ((TilePage)Data.CurrentArticle.ListSubMenu[Data.CurrentArticlePage + 3]).generatePage();
            }
            else
            {
                Data.PreNextPageLeftPart = null;
                Data.PreNextPageRightPart = null;

            }
            #endregion
        }
    }
}
