using System;
using System.Collections.Generic;
using System.Text;

namespace TextImageGenerator
{
    public struct TextImageColor
    {
        public TextImageColor()
        {
        }

        public static TextImageColor Create(string hex)
        {
            return new TextImageColor(hex);
        }

        public TextImageColor(string hex)
        {
            var trimStr = hex.TrimStart('#');
            if (trimStr.Length < 8)
            {
                throw new ArgumentException($"Invalid Format : {hex}");
            }
            var red = trimStr.Substring(0, 2);
            var green = trimStr.Substring(2, 2);
            var blue = trimStr.Substring(4, 2);
            var alpha = trimStr.Substring(6, 2);

            Red = Convert.ToByte(red, 16);
            Green = Convert.ToByte(green, 16);
            Blue = Convert.ToByte(blue, 16);
            Alpha = Convert.ToByte(alpha, 16);
        }

        public byte Red { get; set; } = 0;
        public byte Green { get; set; } = 0;
        public byte Blue { get; set; } = 0;
        public byte Alpha { get; set; } = 255;
    }
}
