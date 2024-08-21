using System.Windows.Controls;
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
    
    public override void Draw(Panel panel)
    {
        Figure = new System.Windows.Shapes.Rectangle()
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
        return Localization.Resources.Resources.RectangleName;
    }

    public override void SetResourceName()
    {
        FigureName = GetNameFromResources();
    }
}