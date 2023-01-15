using SkiaSharp;

namespace TextImageGenerator
{
    public static class TextImageGenerator
    {
        public static void GenerateFile(string outputFilePath, TextImageContent content)
        {
            if(string.IsNullOrWhiteSpace(outputFilePath))
            {
                throw new ArgumentException("outputFilePath is NullOrWhiteSpace");
            }

            // Create Bitmap
            using var bitmap = new SKBitmap(content.ImageWidth, content.ImageHeight, ConvertColorType(content.ColorType), ConvertAlphaType(content.AlphaType));
            DrawContent(bitmap, content);

            // Save
            {
                // Create Directory
                var parentDir = System.IO.Path.GetDirectoryName(outputFilePath);
                if (!string.IsNullOrEmpty(parentDir) && System.IO.Directory.Exists(parentDir))
                {
                    System.IO.Directory.CreateDirectory(parentDir);
                }
                // Delete AlreadyFile
                if (System.IO.File.Exists(outputFilePath))
                {
                    System.IO.File.Delete(outputFilePath);
                }
                try
                {
                    // Output ImageFile
                    using var fileStream = new FileStream(outputFilePath, FileMode.OpenOrCreate);
                    using var skStream = new SKManagedWStream(fileStream);
                    if (!bitmap.Encode(skStream, ConvertEncodedImageFormat(content.EncodeFormat, outputFilePath), content.EncodeQuality))
                    {
                        throw new Exception("Bitmap Encode Error.");
                    }
                }
                catch
                {
                    // Delete ErrorFile
                    if (System.IO.File.Exists(outputFilePath))
                    {
                        System.IO.File.Delete(outputFilePath);
                    }
                    throw;
                }
            }
        }

        public static byte[] GenerateBytes(TextImageContent content)
        {
            // Create
            using var bitmap = new SKBitmap(content.ImageWidth, content.ImageHeight, ConvertColorType(content.ColorType), ConvertAlphaType(content.AlphaType));
            DrawContent(bitmap, content);

            // Copy
            {
                using var memoryStream = new MemoryStream();
                using var skStream = new SKManagedWStream(memoryStream);
                if (!bitmap.Encode(skStream, ConvertEncodedImageFormat(content.EncodeFormat, string.Empty), content.EncodeQuality))
                {
                    throw new Exception("Bitmap Encode Error.");
                }
                return memoryStream.ToArray();
            }
        }

        private static void DrawContent(SKBitmap bitmap, TextImageContent content)
        {
            using var canvas = new SKCanvas(bitmap);

            // Background
            var backgroundColor = new SKColor(content.BackgroundColor.Red, content.BackgroundColor.Green, content.BackgroundColor.Blue, content.BackgroundColor.Alpha);
            canvas.Clear(backgroundColor);

            var autoLineHeightCount = content.Lines.Where(x => x.LineHeight == TextImageLineText.AutoSize).Count();
            var totalLineHeightSpecified = content.Lines.Where(x => 0 <= x.LineHeight).Select(x => x.LineHeight).Sum();
            var autoLineHeightSize = (autoLineHeightCount == 0 || content.ImageHeight <= totalLineHeightSpecified) ? 0.0f : ((float)content.ImageHeight - (float)totalLineHeightSpecified) / (float)autoLineHeightCount;

            var midX = (float)content.ImageWidth / 2.0f;
            var topY = 0.0f;
            foreach (var lineContent in content.Lines)
            {
                var lineWidth = lineContent.LineWidth == TextImageLineText.AutoSize ? content.ImageWidth : lineContent.LineWidth;
                var lineHeight = lineContent.LineHeight == TextImageLineText.AutoSize ? autoLineHeightSize : lineContent.LineHeight;
                var lineMidY = topY + lineHeight / 2.0f;
                topY += lineHeight;

                var fontType = string.IsNullOrEmpty(lineContent.FontFamily) ? SKTypeface.CreateDefault() : SKTypeface.FromFamilyName(lineContent.FontFamily);
                var textColor = new SKColor(lineContent.TextColor.Red, lineContent.TextColor.Green, lineContent.TextColor.Blue, lineContent.TextColor.Alpha);
                var textPaint = new SKPaint { Typeface = fontType, IsAntialias = true, Style = SKPaintStyle.Fill, Color = textColor };
                var outlineColor = new SKColor(lineContent.OutlineColor.Red, lineContent.OutlineColor.Green, lineContent.OutlineColor.Blue, lineContent.OutlineColor.Alpha);
                var outlinePaint = new SKPaint { Typeface = fontType, IsAntialias = true, Style = SKPaintStyle.Stroke, StrokeWidth = lineContent.OutlineSize, Color = outlineColor };

                var calcTextSize = CalcTextSize(lineWidth, lineHeight, lineContent.LineText, outlinePaint) * lineContent.TextSizeScale;
                textPaint.TextSize = calcTextSize;
                outlinePaint.TextSize = calcTextSize;

                var previewTextBounds = new SKRect();
                outlinePaint.MeasureText(lineContent.LineText, ref previewTextBounds);

                canvas.DrawText(lineContent.LineText, midX - previewTextBounds.MidX, lineMidY - previewTextBounds.MidY, outlinePaint);
                canvas.DrawText(lineContent.LineText, midX - previewTextBounds.MidX, lineMidY - previewTextBounds.MidY, textPaint);
            }
        }

