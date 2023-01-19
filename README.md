# TextImageGenerator
Generates an image file with text.(PNG/JPG)

# How To Use
```
TextImageGenerator.exe -Output:./Test.png -Text:TestText -Width:512 -Height:512
```
<img src="https://user-images.githubusercontent.com/114784289/213452417-87ad6b1f-c580-451c-8800-48d208ee3bcc.png" width="256">

```
TextImageGenerator.exe -Output:./Test2.png -Text:Bold\nItalic\nOutline -FontBoldArray:true,false,false -FontItalicArray:false,true,false -OutlineSizeArray:0,0,5 -TextScale:0.9 -Width:500 -Height:600 -BackgroundColor:222222FF -TextColorArray:AA0000FF,00BB00FF,0000CCFF -OutlineColor:FFFFFFFF
```

<img src="https://user-images.githubusercontent.com/114784289/213452427-b30ca844-6845-481b-97d0-e3c302658f4a.png" width="256">

```
TextImageGenerator.exe -Output:./Test3.png -Width:300 -Height:300 -Text:Yu Gothic\nMeiryo\nMS Gothic -FontFamilyArray:Yu Gothic,Meiryo,MS Gothic -TextScale:0.9 -TextSizeArray:80,40,20
```

<img src="https://user-images.githubusercontent.com/114784289/213477687-c81871a0-2779-44e9-892e-c0ead9056358.png" width="256">

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
