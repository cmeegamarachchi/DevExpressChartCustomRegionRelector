using System;
using System.Drawing;

namespace DevExpressChartCustomRegionSelector
{
    public static class DiagramToPointHelper
    {
        public static Rectangle CreateRectangle(Point corner1, Point corner2)
        {
            var x = corner1.X < corner2.X ? corner1.X : corner2.X;
            var y = corner1.Y < corner2.Y ? corner1.Y : corner2.Y;
            var width = Math.Abs(corner1.X - corner2.X);
            var height = Math.Abs(corner1.Y - corner2.Y);
            return new Rectangle(x, y, width, height);
        }

        public static Point GetLastSelectionCornerPosition(Point p, Rectangle bounds)
        {
            if (p.X < bounds.Left)
                p.X = bounds.Left;
            else if (p.X > bounds.Right)
                p.X = bounds.Right - 1;
            if (p.Y < bounds.Top)
                p.Y = bounds.Top;
            else if (p.Y > bounds.Bottom)
                p.Y = bounds.Bottom - 1;
            return p;
        }
    }
}