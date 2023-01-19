# TextImageGenerator
Generates an image file with text.(PNG/JPG)

# How To Use
```
TextImageGenerator.exe -Output:./Test.png -Text:TestText -Width:512 -Height:512
```

## Parameter
| Parameter | Required | Example |
|---|---|---|
|Output|✓|-Output:./Test.png|
|Width|✓|-Width:1024|
|Height|✓|-Height:1024|
|Text<br>[new line is \n]|✓|-Text:ABCDE\nあいうえお\n01234|
|ColorType||-ColorType:Bgra8888|
|AlphaType||-AlphaType:Premul|
|Format<br>[png or jpg]||-Format:png|
|Quality||-Quality:100|
|BackgroundColor<br>[RGBA]||-BackgroundColor:00000000|
|LineWidth<br>LineWidthArray||-LineWidth:128<br>-LineWidthArray:128,256,128|
|LineHeight<br>LineHeightArray||-LineHeight:128<br>-LineHeightArray:128,256,128|
|TextSize<br>TextSizeArray||-TextSize:20.0|
|OutlineSize<br>OutlineSizeArray||-OutlineSize:2.5|
|TextScale<br>TextScaleArray||-TextScale:0.9|
|Antialias<br>AntialiasArray||-Antialias:1<br>-Antialias:false|
|FontFamily<br>FontFamilyArray||-FontFamily:Meiryo|
|FontBold<br>FontBoldArray||-FontBold:0|
|FontItalic<br>FontItalicArray||-FontItalic:true|
|TextColor<br>TextColorArray<br>[RGBA]||-TextColor:FFFFFFFF|
|OutlineColor<br>OutlineColorArray<br>[RGBA]||-OutlineColor:000000FF|


# Dependent library
[mono/SkiaSharp](https://github.com/mono/SkiaSharp)
