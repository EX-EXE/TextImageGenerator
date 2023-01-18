
namespace TextImageGenerator.App
{
    internal class Program
    {
        static readonly string ParamLicenceKey = "-Licence:";

        // Image
        static readonly string ParamOutputKey = "-Output:";

        static readonly string ParamWidthKey = "-Width:";
        static readonly string ParamHeightKey = "-Height:";
        static readonly string ParamTextKey = "-Text:";

        static readonly string ParamColorTypeKey = "-ColorType:";
        static readonly string ParamAlphaTypeKey = "-AlphaType:";

        static readonly string ParamFormatKey = "-Format:";
        static readonly string ParamQualityKey = "-Quality:";

        static readonly string ParamBackgroundColorKey = "-BackgroundColor:";

        // General
        static readonly string ParamLineWidthKey = "-LineWidth:";
        static readonly string ParamLineHeightKey = "-LineHeight:";

        static readonly string ParamTextSizeKey = "-TextSize:";
        static readonly string ParamOutlineSizeKey = "-OutlineSize:";
        static readonly string ParamTextScaleKey = "-TextScale:";

        static readonly string ParamAntialiasKey = "-Antialias:";
        static readonly string ParamFontFamilyKey = "-FontFamily:";
        static readonly string ParamFontBoldKey = "-FontBold:";
        static readonly string ParamFontItalicKey = "-FontItalic:";

        static readonly string ParamTextColorKey = "-TextColor:";
        static readonly string ParamOutlineColorKey = "-OutlineColor:";

        // Array
        static readonly string ParamLineWidthsKey = "-LineWidths:";
        static readonly string ParamLineHeightsKey = "-LineHeights:";

        static readonly string ParamTextSizesKey = "-TextSizes:";
        static readonly string ParamOutlineSizesKey = "-OutlineSizes:";
        static readonly string ParamTextScalesKey = "-TextScales:";

        static readonly string ParamAntialiasesKey = "-Antialiases:";
        static readonly string ParamFontFamiliesKey = "-FontFamilies:";
        static readonly string ParamFontBoldsKey = "-FontBolds:";
        static readonly string ParamFontItalicsKey = "-FontItalics:";

        static readonly string ParamTextColorsKey = "-TextColors:";
        static readonly string ParamOutlineColorsKey = "-OutlineColors:";



        static readonly string[] ParamKeys = new[]
        {
            ParamLicenceKey,

            ParamOutputKey,
            ParamWidthKey,
            ParamHeightKey,
            ParamTextKey,
            ParamColorTypeKey,
            ParamAlphaTypeKey,
            ParamFormatKey,
            ParamQualityKey,
            ParamBackgroundColorKey,

            ParamLineWidthKey,
            ParamLineHeightKey,
            ParamTextSizeKey,
            ParamOutlineSizeKey,
            ParamTextScaleKey,
            ParamAntialiasKey,
            ParamFontFamilyKey,
            ParamFontBoldKey,
            ParamFontItalicKey,
            ParamTextColorKey,
            ParamOutlineColorKey,

            ParamLineWidthsKey,
            ParamLineHeightsKey,
            ParamTextSizesKey,
            ParamOutlineSizesKey,
            ParamTextScalesKey,
            ParamAntialiasesKey,
            ParamFontFamiliesKey,
            ParamFontBoldsKey,
            ParamFontItalicsKey,
            ParamTextColorsKey,
            ParamOutlineColorsKey,
        };