        private static float CalcTextSize(float width, float height, string text, SKPaint basePaint)
        {
            float InternalCalcTextSize(float width, float height, float textSize, ref SKRect previewTextBounds)
            {

                var calcWidth = (width / previewTextBounds.Width);
                var calcHeight = (height / previewTextBounds.Height);
                var calcTextSize = calcWidth < calcHeight ? textSize * calcWidth : textSize * calcHeight;
                return calcTextSize;
            }
            float InternalAdjustTextSize(float width, float height, ref string text, ref SKPaint paint, ref SKRect previewTextBounds)
            {
                paint.MeasureText(text, ref previewTextBounds);
                if (previewTextBounds.Width <= width && previewTextBounds.Height <= height)
                {
                    return paint.TextSize;
                }
                paint.TextSize = InternalCalcTextSize(width, height, paint.TextSize, ref previewTextBounds);
                return InternalAdjustTextSize(width, height, ref text, ref paint, ref previewTextBounds);
            }


            var initTextSize = 128.0f;
            var previewPaint = basePaint.Clone();
            var previewTextBounds = new SKRect();
            previewPaint.TextSize = initTextSize;
            previewPaint.MeasureText(text, ref previewTextBounds);
            previewPaint.TextSize = InternalCalcTextSize(width, height, previewPaint.TextSize, ref previewTextBounds);
            return InternalAdjustTextSize(width, height, ref text, ref previewPaint, ref previewTextBounds);
        }

        private static SKEncodedImageFormat ConvertEncodedImageFormat(string typeName, string filePath)
        {
            // By Name
            if (typeName.Equals("Bmp", StringComparison.OrdinalIgnoreCase))
            {
                return SKEncodedImageFormat.Bmp;
            }
            else if (typeName.Equals("Gif", StringComparison.OrdinalIgnoreCase))
            {
                return SKEncodedImageFormat.Gif;
            }
            else if (typeName.Equals("Ico", StringComparison.OrdinalIgnoreCase))
            {
                return SKEncodedImageFormat.Ico;
            }
            else if (typeName.Equals("Jpeg", StringComparison.OrdinalIgnoreCase))
            {
                return SKEncodedImageFormat.Jpeg;
            }
            else if (typeName.Equals("Png", StringComparison.OrdinalIgnoreCase))
            {
                return SKEncodedImageFormat.Png;
            }
            else if (typeName.Equals("Wbmp", StringComparison.OrdinalIgnoreCase))
            {
                return SKEncodedImageFormat.Wbmp;
            }
            else if (typeName.Equals("Pkm", StringComparison.OrdinalIgnoreCase))
            {
                return SKEncodedImageFormat.Pkm;
            }
            else if (typeName.Equals("Ktx", StringComparison.OrdinalIgnoreCase))
            {
                return SKEncodedImageFormat.Ktx;
            }
            else if (typeName.Equals("Astc", StringComparison.OrdinalIgnoreCase))
            {
                return SKEncodedImageFormat.Astc;
            }
            else if (typeName.Equals("Dng", StringComparison.OrdinalIgnoreCase))
            {
                return SKEncodedImageFormat.Dng;
            }
            else if (typeName.Equals("Heif", StringComparison.OrdinalIgnoreCase))
            {
                return SKEncodedImageFormat.Heif;
            }
            else if (typeName.Equals("Avif", StringComparison.OrdinalIgnoreCase))
            {
                return SKEncodedImageFormat.Avif;
            }

            // By Path
            var extension = System.IO.Path.GetExtension(filePath);
            if (extension.Equals(".bmp", StringComparison.OrdinalIgnoreCase))
            {
                return SKEncodedImageFormat.Bmp;
            }
            else if (extension.Equals(".gif", StringComparison.OrdinalIgnoreCase))
            {
                return SKEncodedImageFormat.Gif;
            }
            else if (extension.Equals(".ico", StringComparison.OrdinalIgnoreCase))
            {
                return SKEncodedImageFormat.Ico;
            }
            else if (extension.Equals(".jpg", StringComparison.OrdinalIgnoreCase) || extension.Equals(".jpeg", StringComparison.OrdinalIgnoreCase))
            {
                return SKEncodedImageFormat.Jpeg;
            }
            else if (extension.Equals(".png", StringComparison.OrdinalIgnoreCase))
            {
                return SKEncodedImageFormat.Png;
            }
            else if (extension.Equals(".wbmp", StringComparison.OrdinalIgnoreCase))
            {
                return SKEncodedImageFormat.Wbmp;
            }
            else if (extension.Equals(".pkm", StringComparison.OrdinalIgnoreCase))
            {
                return SKEncodedImageFormat.Pkm;
            }
            else if (extension.Equals(".ktx", StringComparison.OrdinalIgnoreCase))
            {
                return SKEncodedImageFormat.Ktx;
            }
            else if (extension.Equals(".astc", StringComparison.OrdinalIgnoreCase))
            {
                return SKEncodedImageFormat.Astc;
            }
            else if (extension.Equals(".dng", StringComparison.OrdinalIgnoreCase))
            {
                return SKEncodedImageFormat.Dng;
            }
            else if (extension.Equals(".heif", StringComparison.OrdinalIgnoreCase))
            {
                return SKEncodedImageFormat.Heif;
            }
            else if (extension.Equals(".avif", StringComparison.OrdinalIgnoreCase))
            {
                return SKEncodedImageFormat.Avif;
            }

            // Default
            return SKEncodedImageFormat.Bmp;
        }

