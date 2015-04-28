using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessing
{
    /// <summary>
    /// 2次元の点を表す。
    /// </summary>
    class Point : IEquatable<Point>
    {
        /// <summary>
        /// Point を (0, 0) で初期化する。
        /// </summary>
        public Point()
        {
            X = 0;
            Y = 0;
        }

        /// <summary>
        /// Point を (x, y) で初期化する。
        /// </summary>
        /// <param name="x">X 座標</param>
        /// <param name="y">Y 座標</param>
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// X 座標
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Y 座標
        /// </summary>
        public int Y { get; set; }

        public bool Equals(Point other)
        {
            if ((object)other != null)
            {
                return false;
            }

            return
                X == other.X
                && Y == other.Y;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            var other = obj as Point;
            return Equals(other);
        }

        public static bool operator ==(Point a, Point b)
        {
            if ((object)a == null)
            {
                return (object)b == null;
            }

            return a.Equals(b);
        }

        public static bool operator !=(Point a, Point b)
        {
            if ((object)a == null)
            {
                return (object)b != null;
            }

            return !a.Equals(b);
        }
    }
}
