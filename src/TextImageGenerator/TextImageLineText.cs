using System;
using System.Collections.Generic;
using System.Text;

namespace TextImageGenerator
{
    public class TextImageLineText
    {
        public static readonly int AutoSize = -1;

        public string LineText = string.Empty;
        public int LineWidth { get; set; } = AutoSize;
        public int LineHeight { get; set; } = AutoSize;
        public float TextSizeScale { get; set; } = 1.0f;
        public float OutlineSize { get; set; } = 0.0f;
        public string FontFamily { get; set; } = string.Empty;

        public TextImageColor TextColor { get; set; } = new TextImageColor();
        public TextImageColor OutlineColor { get; set; } = new TextImageColor();
    }
}
