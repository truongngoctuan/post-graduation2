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

namespace Paris.Controls
{
    internal static class VectorHelper
    {
        // Methods
        internal static Vector2 Center(Size size, Dock direction)
        {
            switch (direction)
            {
                case Dock.Left:
                    return new Vector2(0.0, size.Height / 2.0);

                case Dock.Right:
                    return new Vector2(size.Width, size.Height / 2.0);

                case Dock.Top:
                    return new Vector2(size.Width / 2.0, 0.0);

                case Dock.Bottom:
                    return new Vector2(size.Width / 2.0, size.Height);
            }
            return new Vector2();
        }

        internal static Vector2 CircleCenter(Vector2 a, Vector2 b, Vector2 c)
        {
            double num = (((((a.X * b.Y) + (b.X * c.Y)) + (c.X * a.Y)) - (a.X * c.Y)) - (b.X * a.Y)) - (c.X * b.Y);
            if (num == 0.0)
            {
                return new Vector2(double.PositiveInfinity, double.PositiveInfinity);
            }
            double num2 = (((((((a.X * a.X) + (a.Y * a.Y)) * b.Y) + (((b.X * b.X) + (b.Y * b.Y)) * c.Y)) + (((c.X * c.X) + (c.Y * c.Y)) * a.Y)) - (((a.X * a.X) + (a.Y * a.Y)) * c.Y)) - (((b.X * b.X) + (b.Y * b.Y)) * a.Y)) - (((c.X * c.X) + (c.Y * c.Y)) * b.Y);
            double num3 = (((((((a.X * a.X) + (a.Y * a.Y)) * b.X) + (((b.X * b.X) + (b.Y * b.Y)) * c.X)) + (((c.X * c.X) + (c.Y * c.Y)) * a.X)) - (((a.X * a.X) + (a.Y * a.Y)) * c.X)) - (((b.X * b.X) + (b.Y * b.Y)) * a.X)) - (((c.X * c.X) + (c.Y * c.Y)) * b.X);
            return new Vector2((0.5 * num2) / num, (-0.5 * num3) / num);
        }

        internal static Vector2 Direction(Dock direction)
        {
            switch (direction)
            {
                case Dock.Left:
                    return new Vector2(1.0, 0.0);

                case Dock.Right:
                    return new Vector2(-1.0, 0.0);

                case Dock.Top:
                    return new Vector2(0.0, 1.0);

                case Dock.Bottom:
                    return new Vector2(0.0, -1.0);
            }
            return new Vector2();
        }

        internal static Vector2 DirectionSymmetry(Dock direction, Vector2 v)
        {
            switch (direction)
            {
                case Dock.Left:
                case Dock.Right:
                    return new Vector2(-v.X, v.Y);

                case Dock.Top:
                case Dock.Bottom:
                    return new Vector2(v.X, -v.Y);
            }
            return v;
        }

        internal static IEnumerable<Vector2> FixedPoints(Size size, Dock direction)
        {
            switch (direction)
            {
                case Dock.Left:
                    return new Vector2[] { new Vector2(0.0, 0.0), new Vector2(0.0, size.Height) };

                case Dock.Right:
                    return new Vector2[] { new Vector2(size.Width, 0.0), new Vector2(size.Width, size.Height) };

                case Dock.Top:
                    return new Vector2[] { new Vector2(0.0, 0.0), new Vector2(size.Width, 0.0) };

                case Dock.Bottom:
                    return new Vector2[] { new Vector2(0.0, size.Height), new Vector2(size.Width, size.Height) };
            }
            return null;
        }

        internal static Vector2 Flip(Size size, Dock direction, Vector2 p)
        {
            switch (direction)
            {
                case Dock.Left:
                case Dock.Right:
                    return new Vector2(size.Width - p.X, p.Y);

                case Dock.Top:
                case Dock.Bottom:
                    return new Vector2(p.X, size.Height - p.Y);
            }
            return p;
        }

        internal static Vector2 LocalCoords(Size size, Dock direction, Vector2 p)
        {
            switch (direction)
            {
                case Dock.Left:
                    return (p - new Vector2(size.Width, 0.0));

                case Dock.Right:
                    return p;

                case Dock.Top:
                    return (p - new Vector2(0.0, size.Height));
            }
            return p;
        }

        internal static Transform Reflection(Vector2 dir, Vector2 point)
        {
            TransformGroup group = new TransformGroup();
            TranslateTransform transform = new TranslateTransform();
            transform.X=(-point.X);
            transform.Y=(-point.Y);
            group.Children.Add(transform);
            MatrixTransform transform2 = new MatrixTransform();
            transform2.Matrix = (new Matrix(1.0 - ((2.0 * dir.X) * dir.X), (-2.0 * dir.X) * dir.Y, (-2.0 * dir.X) * dir.Y, 1.0 - ((2.0 * dir.Y) * dir.Y), 0.0, 0.0));
            group.Children.Add(transform2);
            TranslateTransform transform3 = new TranslateTransform();
            transform3.X=(point.X);
            transform3.Y=(point.Y);
            group.Children.Add(transform3);
            return group;
        }

        internal static Vector2 Rotate(Vector2 center, double angle, Vector2 p)
        {
            double num = Math.Sin(angle);
            double num2 = Math.Cos(angle);
            p -= center;
            p = new Vector2((num2 * p.X) - (num * p.Y), (num * p.X) + (num2 * p.Y));
            return (p + center);
        }

        internal static Vector2 Symmetry(Size size, Dock direction, Vector2 p)
        {
            switch (direction)
            {
                case Dock.Left:
                    return new Vector2(-p.X, p.Y);

                case Dock.Right:
                    return new Vector2((2.0 * size.Width) - p.X, p.Y);

                case Dock.Top:
                    return new Vector2(p.X, -p.Y);

                case Dock.Bottom:
                    return new Vector2(p.X, (2.0 * size.Height) - p.Y);
            }
            return p;
        }
    }


}
