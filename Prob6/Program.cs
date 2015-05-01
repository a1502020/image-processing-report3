using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using ImageProcessing;

namespace Prob6
{
    class Program
    {
        static void Main(string[] args)
        {
            // ６．画像CとDのαブレンディング（横方向）を行え。

            // 画像読み込み
            var img1 = new GlayImage("C.pgm");
            var img2 = new GlayImage("D.pgm");
            Debug.Assert(img1.Size == img2.Size);

            // 左が C の画像から始まり、右に進むに連れて徐々に D の画像になるようなアルファブレンド画像を作成
            for (var x = 0; x < img1.Size.Width; ++x)
            {
                var alpha = (double)x / (img1.Size.Width - 1);
                for (var y = 0; y < img1.Size.Height; ++y)
                {
                    img1[x, y] = (int)(img2[x, y] * alpha + img1[x, y] * (1 - alpha));
                }
            }

            // 画像出力
            img1.Write("C_D_alpha.pgm");
        }
    }
}
