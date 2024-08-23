using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using EducationProject1.Models.FigureModels.Abstract;
using EducationProject1.Models.SecondaryModels;

namespace EducationProject1.Models.FigureModels;

public class Triangle : MovingFigureBase
{
    public Triangle()
    {
        InitValues();
    }

    public Triangle(SpeedVector? speedVector = null, FigureSize? figureSize = null) : base(speedVector, figureSize)
    {
        InitValues();
    }

    private void InitValues()
    {
        FillBrush = Brushes.Red;
        FigureName = GetNameFromResources();
    }
    
    private string GetNameFromResources()
    {
        return Localization.Resources.Resources.TriangleName;
    }

    public override void SetResourceName()
    {
        FigureName = GetNameFromResources();
    }
    
    protected override void OnRender(DrawingContext dc)
    {
        base.OnRender(dc);

        Point p1 = new Point(Position.X, Position.Y + Size.Height);
        Point p2 = new Point(Position.X + Size.Width / 2, Position.Y);
        Point p3 = new Point(Position.X + Size.Width, Position.Y + Size.Height);
        
        StreamGeometry geometry = new StreamGeometry();
        using (StreamGeometryContext context = geometry.Open())
        {
            context.BeginFigure(p1, true, true);
            context.LineTo(p2, true, true);
            context.LineTo(p3, true, true);
        }
        
        dc.DrawGeometry(FillBrush, BorderPen, geometry);
    }
}