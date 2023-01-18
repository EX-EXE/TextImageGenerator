# TextImageGenerator
Generates an image file with text.(PNG/JPG)

# How To Use
```
TextImageGenerator.exe -Output:./Test.png -Text:TestText -Width:512 -Height:512
```

## Parameter
| Parameter | Description | Example |
|---|---|---|
|Output|Destination for image file.|-Output:./Test.png|
|Width|Image width|-Width:1024|
|Height|Image width|-Height:1024|
|Text|Text to write to the image.[new line is \n]|-Text:ABCDE\nあいうえお\n01234|
|ColorType||-ColorType:Bgra8888|
|AlphaType||-AlphaType:Premul|
|Format|Image Format[png or jpg]|-Format:png|
|Quality|Jpg Quality|-Quality:100|
|BackgroundColor|[RGBA]|-BackgroundColor:00000000|
|LineWidth||-LineWidth:|
|LineHeight||-LineHeight:|
|TextSize||-TextSize:|
|OutlineSize||-OutlineSize:|
|TextScale||-TextScale:|
|Antialias||-Antialias:|
|FontFamily||-FontFamily:Meiryo|
|FontBold||-FontBold:|
|FontItalic||-FontItalic:|
|TextColor|[RGBA]|-TextColor:FFFFFFFF|
|OutlineColor|[RGBA]|-OutlineColor:000000FF|
|LineWidths||-LineWidths:|
|LineHeights||-LineHeights:|
|TextSizes||-TextSizes:|
|OutlineSizes||-OutlineSizes:|
|TextScales||-TextScales:|
|Antialiases||-Antialiases:|
|FontFamilies||-FontFamilies:Meiryo,Meiryo,Meiryo|
|FontBolds||-FontBolds:|
|FontItalics||-FontItalics:|
|TextColors|[RGBA]|-TextColors:FFFFFFFF,FFFFFFFF,FFFFFFFF|
|OutlineColors|[RGBA]|-OutlineColors:000000FF,000000FF,000000FF|


# Dependent library
[mono/SkiaSharp](https://github.com/mono/SkiaSharp)
