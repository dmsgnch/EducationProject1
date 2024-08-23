using System.Windows;
using System.Windows.Media;
using EducationProject1.Models.FigureModels.Abstract;
using EducationProject1.Models.SecondaryModels;

namespace EducationProject1.Models.FigureModels;

public class Rectangle : MovingFigureBase
{
    public Rectangle()
    {
        InitValues();
    }

    public Rectangle(SpeedVector? speedVector = null, FigureSize? figureSize = null) : base(speedVector, figureSize)
    {
        InitValues();
    }

    private void InitValues()
    {
        FillBrush = Brushes.Green;
        FigureName = GetNameFromResources();
    }
    
    private string GetNameFromResources()
    {
        return Localization.Resources.Resources.RectangleName;
    }

    public override void SetResourceName()
    {
        FigureName = GetNameFromResources();
    }
    
    protected override void OnRender(DrawingContext dc)
    {
        base.OnRender(dc);
        
        Rect rect = new Rect(Position.X, Position.Y, Size.Width, Size.Height);
        
        dc.DrawRectangle(FillBrush, BorderPen, rect);
    }
}