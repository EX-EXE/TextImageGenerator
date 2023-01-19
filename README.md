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
|LineWidth<br>LineWidthArray||-LineWidth:128<br>-LineWidthArray:128,256,128|
|LineHeight<br>LineHeightArray||-LineHeight:128<br>-LineHeightArray:128,256,128|
|TextSize<br>TextSizeArray|TextSize|-TextSize:20.0|
|OutlineSize<br>OutlineSizeArray|OutlineSize|-OutlineSize:2.5|
|TextScale<br>TextScaleArray|TextScale|-TextScale:0.9|
|Antialias<br>AntialiasArray|Enable Antialias|-Antialias:1<br>-Antialias:false|
|FontFamily<br>FontFamilyArray|FontFamily|-FontFamily:Meiryo|
|FontBold<br>FontBoldArray|Enable Bold|-FontBold:0|
|FontItalic<br>FontItalicArray|Enable Italic|-FontItalic:true|
|TextColor<br>TextColorArray|[RGBA]|-TextColor:FFFFFFFF|
|OutlineColor<br>OutlineColorArray|[RGBA]|-OutlineColor:000000FF|


# Dependent library
[mono/SkiaSharp](https://github.com/mono/SkiaSharp)
