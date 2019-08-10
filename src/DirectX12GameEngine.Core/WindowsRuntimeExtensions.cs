using System.Numerics;
using Windows.Foundation;

namespace DirectX12GameEngine.Core
{
    public static class PointExtensions
    {
        public static Vector2 ToVector2(this in Point value)
        {
            return new Vector2((float)value.X, (float)value.Y);
        }

        public static Point ToPoint(this in Vector2 value)
        {
            return new Point(value.X, value.Y);
        }

        public static System.Drawing.PointF ToPointF(this in Point value)
        {
            return new System.Drawing.PointF((float)value.X, (float)value.Y);
        }

        public static Point ToPoint(this in System.Drawing.PointF value)
        {
            return new Point(value.X, value.Y);
        }
    }

    public static class RectangleExtensions
    {
        public static System.Drawing.RectangleF ToRectangleF(this in Rect value)
        {
            return new System.Drawing.RectangleF((float)value.X, (float)value.Y, (float)value.Width, (float)value.Height);
        }

        public static Rect ToRect(this in System.Drawing.RectangleF value)
        {
            return new Rect(value.X, value.Y, value.Width, value.Height);
        }
    }

    public static class SizeExtensions
    {
        public static Vector2 ToVector2(this in Size value)
        {
            return new Vector2((float)value.Width, (float)value.Height);
        }

        public static Size ToSize(this in Vector2 value)
        {
            return new Size(value.X, value.Y);
        }

        public static System.Drawing.SizeF ToSizeF(this in Size value)
        {
            return new System.Drawing.SizeF((float)value.Width, (float)value.Height);
        }

        public static Size ToSize(this in System.Drawing.SizeF value)
        {
            return new Size(value.Width, value.Height);
        }
    }
}
