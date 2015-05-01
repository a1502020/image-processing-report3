using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageProcessing;

namespace TestGlayImage
{
    class Program
    {
        static void Main(string[] args)
        {
            var img = new GlayImage();
            img.Read("A.pgm");
            img.Write("A_p2.pgm");
            img.Write("A_clone.pgm");
            var hist = new Histgram(img);
            hist.GetHistgramImage().Write("A_hist.pgm");
        }
    }
}
