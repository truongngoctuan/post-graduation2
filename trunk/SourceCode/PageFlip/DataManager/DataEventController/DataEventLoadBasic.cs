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
    public class DataEventLoadBasic : IDataEvent
    {//chua chinh xac, can kiem tra lai sau
        public virtual void BeforeAnimation(ref BookData Data)
        {
        }

        public void BeforeAnimationBasic(ref BookData Data, int iCurrenPage, ref TileMenu CurrentMenuItem)
        {
            Data.TurnTypeManager = TurnType.TurnFromRight;
            if (iCurrenPage - 2 >= 0)
            {
                Data._previousPageLeftPart = ((TilePage)CurrentMenuItem.ListSubMenu[iCurrenPage - 2]).generatePage();
                Data._previousPageRightPart = ((TilePage)CurrentMenuItem.ListSubMenu[iCurrenPage - 1]).generatePage();
            }
            else
            {
                Data._previousPageLeftPart = null;
                Data._previousPageRightPart = null;
            }

            if (iCurrenPage + 1 < CurrentMenuItem.ListSubMenu.Count)
            {//have 2 left, right pages
                Data._nextPageLeftPart = ((TilePage)CurrentMenuItem.ListSubMenu[iCurrenPage]).generatePage();
                Data._nextPageRightPart = ((TilePage)CurrentMenuItem.ListSubMenu[iCurrenPage + 1]).generatePage();

            }
            else
            {
                if (CurrentMenuItem.ListSubMenu.Count == 1)
                {
                    Data._nextPageRightPart = ((TilePage)CurrentMenuItem.ListSubMenu[iCurrenPage]).generatePage();
                }
                else
                {
                    Data._currentPageLeftPage = null;
                    Data._currentPageRightPage = null;
                    Data._nextPageLeftPart = null;
                    Data._nextPageRightPart = null;
                }
            }

            if (iCurrenPage + 3 < CurrentMenuItem.ListSubMenu.Count)
            {
                Data.PreNextPageLeftPart = ((TilePage)CurrentMenuItem.ListSubMenu[iCurrenPage + 2]).generatePage();
                Data.PreNextPageRightPart = ((TilePage)CurrentMenuItem.ListSubMenu[iCurrenPage + 3]).generatePage();
            }
            else
            {
                Data.PreNextPageLeftPart = null;
                Data.PreNextPageRightPart = null;

            }
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
