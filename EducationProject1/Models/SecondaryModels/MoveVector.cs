namespace EducationProject1.Models.SecondaryModels;

public class MoveVector
{
    public double dX { get; set; }
    public double dY { get; set; }

    public MoveVector(double dX, double dY)
    {
        this.dX = dX;
        this.dY = dY;
    }
    
    public MoveVector((double dX, double dY) vector)
    {
        (dX, dY) = vector;
    }
}