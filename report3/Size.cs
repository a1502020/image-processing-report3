using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessing
{
    /// <summary>
    /// 2次元の大きさを表す。
    /// </summary>
    class Size : IEquatable<Size>
    {
        /// <summary>
        /// Size を初期化する。
        /// </summary>
        public Size()
        {
            Width = 0;
            Height = 0;
        }

        /// <summary>
        /// Size を (w, h) で初期化する。
        /// </summary>
        /// <param name="w">幅</param>
        /// <param name="h">高さ</param>
        public Size(int w, int h)
        {
            Width = w;
            Height = h;
        }

        /// <summary>
        /// 幅
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// 高さ
        /// </summary>
        public int Height { get; set; }

        public bool Equals(Size other)
        {
            if ((object)other == null)
            {
                return false;
            }

            return
                Width == other.Width
                && Height == other.Height;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            var other = obj as Size;
            return Equals(other);
        }

        public static bool operator ==(Size a, Size b)
        {
            if ((object)a == null)
            {
                return (object)b == null;
            }

            return a.Equals(b);
        }

        public static bool operator !=(Size a, Size b)
        {
            if ((object)a == null)
            {
                return (object)b != null;
            }

            return !a.Equals(b);
        }
    }
}
