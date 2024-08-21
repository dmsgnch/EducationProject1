using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
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

    public override void Draw(Panel panel)
    {
        Figure = new Ellipse
        {
            Width = Size.Width,
            Height = Size.Height,
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
        return Localization.Resources.Resources.CircleName;
    }

    public override void SetResourceName()
    {
        FigureName = GetNameFromResources();
    }
}