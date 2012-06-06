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
    public class DataEventNextMenu: IDataEvent
    {
        public void BeforeAnimation(ref BookData Data)
        {
            //Data.iCurrentMenuPage = idx;
            #region Next Effect
            Data._nextPageLeftPart = ((TilePage)Data.CurrentMenuPage.listSubMenu[Data.iCurrentMenuPage + 2]).generatePage();
            Data._nextPageRightPart = ((TilePage)Data.CurrentMenuPage.listSubMenu[Data.iCurrentMenuPage + 3]).generatePage();

            if (Data.iCurrentMenuPage + 5 < Data.CurrentMenuPage.listSubMenu.Count)
            {
                Data.PreNextPageLeftPart = ((TilePage)Data.CurrentMenuPage.listSubMenu[Data.iCurrentMenuPage + 4]).generatePage();
                Data.PreNextPageRightPart = ((TilePage)Data.CurrentMenuPage.listSubMenu[Data.iCurrentMenuPage + 5]).generatePage();
            }

            Data.iCurrentMenuPage += 2;
            #endregion
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
