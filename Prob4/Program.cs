using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageProcessing;

namespace Prob4
{
    class Program
    {
        static void Main(string[] args)
        {
            // ４．画像AまたはBに対し、濃度値が30～200に収まるようにトーンカーブを各自作成し、処理結果について考察せよ。
            //（1）画像A及び　画像Bが見やすくなるように直線のトーンカーブを設定し、濃度変換を行え。途中で、直線が変化してもよい（複数の直線濃度変換を行うこと）。
            // なぜ、そのようなトーンカーブにしたのかの画像A,Bのヒストグラム分布をもとに理由を示すこと。（画像A用のトーンカーブ、画像B用のトーンカーブ）
            //（2）ガンマ補正として　γ≐2,1/2として、濃度変換を行い、ヒストグラム分布を求めよ。

            Do2();
        }

        static void Do1()
        {
            // 画像読み込み
            var src = new GlayImage("A.pgm");

            // 階調の最小値と最大値を調べる
            var min = 255;
            var max = 0;
            for (var y = 0; y < src.Size.Height; ++y)
            {
                for (var x = 0; x < src.Size.Width; ++x)
                {
                    if (src[x, y] < min)
                    {
                        min = src[x, y];
                    }
                    if (src[x, y] > max)
                    {
                        max = src[x, y];
                    }
                }
            }

            // トーンカーブ作成
            var curve = new ToneCurve();
            for (var i = 0; i < 256; ++i)
            {
                if (i < min)
                {
                    curve[i] = 0;
                }
                else if (i > max)
                {
                    curve[i] = 255;
                }
                else
                {
                    curve[i] = (i - min) * (200 - 30) / (max - min) + 30;
                }
            }

            // 画像を変換
            var dest = src.Apply(curve);

            // 出力
            dest.Write("A_curve.pgm");
        }

        static void Do2()
        {
            // 画像読み込み
            var src = new GlayImage("A.pgm");

            foreach (var item in new List<Tuple<string, double>> { Tuple.Create("2_0", 2.0), Tuple.Create("0_5", 0.5) })
            {
                var name = item.Item1;
                var gamma = item.Item2;

                // ガンマ補正のトーンカーブ作成
                var curve = new ToneCurve();
                for (var i = 0; i < 256; ++i)
                {
                    var val = (int)(255 * Math.Pow(curve[i] / 255.0, 1 / gamma));
                    if (val >= 256)
                    {
                        val = 255;
                    }
                    curve[i] = val;
                }

                // 画像を変換
                var dest = src.Apply(curve);

                // 画像を出力
                dest.Write("A_gamma_" + name + ".pgm");

                // ヒストグラム画像を生成
                var hist = GetHistgramImage(dest);

                // ヒストグラム画像を出力
                hist.Write("A_gamma_" + name + "_hist.pgm");
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
