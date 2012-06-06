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
    public class DataEventLoadMenu: IDataEvent
    {//chua chinh xac, can kiem tra lai sau
        public void BeforeAnimation(ref BookData Data)
        {
            //Data.iCurrentMenuPage = idx;
            #region Next Effect

            if (Data.iCurrentMenuPage - 2 >= 0)
            {
                Data._previousPageLeftPart = ((TilePage)Data.CurrentMenuPage.listSubMenu[Data.iCurrentMenuPage - 2]).generatePage();
                Data._previousPageRightPart = ((TilePage)Data.CurrentMenuPage.listSubMenu[Data.iCurrentMenuPage - 1]).generatePage();
            }
            else
            {
                Data._previousPageLeftPart = null;
                Data._previousPageRightPart = null;
            }

            if (Data.iCurrentMenuPage + 1 < Data.CurrentMenuPage.listSubMenu.Count)
            {//have 2 left, right pages
                Data._nextPageLeftPart = ((TilePage)Data.CurrentMenuPage.listSubMenu[Data.iCurrentMenuPage]).generatePage();
                Data._nextPageRightPart = ((TilePage)Data.CurrentMenuPage.listSubMenu[Data.iCurrentMenuPage + 1]).generatePage();

            }
            else
            {
                Data._currentPageRightPage = null;
                Data._nextPageRightPart = null;
                Data._nextPageLeftPart = null;
            }

            if (Data.iCurrentMenuPage + 3 < Data.CurrentMenuPage.listSubMenu.Count)
            {
                Data.PreNextPageLeftPart = ((TilePage)Data.CurrentMenuPage.listSubMenu[Data.iCurrentMenuPage + 2]).generatePage();
                Data.PreNextPageRightPart = ((TilePage)Data.CurrentMenuPage.listSubMenu[Data.iCurrentMenuPage + 3]).generatePage();
            }
            else
            {
                Data.PreNextPageRightPart = null;
                Data.PreNextPageLeftPart = null;
            }
            #endregion
        }

        public void AfterAnimation(ref BookData Data)
        {
            Data._currentPageLeftPage = Data._nextPageLeftPart;
            Data._currentPageRightPage = Data._nextPageRightPart;

            Data._nextPageLeftPart = Data.PreNextPageLeftPart;
            Data._nextPageRightPart = Data.PreNextPageRightPart;

            Data.PreNextPageLeftPart = null;
            Data.PreNextPageRightPart = null;
        }
    }
}
