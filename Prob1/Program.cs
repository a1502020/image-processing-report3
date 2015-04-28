using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageProcessing;

namespace Prob1
{
    class Program
    {
        static void Main(string[] args)
        {
            // １．画像A,Bのヒストグラム分布を求めよ（C言語による画像処理論　参照）。

            foreach (var name in new string[] { "A", "B" })
            {
                var img = new GlayImage(name + ".pgm");
                var imgHist = GetHistgramImage(img);
                imgHist.Write(name + "_hist.pgm");
            }
        }

        static GlayImage GetHistgramImage(GlayImage src)
        {
            var hist = new int[256]; // 0 で初期化
            var maxIndex = 0;
            var maxValue = 0;
            for (var y = 0; y < src.Size.Height; ++y)
            {
                for (var x = 0; x < src.Size.Width; ++x)
                {
                    var v = src[x, y];
                    ++hist[v];
                    if (hist[v] > maxValue)
                    {
                        maxIndex = v;
                        maxValue = hist[v];
                    }
                }
            }

            var res = new GlayImage(new Size(256, 256));
            for (var x = 0; x < 256; ++x)
            {
                for (var y = 0; y < 256; ++y)
                {
                    res[x, y] = (y * maxValue / 255 > maxValue - hist[x]) ? 255 : 0;
                }
            }

            return res;
        }
    }
}
