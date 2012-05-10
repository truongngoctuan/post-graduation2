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
using System.Collections.Generic;

namespace PageFlip.DataLoader
{
    public class MenuLoader
    {
        static string strMenuXml = string.Empty;
        static void LoadMenuXml()
        {
            strMenuXml = @"hard code";
        }

        public static List<object> LoadMenu(int iLvl)
        {
            switch (iLvl)
            {
                case 0:
                    {
                        return LoadMenuLvl0();
                    }
                default:
                    {
                        return null;
                    }
            }
        }

        static List<object> LoadMenuLvl0()
        {
            if (strMenuXml == string.Empty)
            {
                LoadMenuXml();
            }

            if (strMenuXml == string.Empty)
            {
                return null;
            }

            List<object> listMenuItem = new List<object>();
            for (int i = 0; i < 5; i++)
            {
                MenuItemLvl0 item = new MenuItemLvl0();
                item.ImagePath = "Images/MenuThumLvl" + i.ToString() + ".jpg";
                item.ImagePathDescription = "Images/MenuLvl" + i.ToString() + ".png";
                listMenuItem.Add(item);
            }

            return listMenuItem;
        }
    }
}
