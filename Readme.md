# Chinh's c# functions

A collection of utility functions I've shared on my [blog](https://www.chinhdo.com).

## Detecting Blank Images

Code is in [ImageProcessor.cs](https://github.com/chinhdo/dotnet-functions/blob/master/ChinhDo.Functions/ImageProcessor.cs)

See my blog post [Detecting Blank Images with c#](https://www.chinhdo.com/20080910/detect-blank-images/)

```csharp
ImageProcessor ip = new ImageProcessor();
Console.WriteLine(ip.IsBlank("myImage.jpg"));
```

## Converting a generic List of objects to a DataTable

Code is in [DataUtils.cs](https://github.com/chinhdo/dotnet-functions/blob/master/ChinhDo.Functions/DataUtils.cs)

See my blog post [Convert List<T>/IEnumerable to DataTable/DataView](https://www.chinhdo.com/20090402/convert-list-to-datatable/)

```
List<Person> persons = new List<Person>();
...
DataTable table = DataUtils.ToDataTable(persons);
```

## Break a list into chunks

See [Splitting a Generic List<T> into Multiple Chunks](https://www.chinhdo.com/20080515/chunking/)
  
Code is in [DataUtils.cs](https://github.com/chinhdo/dotnet-functions/blob/master/ChinhDo.Functions/DataUtils.cs)
