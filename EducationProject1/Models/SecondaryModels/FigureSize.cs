using MessagePack;

namespace EducationProject1.Models.SecondaryModels;

[MessagePackObject]
public class FigureSize
{
    [Key(0)]
    public double Height { get; set; }
    [Key(1)]
    public double Width { get; set; }

    public FigureSize()
    { }

    public FigureSize(double height, double width)
    {
        Height = height;
        Width = width;
    }
    
    public FigureSize((double height, double width) sizeVector)
    {
        (Height, Width) = sizeVector;
    }
}