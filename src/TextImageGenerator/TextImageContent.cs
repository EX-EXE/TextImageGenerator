using System;
using System.Collections.Generic;
using System.Text;

namespace TextImageGenerator
{
    public class TextImageContent
    {
        public int ImageWidth { get; set; } = 512;
        public int ImageHeight { get; set; } = 512;
        public TextImageLineText[] Lines { get; set; } = Array.Empty<TextImageLineText>();

        public string ColorType { get; set; } = "";
        public string AlphaType { get; set; } = "";

        public string EncodeFormat { get; set; } = "";
        public int EncodeQuality { get; set; } = 100;

        public TextImageColor BackgroundColor { get; set; } = new TextImageColor() { Alpha = 255, Red = 255, Green = 255, Blue = 255 };
    }
}
