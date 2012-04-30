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
using PageFlip;

namespace Paris.Controls
{

    public enum Dock : byte
    {
        Bottom = 3,
        Left = 0,
        Right = 1,
        Top = 2
    }

    internal class BookFoldTransition
    {
        // Fields
        private const float InnerShadow = 150f;
        private const float OuterShadow = 8f;

        // Methods
        private static void AddShadow(UIElement element, Vector2 point, Vector2 dir, Size size, bool showOuterShadows, bool showInnerShadows)
        {
            if (!showInnerShadows && !showOuterShadows)
            {
                element.Effect = (null);
            }
            else
            {
                //Size s = new Size((element.Direction == Dock.Left) ? ((double)0f) : ((double)8f), (element.Direction == Dock.Top) ? ((double)0f) : ((double)8f));
                //Size size3 = size;
                //size.Width =(size.Width + (s.Width + ((element.Direction == Dock.Right) ? ((double)0f) : ((double)8f))));
                //size.Height = (size.Height + (s.Height + ((element.Direction == Dock.Bottom) ? ((double)0f) : ((double)8f))));

                Size s = new Size(0, 0);
                Size size3 = size;
                size.Width = (size.Width + s.Width);
                size.Height = (size.Height + s.Height);

                Vector2 v = point + new Vector2(s);
                FoldEffect effect = element.Effect as FoldEffect;
                if (effect == null)
                {
                    element.Effect = (effect = new FoldEffect());
                }
                effect.Size = size;
                effect.ContentSize = size3;
                effect.Padding = s;
                effect.RectAB = (Point)dir;
                effect.RectC = -dir.Dot(v);
                effect.OuterShadows = showOuterShadows ? 0.125f : 0f;
                effect.InnerShadows = showInnerShadows ? 0.006666667f : 0f;
            }
        }

        private static Vector2 BoundDrag(Size size, Dock direction, Vector2 dragStart, Vector2 drag)
        {
            foreach (Vector2 vector in VectorHelper.FixedPoints(size, direction))
            {
                Vector2 vector2 = dragStart - vector;
                double length = vector2.Length;
                Vector2 vector3 = drag - vector;
                if (vector3.Length > length)
                {
                    Vector2 vector4 = drag - vector;
                    drag = vector + ((Vector2)(vector4.Normalized * length));
                }
            }
            return drag;
        }

        private static Geometry Clip(Vector2 midpoint, Vector2 dir, Size size)
        {
            dir = (Vector2)((dir.Normalized * Math.Max(size.Width, size.Height)) * 2.0);
            Vector2 vector = new Vector2(-dir.Y, dir.X);
            PathGeometry geometry = new PathGeometry();
            PathFigureCollection figures = new PathFigureCollection();
            PathFigure figure = new PathFigure();
            figure.IsClosed=(true);
            figure.StartPoint=((Point)midpoint);
            PathSegmentCollection segments = new PathSegmentCollection();
            PolyLineSegment segment = new PolyLineSegment();
            PointCollection points = new PointCollection();
            points.Add((Point)(midpoint + vector));
            points.Add((Point)((midpoint + vector) + dir));
            points.Add((Point)((midpoint - vector) + dir));
            points.Add((Point)(midpoint - vector));
            segment.Points=(points);
            segments.Add(segment);
            figure.Segments=(segments);
            figures.Add(figure);
            geometry.Figures=(figures);
            return geometry;
        }

        private Dock Opposite(Dock dir)
        {
            switch (dir)
            {
                case Dock.Left:
                    return Dock.Right;

                case Dock.Right:
                    return Dock.Left;

                case Dock.Top:
                    return Dock.Bottom;

                case Dock.Bottom:
                    return Dock.Top;
            }
            return dir;
        }

        public void Turn(UIElement item, bool showOuterShadows, bool showInnerShadows, Size bookSize)
        {

            Size size = bookSize;
            //if (size.Width == 0.0)
            //{
            //    size = ((item.Direction == Dock.Left) || (item.Direction == Dock.Right)) ? new Size(bookSize.Width / 2.0, bookSize.Height) : new Size(bookSize.Width, bookSize.Height / 2.0);
            //}
            //if (!item.TransitionFront)
            //{
                
            //}
            item.RenderTransform = (null);
            item.Clip = (null);

            //AddShadow(item, VectorHelper.Center(new Size(size.Width / 2, size.Height), item.Direction), VectorHelper.Direction(item.Direction), new Size(size.Width / 2, size.Height), showOuterShadows, showInnerShadows);
            AddShadow(item, VectorHelper.Center(new Size(size.Width, size.Height), Dock.Right), VectorHelper.Direction(Dock.Right), new Size(size.Width, size.Height), showOuterShadows, showInnerShadows);
            //if (item.DragStart == item.DragCurrent)
            //{
            //    return;
            //}

        }
    }


}
