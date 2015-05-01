using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ImageProcessing
{
    class GlayImage
    {
        public GlayImage()
        {
        }

        public GlayImage(string path)
        {
            Read(path);
        }

        public GlayImage(Size size)
        {
            Size = size;
        }

        public void Read(string path)
        {
            var state = State.Header;
            var type = "P2";
            var y = 0;

            using (var reader = new BinaryReader(File.OpenRead(path)))
            {
                while (state != State.Done)
                {
                    if (state == State.Header)
                    {
                        type = readLine(reader);
                        if (type != "P2" && type != "P5")
                        {
                            throw new NotImplementedException("P2, P5 形式以外は未対応です。");
                        }
                        state = State.Size;
                    }
                    else if (state == State.Size)
                    {
                        var line = readLine(reader);
                        if (line.StartsWith("#"))
                        {
                            continue;
                        }
                        var values = line.Split(new char[] { ' ' }).Select(str => int.Parse(str)).ToList();
                        Size = new Size(values[0], values[1]);
                        state = State.MaxValue;
                    }
                    else if (state == State.MaxValue)
                    {
                        var line = readLine(reader);
                        if (line.StartsWith("#"))
                        {
                            continue;
                        }
                        if (line != "255")
                        {
                            throw new NotImplementedException("256階調以外は未対応です。");
                        }
                        state = (type == "P2") ? State.ContentP2 : State.ContentP5;
                    }
                    else if (state == State.ContentP2)
                    {
                        var values = readLine(reader)
                            .Split(new char[] { ' ' })
                            .Select(str => int.Parse(str)).ToList();
                        for (var x = 0; x < Size.Width; ++x)
                        {
                            image[x][y] = values[x];
                        }
                        ++y;
                        if (y >= Size.Height)
                        {
                            state = State.Done;
                        }
                    }
                    else if (state == State.ContentP5)
                    {
                        for (var x = 0; x < Size.Width; ++x)
                        {
                            image[x][y] = reader.ReadByte();
                        }
                        ++y;
                        if (y >= Size.Height)
                        {
                            state = State.Done;
                        }
                    }
                }
            }
        }

        public void Write(string path, string type = "P5")
        {
            if (type != "P2" && type != "P5")
            {
                throw new NotImplementedException("P2, P5 形式以外は未対応です。");
            }

            if (type == "P2")
            {
                using (var writer = new StreamWriter(path))
                {
                    for (var y = 0; y < Size.Height; ++y)
                    {
                        for (var x = 0; x < Size.Width; ++x)
                        {
                            if (x != 0)
                            {
                                writer.Write(" ");
                            }
                            writer.Write("{0}", image[x][y]);
                        }
                        writer.WriteLine();
                    }
                }
            }
            else
            {
                var sjis = Encoding.GetEncoding("Shift_JIS");
                using (var writer = new BinaryWriter(File.OpenWrite(path)))
                {
                    writeLine(writer, type);
                    writeLine(writer, String.Format("{0} {1}", Size.Width, Size.Height));
                    writeLine(writer, String.Format("{0}", 255));
                    for (var y = 0; y < Size.Height; ++y)
                    {
                        for (var x = 0; x < Size.Width; ++x)
                        {
                            writer.Write((byte)image[x][y]);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 画像の大きさ
        /// </summary>
        public Size Size
        {
            get
            {
                return size;
            }
            set
            {
                var newImg = new List<List<int>>();
                for (var y = 0; y < value.Height; ++y)
                {
                    var line = new List<int>();
                    for (var x = 0; x < value.Width; ++x)
                    {
                        if (x < size.Width && y < size.Height)
                        {
                            line.Add(image[x][y]);
                        }
                        else
                        {
                            line.Add(0);
                        }
                    }
                    newImg.Add(line);
                }
                image = newImg;
                size = value;
            }
        }

        /// <summary>
        /// 画素
        /// </summary>
        /// <param name="x">X 座標</param>
        /// <param name="y">Y 座標</param>
        /// <returns>階調値</returns>
        public int this[int x, int y]
        {
            get
            {
                return image[x][y];
            }
            set
            {
                image[x][y] = value;
            }
        }

        /// <summary>
        /// 各画素にトーンカーブを適用した画像を取得する。
        /// </summary>
        /// <param name="curve">トーンカーブ</param>
        /// <returns>トーンカーブ適用後の画像</returns>
        public GlayImage Apply(ToneCurve curve)
        {
            var res = new GlayImage(size);
            for (var y = 0; y < size.Height; ++y)
            {
                for (var x = 0; x < size.Width; ++x)
                {
                    res[x, y] = curve[this[x, y]];
                }
            }
            return res;
        }

        private Size size = new Size(0, 0);
        private List<List<int>> image = new List<List<int>>();

        enum State
        {
            Header,
            Size,
            MaxValue,
            ContentP2,
            ContentP5,
            Done,
        }

        private static string readLine(BinaryReader reader)
        {
            var builder = new StringBuilder();
            var b = (byte)0;
            while (b != 0x0A)
            {
                b = reader.ReadByte();
                builder.Append((char)b);
            }
            var res = builder.ToString();
            if (res.EndsWith("\r\n"))
            {
                res = res.Substring(0, res.Length - 2);
            }
            else
            {
                res = res.Substring(0, res.Length - 1);
            }
            return res;
        }

        private static void writeLine(BinaryWriter writer, string str)
        {
            var sjis = Encoding.GetEncoding("Shift_JIS");
            var nl = Environment.NewLine;
            writer.Write(sjis.GetBytes(str + nl));
        }
    }
}
