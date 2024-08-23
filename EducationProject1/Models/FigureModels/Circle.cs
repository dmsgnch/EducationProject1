using System.Windows;
using System.Windows.Media;
using EducationProject1.Models.FigureModels.Abstract;
using EducationProject1.Models.SecondaryModels;

namespace EducationProject1.Models.FigureModels;

public class Circle : MovingFigureBase
{
    public Circle()
    {
        InitValues();
    }

    public Circle(SpeedVector? speedVector = null, FigureSize? figureSize = null) : base(speedVector, figureSize)
    {
        InitValues();
    }

    private void InitValues()
    {
        FillBrush = Brushes.Blue;
        Size.Height = Size.Width;
        FigureName = GetNameFromResources();
    }

    private string GetNameFromResources()
    {
        return Localization.Resources.Resources.CircleName;
    }

    public override void SetResourceName()
    {
        FigureName = GetNameFromResources();
    }
    
    protected override void OnRender(DrawingContext dc)
    {
        base.OnRender(dc);

        var radius = Size.Width / 2;
        
        dc.DrawEllipse(FillBrush, BorderPen, 
            new Point(Position.X + radius, Position.Y + radius), radius, radius);
    }
}