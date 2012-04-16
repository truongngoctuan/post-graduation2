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

namespace Paris.Controls
{
    internal class BookFoldTransition : BookTransition
    {
        // Fields
        private const float InnerShadow = 150f;
        private const float OuterShadow = 8f;

        // Methods
        private static void AddShadow(BookItem element, Vector2 point, Vector2 dir, Size size, bool showOuterShadows, bool showInnerShadows)
        {
            if (!showInnerShadows && !showOuterShadows)
            {
                element.Effect = (null);
            }
            else
            {
                Size s = new Size((element.Direction == Dock.Left) ? ((double)0f) : ((double)8f), (element.Direction == Dock.Top) ? ((double)0f) : ((double)8f));
                Size size3 = size;
                size.Width =(size.Width + (s.Width + ((element.Direction == Dock.Right) ? ((double)0f) : ((double)8f))));
                size.Height = (size.Height + (s.Height + ((element.Direction == Dock.Bottom) ? ((double)0f) : ((double)8f))));
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

        public override void Turn(BookItem item, bool showOuterShadows, bool showInnerShadows, Size bookSize)
        {
            Size size = item.RenderSize;
            if (size.Width == 0.0)
            {
                size = ((item.Direction == Dock.Left) || (item.Direction == Dock.Right)) ? new Size(bookSize.Width / 2.0, bookSize.Height) : new Size(bookSize.Width, bookSize.Height / 2.0);
            }
            if (!item.TransitionFront)
            {
                item.RenderTransform = (null);
                item.Clip = (null);
                AddShadow(item, VectorHelper.Center(size, item.Direction), VectorHelper.Direction(item.Direction), size, showOuterShadows, showInnerShadows);
                if (item.DragStart == item.DragCurrent)
                {
                    return;
                }
            }
            Dock direction = item.TransitionFront ? item.Direction : this.Opposite(item.Direction);
            Vector2 p = item.DragStart * bookSize;
            p = VectorHelper.Symmetry(bookSize, direction, p);
            p = VectorHelper.Flip(bookSize, direction, p);
            p = VectorHelper.LocalCoords(size, direction, p);
            Vector2 drag = VectorHelper.LocalCoords(size, direction, item.DragCurrent * bookSize);
            drag = BoundDrag(size, direction, p, drag);
            Vector2 vector3 = VectorHelper.Symmetry(size, direction, p);
            Vector2 vector4 = (Vector2)((drag + vector3) / 2.0);
            Vector2 point = VectorHelper.Center(size, direction);
            Vector2 vector10 = (drag != vector3) ? (vector3 - drag) : (drag - point);
            Vector2 normalized = vector10.Normalized;
            Vector2 vector7 = VectorHelper.Symmetry(size, direction, vector4);
            if (item.TransitionFront)
            {
                TransformGroup group = new TransformGroup();
                group.Children.Add(VectorHelper.Reflection(VectorHelper.Direction(direction), point));
                group.Children.Add(VectorHelper.Reflection(normalized, vector4));
                item.RenderTransform=(group);
                Vector2 dir = VectorHelper.DirectionSymmetry(direction, normalized);
                Vector2 midpoint = showInnerShadows ? (vector7 - ((Vector2)(dir * 150.0))) : vector7;
                item.Clip=(Clip(midpoint, dir, size));
                AddShadow(item, vector7, dir, size, showOuterShadows, showInnerShadows);
            }
            else
            {
                item.Clip=(Clip(VectorHelper.Flip(size, direction, vector7), -normalized, size));
            }
        }
    }


}
