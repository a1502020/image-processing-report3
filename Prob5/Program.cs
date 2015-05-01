using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageProcessing;

namespace Prob5
{
    class Program
    {
        static void Main(string[] args)
        {
            // ５．画像CまたはDに対し、ソラリゼーション（入力[0－255],出 力[0－254]の濃度値の間で振幅127,周期3の余弦波 曲線）を行い、ヒストグ ラム分布の変化を確認せよ。

            // ソラリゼーション用トーンカーブ作成
            var curve = new ToneCurve();
            for (var i = 0; i < 256; ++i)
            {
                curve[i] = (int)(127 * (Math.Cos(i * 3 * 2 * Math.PI / 256) + 1));
            }

            foreach (var name in new List<string> { "C", "D" })
            {
                // 画像読み込み
                var src = new GlayImage(name + ".pgm");

                // ヒストグラム画像を生成して出力
                new Histgram(src).GetHistgramImage().Write(name + "_hist.pgm");

                // ソラリゼーション適用
                var dest = src.Apply(curve);

                // 画像を出力
                dest.Write(name + "_solar.pgm");

                // ヒストグラム画像を生成して出力
                new Histgram(dest).GetHistgramImage().Write(name + "_solar_hist.pgm");
            }
        }
    }
}
