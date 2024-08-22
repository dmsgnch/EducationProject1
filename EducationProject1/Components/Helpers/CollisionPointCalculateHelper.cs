using EducationProject1.Models.FigureModels.Abstract;
using EducationProject1.Models.SecondaryModels;

namespace EducationProject1.Components.Helpers;

public static class CollisionPointCalculateHelper
{
    public static Position GetFiguresCollisionPoint(
        MovingFigureBase firstFigure, 
        MovingFigureBase secondFigure)
    {
        var firstFigurePosition = GetFigureCenterPosition(firstFigure);
        var secondFigurePosition = GetFigureCenterPosition(secondFigure);

        return new Position(
            (firstFigurePosition.X + secondFigurePosition.X) / 2,
            (firstFigurePosition.Y + secondFigurePosition.Y) / 2);
    }

    private static Position GetFigureCenterPosition(MovingFigureBase figure)
    {
        return new Position(
            figure.Position.X + figure.Size.Width,
            figure.Position.Y + figure.Size.Height);
    }
}