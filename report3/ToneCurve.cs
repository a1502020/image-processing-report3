using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessing
{
    /// <summary>
    /// 256階調から256階調のトーンカーブを表す。
    /// </summary>
    class ToneCurve : IEquatable<ToneCurve>
    {
        /// <summary>
        /// ToneCurve を恒等写像で初期化する。
        /// </summary>
        public ToneCurve()
        {
            for (var i = 0; i < 256; ++i)
            {
                curve.Add(i);
            }
        }

        public int this[int src]
        {
            get
            {
                return curve[src];
            }
            set
            {
                if (src < 0 || src >= 256 || value < 0 || value >= 256)
                {
                    throw new ArgumentOutOfRangeException();
                }
                curve[src] = value;
            }
        }

        public ToneCurve Clone()
        {
            var res = new ToneCurve();
            for (var i = 0; i < 256; ++i)
            {
                res[i] = this[i];
            }
            return res;
        }

        public bool Equals(ToneCurve other)
        {
            if ((object)other == null)
            {
                return false;
            }

            return curve.Equals(other.curve);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            var other = obj as ToneCurve;
            return Equals(other);
        }

        public override int GetHashCode()
        {
            return curve.GetHashCode();
        }

        public static bool operator ==(ToneCurve a, ToneCurve b)
        {
            if ((object)a == null)
            {
                return (object)b == null;
            }

            return a.Equals(b);
        }

        public static bool operator !=(ToneCurve a, ToneCurve b)
        {
            if ((object)a == null)
            {
                return (object)b != null;
            }

            return a.Equals(b);
        }

        private List<int> curve = new List<int>();
    }
}
