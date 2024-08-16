using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using EducationProject1.Helpers;
using EducationProject1.Models.SecondaryModels;

namespace EducationProject1.Models.Abstract;

public abstract class FigureBase : IMovable, INotifyPropertyChanged
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
    public MoveVector MoveVector { get; set; }
    protected ObjectSize Size { get; set; }
    protected SolidColorBrush? FillBrush { get; init; }
    public Shape? Figure { get; set; }

    public FigureBase()
    {
        Size = new ObjectSize(RandomHelper.GetSizeVectorRandomNumbers());
        MoveVector = new MoveVector(RandomHelper.GetMoveVectorRandomNumbers());
    }
    
    public virtual void Move(Canvas myCanvas)
    {
        if (Figure is null) return;
        
        double newX = Canvas.GetLeft(Figure) + MoveVector.dX;
        double newY = Canvas.GetTop(Figure) + MoveVector.dY;
        
        if (newX <= 0 || newX >= myCanvas.ActualWidth - Size.Width)
            MoveVector.dX = -MoveVector.dX;
        if (newY <= 0 || newY >= myCanvas.ActualHeight - Size.Height)
            MoveVector.dY = -MoveVector.dY;
        
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