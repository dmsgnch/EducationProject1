using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using EducationProject1.Helpers;
using EducationProject1.Models.Abstract;

namespace EducationProject1.Models;

public class Triangle : FigureBase
{
    public Triangle()
    {
        FillBrush = Brushes.Red;
        FigureName = GetNameFromResources();
    }

    public override void Draw(Canvas canvas)
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
        return Localization.Resources.Resources.TriangleName;
    }

    public override void SetResourceName()
    {
        FigureName = GetNameFromResources();
    }
}