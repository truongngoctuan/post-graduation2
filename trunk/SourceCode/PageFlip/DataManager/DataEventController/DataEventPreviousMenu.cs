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
    public class DataEventPreviousMenu : IDataEvent
    {
        public void BeforeAnimation(ref BookData Data)
        {
            Data.TurnTypeManager = TurnType.TurnLeft;

            Data._previousPageLeftPart = ((TilePage)Data.CurrentMenuPage.ListSubMenu[Data.iCurrentMenuPage - 2]).generatePage();
            Data._previousPageRightPart = ((TilePage)Data.CurrentMenuPage.ListSubMenu[Data.iCurrentMenuPage - 1]).generatePage();

            if (Data.iCurrentMenuPage - 4 >= 0)
            {
                Data.PrePreviousPageLeftPart = ((TilePage)Data.CurrentMenuPage.ListSubMenu[Data.iCurrentMenuPage - 4]).generatePage();
                Data.PrePreviousPageRightPart = ((TilePage)Data.CurrentMenuPage.ListSubMenu[Data.iCurrentMenuPage - 3]).generatePage();
            }
            else
            {
                Data.PrePreviousPageLeftPart = null;
                Data.PrePreviousPageRightPart = null;
            }

            Data.iCurrentMenuPage -= 2;
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
