# Chinh's c# functions

A collection of utility functions I've shared on my [blog](https://www.chinhdo.com).

# Detecting Blank Images

See my blog post [Detecting Blank Images with c#](https://www.chinhdo.com/20080910/detect-blank-images/)

```csharp
ImageProcessor ip = new ImageProcessor();
Console.WriteLine(ip.IsBlank("myImage.jpg"));
```