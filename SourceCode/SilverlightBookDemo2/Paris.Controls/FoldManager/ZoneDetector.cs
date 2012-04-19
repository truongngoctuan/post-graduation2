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

namespace Paris.Controls.FoldManager
{
    public class ZoneDetector
    {
        /// <summary>
        /// split book into 4 part: topleft, bottomleft, topright, bottomright; and center
        /// </summary>
        /// <param name="size">booksize</param>
        /// <param name="FoldSize"></param>
        /// <param name="pos"></param>
        /// <param name="Orientation"></param>
        /// <returns></returns>
        public static BookZone GetZone(Size size, double FoldSize, Point pos,
            System.Windows.Controls.Orientation Orientation)
        {
            //Size size = base.RenderSize;
            if (((pos.X < 0.0) || (pos.X > size.Width)) || ((pos.Y < 0.0) || (pos.Y > size.Height)))
            {
                return BookZone.Out;
            }
            if (Orientation == System.Windows.Controls.Orientation.Horizontal)
            {
                if (pos.X > (size.Width - FoldSize))
                {
                    if (pos.Y >= (size.Height / 2.0))
                    {
                        return BookZone.BottomRight;
                    }
                    return BookZone.TopRight;
                }
                if (pos.X < FoldSize)
                {
                    if (pos.Y >= (size.Height / 2.0))
                    {
                        return BookZone.BottomLeft;
                    }
                    return BookZone.TopLeft;
                }
            }
            else
            {
                if (pos.Y > (size.Height - FoldSize))
                {
                    if (pos.X >= (size.Width / 2.0))
                    {
                        return BookZone.BottomRight;
                    }
                    return BookZone.BottomLeft;
                }
                if (pos.Y < FoldSize)
                {
                    if (pos.X >= (size.Width / 2.0))
                    {
                        return BookZone.TopRight;
                    }
                    return BookZone.TopLeft;
                }
            }
            return BookZone.Center;
        }


        //private void UpdateZone(PageFoldVisibility ShowPageFold, ref BookZone CurrentZone, BookItem _fold, BookItem _dragged,
        //    BookZone zone)
        //{
        //    if ((ShowPageFold == PageFoldVisibility.OnMouseOver) && ((CurrentZone != zone) || (_fold == null)))
        //    {
        //        this.UndoFold();
        //        if (((_dragged == null) && (zone != BookZone.Center)) && (zone != BookZone.Out))
        //        {
        //            this.Fold(zone);
        //        }
        //    }
        //    CurrentZone = zone;
        //}
    }
}
