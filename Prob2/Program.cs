using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageProcessing;

namespace Prob2
{
    class Program
    {
        static void Main(string[] args)
        {
            // ２．画像 A,B の反転処理を行い、ヒストグラム分布の変化を確認せよ。

            foreach (var name in new string[] { "A", "B" })
            {
                var img = new GlayImage(name + ".pgm");

                // 階調を反転
                for (var y = 0; y < img.Size.Height; ++y)
                {
                    for (var x = 0; x < img.Size.Width; ++x)
                    {
                        img[x, y] = 255 - img[x, y];
                    }
                }
                img.Write(name + "_rev.pgm");

                var imgHist = GetHistgramImage(img);
                imgHist.Write(name + "_rev_hist.pgm");
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
