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
using System.Runtime.InteropServices;

namespace Paris.Controls
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct Vector2
    {
        public double X;
        public double Y;
        public Vector2(Point p)
        {
            this.X = p.X;
            this.Y = p.Y;
        }

        public Vector2(Size s)
        {
            this.X = s.Width;
            this.Y = s.Height;
        }

        public Vector2(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        public Vector2 Normalized
        {
            get
            {
                return (Vector2)(this / this.Length);
            }
        }
        public double Length
        {
            get
            {
                return Math.Sqrt((this.X * this.X) + (this.Y * this.Y));
            }
        }
        public static Vector2 operator +(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.X + v2.X, v1.Y + v2.Y);
        }

        public static Vector2 operator -(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.X - v2.X, v1.Y - v2.Y);
        }

        public static Vector2 operator *(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.X * v2.X, v1.Y * v2.Y);
        }

        public static Vector2 operator *(Vector2 v1, Size s)
        {
            return new Vector2(v1.X * s.Width, v1.Y * s.Height);
        }

        public static Vector2 operator /(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.X / v2.X, v1.Y / v2.Y);
        }

        public static Vector2 operator /(Vector2 v1, Size s)
        {
            return new Vector2(v1.X / s.Width, v1.Y / s.Height);
        }

        public static Vector2 operator -(Vector2 v)
        {
            return new Vector2(-v.X, -v.Y);
        }

        public static Vector2 operator *(Vector2 v, double d)
        {
            return new Vector2(v.X * d, v.Y * d);
        }

        public static Vector2 operator *(double d, Vector2 v)
        {
            return (Vector2)(v * d);
        }

        public static Vector2 operator /(Vector2 v, double d)
        {
            return (Vector2)(v * (1.0 / d));
        }

        public double Dot(Vector2 v)
        {
            return ((this.X * v.X) + (this.Y * v.Y));
        }

        public static bool operator ==(Vector2 v1, Vector2 v2)
        {
            return ((v1.X == v2.X) && (v1.Y == v2.Y));
        }

        public static bool operator !=(Vector2 v1, Vector2 v2)
        {
            return !(v1 == v2);
        }

        public double Angle(Vector2 b)
        {
            Vector2 normalized = this.Normalized;
            b = b.Normalized;
            return (Math.Sign(normalized.Dot(new Vector2(-b.Y, b.X))) * Math.Acos(Math.Min(1.0, Math.Max(-1.0, normalized.Dot(b)))));
        }

        public static implicit operator Point(Vector2 v)
        {
            return new Point(v.X, v.Y);
        }

        public static implicit operator Vector2(Point v)
        {
            return new Vector2(v.X, v.Y);
        }

        public override string ToString()
        {
            Point point = ((Point)this);
            return point.ToString();
        }
    }


}