        private static SKColorType ConvertColorType(string typeName)
        {
            if (typeName.Equals("Unknown", StringComparison.OrdinalIgnoreCase))
            {
                return SKColorType.Unknown;
            }
            else if (typeName.Equals("Alpha8", StringComparison.OrdinalIgnoreCase))
            {
                return SKColorType.Alpha8;
            }
            else if (typeName.Equals("Rgb565", StringComparison.OrdinalIgnoreCase))
            {
                return SKColorType.Rgb565;
            }
            else if (typeName.Equals("Argb4444", StringComparison.OrdinalIgnoreCase))
            {
                return SKColorType.Argb4444;
            }
            else if (typeName.Equals("Rgba8888", StringComparison.OrdinalIgnoreCase))
            {
                return SKColorType.Rgba8888;
            }
            else if (typeName.Equals("Rgb888x", StringComparison.OrdinalIgnoreCase))
            {
                return SKColorType.Rgb888x;
            }
            else if (typeName.Equals("Rgba1010102", StringComparison.OrdinalIgnoreCase))
            {
                return SKColorType.Rgba1010102;
            }
            else if (typeName.Equals("Bgra8888", StringComparison.OrdinalIgnoreCase))
            {
                return SKColorType.Bgra8888;
            }
            else if (typeName.Equals("Rgb101010x", StringComparison.OrdinalIgnoreCase))
            {
                return SKColorType.Rgb101010x;
            }
            else if (typeName.Equals("Gray8", StringComparison.OrdinalIgnoreCase))
            {
                return SKColorType.Gray8;
            }
            else if (typeName.Equals("RgbaF16", StringComparison.OrdinalIgnoreCase))
            {
                return SKColorType.RgbaF16;
            }
            else if (typeName.Equals("RgbaF16Clamped", StringComparison.OrdinalIgnoreCase))
            {
                return SKColorType.RgbaF16Clamped;
            }
            else if (typeName.Equals("RgbaF32", StringComparison.OrdinalIgnoreCase))
            {
                return SKColorType.RgbaF32;
            }
            else if (typeName.Equals("Rg88", StringComparison.OrdinalIgnoreCase))
            {
                return SKColorType.Rg88;
            }
            else if (typeName.Equals("AlphaF16", StringComparison.OrdinalIgnoreCase))
            {
                return SKColorType.AlphaF16;
            }
            else if (typeName.Equals("RgF16", StringComparison.OrdinalIgnoreCase))
            {
                return SKColorType.RgF16;
            }
            else if (typeName.Equals("Alpha16", StringComparison.OrdinalIgnoreCase))
            {
                return SKColorType.Alpha16;
            }
            else if (typeName.Equals("Rg1616", StringComparison.OrdinalIgnoreCase))
            {
                return SKColorType.Rg1616;
            }
            else if (typeName.Equals("Rgba16161616", StringComparison.OrdinalIgnoreCase))
            {
                return SKColorType.Rgba16161616;
            }
            else if (typeName.Equals("Bgra1010102", StringComparison.OrdinalIgnoreCase))
            {
                return SKColorType.Bgra1010102;
            }
            else if (typeName.Equals("Bgr101010x", StringComparison.OrdinalIgnoreCase))
            {
                return SKColorType.Bgr101010x;
            }
            return SKImageInfo.PlatformColorType;
        }

        private static SKAlphaType ConvertAlphaType(string typeName)
        {
            if (typeName.Equals("Unknown", StringComparison.OrdinalIgnoreCase))
            {
                return SKAlphaType.Unknown;
            }
            else if (typeName.Equals("Opaque", StringComparison.OrdinalIgnoreCase))
            {
                return SKAlphaType.Opaque;
            }
            else if (typeName.Equals("Premul", StringComparison.OrdinalIgnoreCase))
            {
                return SKAlphaType.Premul;
            }
            else if (typeName.Equals("Unpremul", StringComparison.OrdinalIgnoreCase))
            {
                return SKAlphaType.Unpremul;
            }
            return SKAlphaType.Premul;
        }
    }
}
