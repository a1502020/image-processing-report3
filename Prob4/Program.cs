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

            Do1();
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
    }
}
