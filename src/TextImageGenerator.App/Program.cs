using static System.Net.Mime.MediaTypeNames;

namespace TextImageGenerator.App
{
    internal class Program
    {
        static readonly string ParamLicenceKey = "-Licence:";

        static readonly string ParamOutputKey = "-Output:";

        static readonly string ParamWidthKey = "-Width:";
        static readonly string ParamHeightKey = "-Height:";
        static readonly string ParamTextKey = "-Text:";

        static readonly string ParamColorTypeKey = "-ColorType:";
        static readonly string ParamAlphaTypeKey = "-AlphaType:";

        static readonly string ParamFormatKey = "-Format:";
        static readonly string ParamQualityKey = "-Quality:";

        static readonly string ParamBackgroundColorKey = "-BackgroundColor:";
        static readonly string ParamTextColorKey = "-TextColor:";
        static readonly string ParamOutlineColorKey = "-OutlineColor:";

        static readonly string ParamLineWidthsKey = "-LineeWidths:";
        static readonly string ParamLineHeightsKey = "-LineHeights:";
        static readonly string ParamFontKey = "-Font:";
        static readonly string ParamTextScaleKey = "-TextScale:";
        static readonly string ParamOutlineSizeKey = "-OutlineSize:";

        static readonly string[] ParamKeys = new[]
        {
            ParamLicenceKey          ,
            ParamOutputKey           ,
            ParamWidthKey            ,
            ParamHeightKey           ,
            ParamTextKey             ,
            ParamColorTypeKey        ,
            ParamAlphaTypeKey        ,
            ParamFormatKey           ,
            ParamQualityKey          ,
            ParamBackgroundColorKey  ,
            ParamTextColorKey        ,
            ParamOutlineColorKey     ,
            ParamLineWidthsKey       ,
            ParamLineHeightsKey      ,
            ParamFontKey             ,
            ParamTextScaleKey        ,
            ParamOutlineSizeKey      ,
        };

        static void Main()
        {
            var args = ParseArgs();
            if (args.ContainsKey(ParamLicenceKey))
            {
                Licence.WriteLine();
            }

            // Generate
            var content = new TextImageContent();
            var outputPath = string.Empty;
            if (args.ContainsKey(ParamOutputKey))
            {
                outputPath = args[ParamOutputKey];
            }
            if (args.ContainsKey(ParamWidthKey) && int.TryParse(args[ParamWidthKey], out var width))
            {
                content.ImageWidth = width;
            }
            if (args.ContainsKey(ParamHeightKey) && int.TryParse(args[ParamHeightKey], out var height))
            {
                content.ImageWidth = height;
            }
            if (args.ContainsKey(ParamTextKey))
            {
                var font = string.Empty;
                var scale = 1.0f;
                var outlineSize = 1.0f;
                var textColor = new TextImageColor();
                var outlineColor = new TextImageColor();
                var lineHeights = new[] { TextImageLineText.AutoSize };
                var lineWidths = new[] { TextImageLineText.AutoSize };
                if (args.ContainsKey(ParamLineHeightsKey))
                {
                    var heights = args[ParamLineHeightsKey].Split(",").Select(x => int.Parse(x)).ToArray();
                    if (0 < heights.Length)
                    {
                        lineHeights = heights;
                    }
                }
                if (args.ContainsKey(ParamLineWidthsKey))
                {
                    var widths = args[ParamLineWidthsKey].Split(",").Select(x => int.Parse(x)).ToArray();
                    if (0 < widths.Length)
                    {
                        lineWidths = widths;
                    }
                }
                if (args.ContainsKey(ParamFontKey))
                {
                    font = args[ParamFontKey];
                }
                if (args.ContainsKey(ParamTextScaleKey) && float.TryParse(args[ParamTextScaleKey], out var scaleValue))
                {
                    scale = scaleValue;
                }
                if (args.ContainsKey(ParamOutlineSizeKey) && float.TryParse(args[ParamOutlineSizeKey], out var outlineSizeValue))
                {
                    outlineSize = outlineSizeValue;
                }
                if (args.ContainsKey(ParamTextColorKey))
                {
                    textColor = new TextImageColor(args[ParamTextColorKey]);
                }
                if (args.ContainsKey(ParamOutlineColorKey))
                {
                    outlineColor = new TextImageColor(args[ParamOutlineColorKey]);
                }
                var lines = new List<TextImageLineText>();
                foreach (var (index, lineText) in args[ParamTextKey].Replace("\r\n", "\n").Replace("\\n", "\n").Split('\n').Select((x, i) => (i, x)))
                {
                    var lineContent = new TextImageLineText()
                    {
                        LineText = lineText,
                        TextSizeScale = scale,
                        OutlineSize = outlineSize,
                        FontFamily = font,
                        TextColor = textColor,
                        OutlineColor = outlineColor,
                    };
                    if (index < lineHeights.Length)
                    {
                        lineContent.LineHeight = lineHeights[index];
                    }
                    if (index < lineWidths.Length)
                    {
                        lineContent.LineWidth = lineWidths[index];
                    }
                    lines.Add(lineContent);
                }
                content.Lines = lines.ToArray();
            }
            if (args.ContainsKey(ParamColorTypeKey))
            {
                content.ColorType = args[ParamColorTypeKey];
            }
            if (args.ContainsKey(ParamAlphaTypeKey))
            {
                content.AlphaType = args[ParamAlphaTypeKey];
            }
            if (args.ContainsKey(ParamFormatKey))
            {
                content.EncodeFormat = args[ParamFormatKey];
            }
            if (args.ContainsKey(ParamQualityKey) && int.TryParse(args[ParamQualityKey], out int quality))
            {
                content.EncodeQuality = quality;
            }
            if (args.ContainsKey(ParamBackgroundColorKey))
            {
                content.BackgroundColor = new TextImageColor(args[ParamBackgroundColorKey]);
            }
            TextImageGenerator.GenerateFile(outputPath, content);
        }

        static Dictionary<string, string> ParseArgs()
        {
            var cmd = Environment.CommandLine;
            var keyIndexes = new List<(string key, int index)>();
            foreach (var key in ParamKeys)
            {
                var keyIndex = cmd.IndexOf(key, StringComparison.OrdinalIgnoreCase);
                if (0 <= keyIndex)
                {
                    keyIndexes.Add((key, keyIndex));
                }
            }
            var sortKeyIndexes = keyIndexes.OrderBy(x => x.index).ToArray();
            var result = new Dictionary<string, string>();
            foreach (var (index, keyIndex) in sortKeyIndexes.Select((x, i) => (i, x)))
            {
                var startIndex = keyIndex.index + keyIndex.key.Length;
                var lastIndex = (index != sortKeyIndexes.Length - 1) ? sortKeyIndexes[index + 1].index : cmd.Length;
                result.Add(keyIndex.key, cmd.Substring(startIndex, lastIndex - startIndex).Trim());
            }
            return result;
        }
    }
}