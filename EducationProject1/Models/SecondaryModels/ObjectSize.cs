namespace EducationProject1.Models.SecondaryModels;

public class ObjectSize
{
    public double Height { get; set; }
    public double Width { get; set; }

    public ObjectSize(double height, double width)
    {
        Height = height;
        Width = width;
    }
    
    public ObjectSize((double height, double width) sizeVector)
    {
        (Height, Width) = sizeVector;
    }
}