using MessagePack;

namespace EducationProject1.Models.SecondaryModels;

[MessagePackObject]
public class SpeedVector
{
    [Key(0)]
    public bool IsStopped { get; set; } = false;

    private double _dX;
    [Key(1)]
    public double dX
    {
        get => _dX * Convert.ToDouble(!IsStopped);
        set => _dX = value;
    }
    
    private double _dY;
    [Key(2)]
    public double dY
    {
        get => _dY * Convert.ToDouble(!IsStopped);
        set => _dY = value;
    }

    public SpeedVector()
    { }

    public SpeedVector(double dX, double dY)
    {
        this.dX = dX;
        this.dY = dY;
    }
    
    public SpeedVector((double dX, double dY) vector)
    {
        (dX, dY) = vector;
    }
}