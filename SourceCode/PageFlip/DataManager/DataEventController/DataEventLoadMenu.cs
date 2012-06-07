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
    public class DataEventLoadMenu: DataEventLoadBasic
    {//chua chinh xac, can kiem tra lai sau
        public override void BeforeAnimation(ref BookData Data)
        {
            Data.TurnTypeManager = TurnType.TurnFromRight;
            BeforeAnimationBasic(ref Data, Data.iCurrentMenuPage, ref Data.CurrentMenuPage);
                        
            //#region Next Effect

            //if (Data.iCurrentMenuPage - 2 >= 0)
            //{
            //    Data._previousPageLeftPart = ((TilePage)Data.CurrentMenuPage.ListSubMenu[Data.iCurrentMenuPage - 2]).generatePage();
            //    Data._previousPageRightPart = ((TilePage)Data.CurrentMenuPage.ListSubMenu[Data.iCurrentMenuPage - 1]).generatePage();
            //}
            //else
            //{
            //    Data._previousPageLeftPart = null;
            //    Data._previousPageRightPart = null;
            //}

            //if (Data.iCurrentMenuPage + 1 < Data.CurrentMenuPage.ListSubMenu.Count)
            //{//have 2 left, right pages
            //    Data._nextPageLeftPart = ((TilePage)Data.CurrentMenuPage.ListSubMenu[Data.iCurrentMenuPage]).generatePage();
            //    Data._nextPageRightPart = ((TilePage)Data.CurrentMenuPage.ListSubMenu[Data.iCurrentMenuPage + 1]).generatePage();

            //}
            //else
            //{
            //    if (CurrentMenuItem.ListSubMenu.Count == 1)
            //    {
            //        Data._nextPageRightPart = ((TilePage)CurrentMenuItem.ListSubMenu[iCurrenPage]).generatePage();
            //    }
            //    else
            //    {
            //        Data._currentPageLeftPage = null;
            //        Data._currentPageRightPage = null;
            //        Data._nextPageLeftPart = null;
            //        Data._nextPageRightPart = null;
            //    }
            //}

            //if (Data.iCurrentMenuPage + 3 < Data.CurrentMenuPage.ListSubMenu.Count)
            //{
            //    Data.PreNextPageLeftPart = ((TilePage)Data.CurrentMenuPage.ListSubMenu[Data.iCurrentMenuPage + 2]).generatePage();
            //    Data.PreNextPageRightPart = ((TilePage)Data.CurrentMenuPage.ListSubMenu[Data.iCurrentMenuPage + 3]).generatePage();
            //}
            //else
            //{
            //    Data.PreNextPageLeftPart = null;
            //    Data.PreNextPageRightPart = null;
                
            //}
            //#endregion
        }

    }
}
