using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageProcessing;

namespace Prob3
{
    class Program
    {
        static void Main(string[] args)
        {
            // ３．１，２におけるそれぞれのコントラストを求めよ。

            foreach (var name in new string[] { "A", "B", "A_rev", "B_rev" })
            {
                var img = new GlayImage(name + ".pgm");

                var minValue = 255;
                var maxValue = 0;
                for (var y = 0; y < img.Size.Height; ++y)
                {
                    for (var x = 0; x < img.Size.Width; ++x)
                    {
                        if (img[x, y] > maxValue)
                        {
                            maxValue = img[x, y];
                        }
                        if (img[x, y] < minValue)
                        {
                            minValue = img[x, y];
                        }
                    }
                }

                Console.WriteLine("{0} のコントラスト : {1}", name, maxValue - minValue);
            }
        }
    }
}
