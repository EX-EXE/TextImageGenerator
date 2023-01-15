# TextImageGenerator
Generates an image file with text.(PNG/JPG)

# How To Use
```
TextImageGenerator.App.exe -Output:./Test.png -Text:TestText -Width:512 -Height:512
```

## Parameter
| Parameter | Description | Example |
|---|---|---|
|Output||-Output:./Test.png|
|Width||-Width:1024|
|Height||-Height:1024|
|Text||-Text:ABCDE\nあいうえお\n01234|
|ColorType||-ColorType:Bgra8888|
|AlphaType||-AlphaType:Premul|
|Format||-Format:png|
|Quality||-Quality:100|
|BackgroundColor||-BackgroundColor:00000000|
|TextColor||-TextColor:FFFFFFFF|
|OutlineColor||-OutlineColor:000000FF|
|LineHeights||-LineHeights:100,-1,200|
|OutlineSize||-OutlineSize:10.0|
|Font||-Font:Meiryo|
|TextScale||-TextScale:1.0|

# Dependent library
[mono/SkiaSharp](https://github.com/mono/SkiaSharp)
