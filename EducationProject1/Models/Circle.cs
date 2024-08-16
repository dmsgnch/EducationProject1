using System.Globalization;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using EducationProject1.Helpers;
using EducationProject1.Models.Abstract;

namespace EducationProject1.Models;

public class Circle : FigureBase
{
    public Circle()
    {
        FillBrush = Brushes.Blue;
        Size.Height = Size.Width;
        FigureName = GetNameFromResources();
    }

    public override void Draw(Canvas canvas)
    {
        Figure = new Ellipse
        {
            Width = Size.Width,
            Height = Size.Height,
            Fill = FillBrush,
            Stroke = Brushes.Black,
            StrokeThickness = 0
        };
        
        Canvas.SetLeft(Figure, 
            RandomHelper.GetNaturalRandomNumberInDiapason(
                1, Convert.ToUInt32(canvas.ActualWidth - Size.Width)));
        Canvas.SetTop(Figure, 
            RandomHelper.GetNaturalRandomNumberInDiapason(
                1, Convert.ToUInt32(canvas.ActualHeight - Size.Height)));

        canvas.Children.Add(Figure);
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