        static int Main()
        {
            var argParser = new ArgumentParser();
            argParser.Init(Environment.CommandLine, ParamKeys);

            var args = ParseArgs();
            if (argParser.ContainsKey(ParamLicenceKey))
            {
                Licence.WriteLine();
            }

            // Param
            var content = new TextImageContent();
            var outputPath = string.Empty;
            {
                // Output
                if (!argParser.TryGetString(ParamOutputKey, out outputPath))
                {
                    Console.WriteLine($"Need parameter. {ParamOutputKey}<FilePath>");
                    return 1;
                }
                // Text
                var text = string.Empty;
                if (!argParser.TryGetString(ParamTextKey, out text))
                {
                    Console.WriteLine($"Need parameter. {ParamTextKey}<Text>");
                    return 1;
                }

                // Width
                if (argParser.TryGetNumber(ParamWidthKey, out int imageWidth))
                {
                    content.ImageWidth = imageWidth;
                }
                else
                {
                    Console.WriteLine($"Need parameter. {ParamWidthKey}<Size>");
                    return 1;
                }
                // Height
                if (argParser.TryGetNumber(ParamHeightKey, out int imageHeight))
                {
                    content.ImageHeight = imageHeight;
                }
                else
                {
                    Console.WriteLine($"Need parameter. {ParamHeightKey}<Size>");
                    return 1;
                }
                // ColorType
                if (argParser.TryGetString(ParamColorTypeKey, out var colorType))
                {
                    content.ColorType = colorType;
                }
                // AlphaType
                if (argParser.TryGetString(ParamAlphaTypeKey, out var alphaType))
                {
                    content.AlphaType = alphaType;
                }
                // Format
                if (argParser.TryGetString(ParamFormatKey, out var format))
                {
                    content.EncodeFormat = format;
                }
                // Quality
                if (argParser.TryGetNumber(ParamQualityKey, out int quality))
                {
                    content.EncodeQuality = quality;
                }
                // BackgroundColor
                if (argParser.TryGetInstance(ParamBackgroundColorKey, out var backgroundColor, TextImageColor.Create))
                {
                    content.BackgroundColor = backgroundColor;
                }

                // Lines
                var (isGeneralLineWidth, valueGeneralLineWidth) = (argParser.TryGetNumber(ParamLineWidthKey, out int generalLineWidth), generalLineWidth);
                var (isGeneralLineHeight, valueGeneralLineHeight) = (argParser.TryGetNumber(ParamLineHeightKey, out int generalLineHeight), generalLineHeight);
                var (isGeneralTextSize, valueGeneralTextSize) = (argParser.TryGetNumber(ParamTextSizeKey, out float generalTextSize), generalTextSize);
                var (isGeneralOutlineSize, valueGeneralOutlineSize) = (argParser.TryGetNumber(ParamOutlineSizeKey, out float generalOutlineSize), generalOutlineSize);
                var (isGeneralTextScale, valueGeneralTextScale) = (argParser.TryGetNumber(ParamTextScaleKey, out float generalTextScale), generalTextScale);
                var (isGeneralAntialias, valueGeneralAntialias) = (argParser.TryGetBool(ParamAntialiasKey, out bool generalAntialias), generalAntialias);
                var (isGeneralFontFamily, valueGeneralFontFamily) = (argParser.TryGetString(ParamFontFamilyKey, out string generalFontFamily), generalFontFamily);
                var (isGeneralFontBold, valueGeneralFontBold) = (argParser.TryGetBool(ParamFontBoldKey, out bool generalFontBold), generalFontBold);
                var (isGeneralFontItalic, valueGeneralFontItalic) = (argParser.TryGetBool(ParamFontItalicKey, out bool generalFontItalic), generalFontItalic);
                var (isGeneralTextColor, valueGeneralTextColor) = (argParser.TryGetInstance(ParamTextColorKey, out TextImageColor generalTextColor, TextImageColor.Create), generalTextColor);
                var (isGeneralOutlineColor, valueGeneralOutlineColor) = (argParser.TryGetInstance(ParamOutlineColorKey, out TextImageColor generalOutlineColor, TextImageColor.Create), generalOutlineColor);

                var (isArrayLineWidth, valueArrayLineWidth) = (argParser.TryGetNumbers(ParamLineWidthsKey, out int[] arrayLineWidth), arrayLineWidth);
                var (isArrayLineHeight, valueArrayLineHeight) = (argParser.TryGetNumbers(ParamLineHeightsKey, out int[] arrayLineHeight), arrayLineHeight);
                var (isArrayTextSize, valueArrayTextSize) = (argParser.TryGetNumbers(ParamTextSizesKey, out float[] arrayTextSize), arrayTextSize);
                var (isArrayOutlineSize, valueArrayOutlineSize) = (argParser.TryGetNumbers(ParamOutlineSizesKey, out float[] arrayOutlineSize), arrayOutlineSize);
                var (isArrayTextScale, valueArrayTextScale) = (argParser.TryGetNumbers(ParamTextScalesKey, out float[] arrayTextScale), arrayTextScale);
                var (isArrayAntialias, valueArrayAntialias) = (argParser.TryGetBools(ParamAntialiasesKey, out bool[] arrayAntialias), arrayAntialias);
                var (isArrayFontFamily, valueArrayFontFamily) = (argParser.TryGetStrings(ParamFontFamiliesKey, out string[] arrayFontFamily), arrayFontFamily);
                var (isArrayFontBold, valueArrayFontBold) = (argParser.TryGetBools(ParamFontBoldsKey, out bool[] arrayFontBold), arrayFontBold);
                var (isArrayFontItalic, valueArrayFontItalic) = (argParser.TryGetBools(ParamFontItalicsKey, out bool[] arrayFontItalic), arrayFontItalic);
                var (isArrayTextColor, valueArrayTextColor) = (argParser.TryGetInstances(ParamTextColorsKey, out TextImageColor[] arrayTextColor, TextImageColor.Create), arrayTextColor);
                var (isArrayOutlineColor, valueArrayOutlineColor) = (argParser.TryGetInstances(ParamOutlineColorsKey, out TextImageColor[] arrayOutlineColor, TextImageColor.Create), arrayOutlineColor);

                T ChoiceParam<T>(int index, T current, bool isArray, T[] valueArray, bool isGeneral, T valueGeneral)
                {
                    if (isArray && index < valueArray.Length)
                    {
                        return valueArray[index];
                    }
                    if (isGeneral)
                    {
                        return valueGeneral;
                    }
                    return current;
                }
                var lineContents = new List<TextImageLineText>();
                foreach (var (index, lineText) in text.Replace("\r\n", "\n").Replace("\\n", "\n").Split('\n').Select((x, i) => (i, x)))
                {
                    var lineContent = new TextImageLineText()
                    {
                        Text = lineText
                    };
                    lineContent.LineWidth = ChoiceParam(index, lineContent.LineWidth, isArrayLineWidth, valueArrayLineWidth, isGeneralLineWidth, valueGeneralLineWidth);
                    lineContent.LineHeight = ChoiceParam(index, lineContent.LineHeight, isArrayLineHeight, valueArrayLineHeight, isGeneralLineHeight, valueGeneralLineHeight);
                    lineContent.TextSize = ChoiceParam(index, lineContent.TextSize, isArrayTextSize, valueArrayTextSize, isGeneralTextSize, valueGeneralTextSize);
                    lineContent.OutlineSize = ChoiceParam(index, lineContent.OutlineSize, isArrayOutlineSize, valueArrayOutlineSize, isGeneralOutlineSize, valueGeneralOutlineSize);
                    lineContent.TextScale = ChoiceParam(index, lineContent.TextScale, isArrayTextScale, valueArrayTextScale, isGeneralTextScale, valueGeneralTextScale);
                    lineContent.Antialias = ChoiceParam(index, lineContent.Antialias, isArrayAntialias, valueArrayAntialias, isGeneralAntialias, valueGeneralAntialias);
                    lineContent.FontFamily = ChoiceParam(index, lineContent.FontFamily, isArrayFontFamily, valueArrayFontFamily, isGeneralFontFamily, valueGeneralFontFamily);
                    lineContent.FontBold = ChoiceParam(index, lineContent.FontBold, isArrayFontBold, valueArrayFontBold, isGeneralFontBold, valueGeneralFontBold);
                    lineContent.FontItalic = ChoiceParam(index, lineContent.FontItalic, isArrayFontItalic, valueArrayFontItalic, isGeneralFontItalic, valueGeneralFontItalic);
                    lineContent.TextColor = ChoiceParam(index, lineContent.TextColor, isArrayTextColor, valueArrayTextColor, isGeneralTextColor, valueGeneralTextColor);
                    lineContent.OutlineColor = ChoiceParam(index, lineContent.OutlineColor, isArrayOutlineColor, valueArrayOutlineColor, isGeneralOutlineColor, valueGeneralOutlineColor);
                    lineContents.Add(lineContent);
                }
                content.Lines = lineContents.ToArray();
            }
            // Generate
            TextImageGenerator.GenerateFile(outputPath, content);
            return 0;
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