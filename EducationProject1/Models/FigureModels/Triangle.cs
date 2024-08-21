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

    public override void Draw(Panel panel)
    {
        Figure = new Polygon
        {
            Points = new PointCollection
            {
                new Point(0, Size.Height),
                new Point(Size.Width / 2, 0),
                new Point(Size.Width, Size.Height)
            },
            Fill = FillBrush,
            Stroke = Brushes.Black,
            StrokeThickness = 0
        };

        Position ??= GetRandomPanelPosition(panel);
        
        Canvas.SetLeft(Figure, Position.X);
        Canvas.SetTop(Figure, Position.Y);
        
        panel.Children.Add(Figure);
    }
    
    private string GetNameFromResources()
    {
        return Localization.Resources.Resources.TriangleName;
    }

    public override void SetResourceName()
    {
        FigureName = GetNameFromResources();
    }
}