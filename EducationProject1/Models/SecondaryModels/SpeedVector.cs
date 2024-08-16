namespace EducationProject1.Models.SecondaryModels;

public class SpeedVector
{
    public bool IsStopped { get; set; } = false;

    private double _dX;
    public double dX
    {
        get => _dX * Convert.ToDouble(!IsStopped);
        set => _dX = value;
    }
    
    private double _dY;
    public double dY
    {
        get => _dY * Convert.ToDouble(!IsStopped);
        set => _dY = value;
    }

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