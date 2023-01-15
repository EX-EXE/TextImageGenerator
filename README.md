# TextImageGenerator
Generates an image file with text.(PNG/JPG)

# How To Use
```
TextImageGenerator.App.exe -Output:./Test.png -Text:TestText -Width:512 -Height:512
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
|TextColor|[RGBA]|-TextColor:FFFFFFFF|
|OutlineColor|[RGBA]|-OutlineColor:000000FF|
|LineHeights||-LineHeights:100,-1,200|
|OutlineSize||-OutlineSize:10.0|
|Font||-Font:Meiryo|
|TextScale||-TextScale:1.0|

# Dependent library
[mono/SkiaSharp](https://github.com/mono/SkiaSharp)
