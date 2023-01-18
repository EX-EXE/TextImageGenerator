using System;
using System.Collections.Generic;
using System.Text;

namespace TextImageGenerator
{
    public class TextImageLineText
    {
        public static readonly int AutoSize = -1;
        public int LineWidth { get; set; } = AutoSize;
        public int LineHeight { get; set; } = AutoSize;

        public string Text = string.Empty;
        public float TextSize { get; set; } = AutoSize;
        public float OutlineSize { get; set; } = 0.0f;
        public float TextScale { get; set; } = 1.0f;

        public bool Antialias { get; set; } = true;
        public string FontFamily { get; set; } = string.Empty;
        public bool FontBold { get; set; } = false;
        public bool FontItalic { get; set; } = false;

        public TextImageColor TextColor { get; set; } = new TextImageColor();
        public TextImageColor OutlineColor { get; set; } = new TextImageColor();
    }
}
