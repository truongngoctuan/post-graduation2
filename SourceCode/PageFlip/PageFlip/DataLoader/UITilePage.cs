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

namespace PageFlip.DataLoader
{
    public class Tile
    {
        Tile generate()
        {
            return new Tile();
        }
    }

    public class TileMainMenu:Tile
    {

    }

    public class TileSubMenu : Tile
    {

    }

    public class TileArticle : Tile
    {

    }
    public class UITilePage
    {//generate a page from multiple tiles
        //tiles in this case mean chapter thumnails

        UIElement generatePage()
        {
            return null;
        }

    }

    public class UITileMainMenuPage : UITilePage
    {
    }

    public class UITileSubMenuPage : UITilePage
    {
    }

    public class UITileArticlesPage : UITilePage
    {
    }
}
