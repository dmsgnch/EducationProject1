using EducationProject1.Models.FigureModels.Abstract;

namespace EducationProject1.Models.SecondaryModels;

public struct FigurePair
{
    public MovingFigureBase First { get; set; }
    public MovingFigureBase Second { get; set; }

    public FigurePair(MovingFigureBase first, MovingFigureBase second)
    {
        First = first;
        Second = second;
    }

    public FigurePair GetInvertPair()
    {
        return new FigurePair(Second, First);
    }
}