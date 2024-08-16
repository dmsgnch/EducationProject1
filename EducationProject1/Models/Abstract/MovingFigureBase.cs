using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using EducationProject1.Helpers;
using EducationProject1.Models.SecondaryModels;

namespace EducationProject1.Models.Abstract;

public abstract class MovingFigureBase : IMovable, INotifyPropertyChanged
{
    private string _figureName;
    public string FigureName
    {
        get => _figureName;
        set
        {
            _figureName = value;
            OnPropertyChanged();
        } 
    }
    public SpeedVector SpeedVector { get; set; }
    public ObjectSize Size { get; set; }
    protected SolidColorBrush? FillBrush { get; init; }
    public Shape? Figure { get; set; }

    public MovingFigureBase()
    {
        Size = new ObjectSize(RandomHelper.GetSizeVectorRandomNumbers());
        SpeedVector = new SpeedVector(RandomHelper.GetMoveVectorRandomNumbers());
    }
    
    public virtual void Move(Canvas myCanvas)
    {
        if (Figure is null) return;
        
        double newX = Canvas.GetLeft(Figure) + SpeedVector.dX;
        double newY = Canvas.GetTop(Figure) + SpeedVector.dY;
        
        if (newX <= 0 || newX >= myCanvas.ActualWidth - Size.Width)
            SpeedVector.dX = -SpeedVector.dX;
        if (newY <= 0 || newY >= myCanvas.ActualHeight - Size.Height)
            SpeedVector.dY = -SpeedVector.dY;
        
        Canvas.SetLeft(Figure, newX);
        Canvas.SetTop(Figure, newY);
    }

    public abstract void Draw(Canvas canvas);
    public abstract void SetResourceName();
    
    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}