using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessing
{
    class Histgram
    {
        public Histgram()
        {
            for (var i = 0; i < 256; ++i)
            {
                hist.Add(0);
            }
        }

        public Histgram(GlayImage img)
        {
            for (var i = 0; i < 256; ++i)
            {
                hist.Add(0);
            }
            Set(img);
        }

        public void Set(GlayImage img)
        {
            for (var i = 0; i < 256; ++i)
            {
                hist[i] = 0;
            }

            for (var y = 0; y < img.Size.Height; ++y)
            {
                for (var x = 0; x < img.Size.Width; ++x)
                {
                    ++hist[img[x, y]];
                }
            }
        }

        /// <summary>
        /// ヒストグラム画像を取得する。
        /// </summary>
        /// <returns>ヒストグラム画像</returns>
        public GlayImage GetHistgramImage()
        {
            // 最大値を求める
            var max = 0;
            for (var i = 0; i < 256; ++i)
            {
                if (hist[i] > max)
                {
                    max = hist[i];
                }
            }

            // 画像を生成
            var res = new GlayImage(new Size(256, 256));
            for (var x = 0; x < 256; ++x)
            {
                for (var y = 0; y < 256; ++y)
                {
                    res[x, y] = (y * max / 255 > max - hist[x]) ? 255 : 0;
                }
            }

            return res;
        }

        /// <summary>
        /// 指定した階調値を持つ画素の数
        /// </summary>
        /// <param name="val">階調値</param>
        /// <returns>指定した階調値を持つ画素の数</returns>
        public int this[int val]
        {
            get
            {
                return hist[val];
            }
        }

        private List<int> hist = new List<int>();
    }
}
