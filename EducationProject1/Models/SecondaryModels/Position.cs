using MessagePack;

namespace EducationProject1.Models.SecondaryModels;

[MessagePackObject]
public class Position
{
    [Key(0)]
    public double X { get; set; }
    [Key(1)]
    public double Y { get; set; }

    public Position()
    { }

    public Position(double x, double y)
    {
        X = x;
        Y = y;
    }

    public Position((double x, double y) posVector)
    {
        (X, Y) = posVector;
    }

    public (double, double) GetPosition() => (X, Y);
}