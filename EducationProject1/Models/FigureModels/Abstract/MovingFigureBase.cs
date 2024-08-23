using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using EducationProject1.Components.Events.CollisionEvents;
using EducationProject1.Components.Exceptions;
using EducationProject1.Components.Extensions.EventArgsExtensions;
using EducationProject1.Components.Helpers;
using EducationProject1.Models.SecondaryModels;

namespace EducationProject1.Models.FigureModels.Abstract;

public abstract class MovingFigureBase : INotifyPropertyChanged
{
    public event EventHandler<NewCollisionEventArgs> NewCollision;

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

    public Position Position { get; set; }
    public SpeedVector SpeedVector { get; set; }
    public FigureSize Size { get; set; }
    protected SolidColorBrush? FillBrush { get; set; }
    public Shape? Figure { get; set; }

    public MovingFigureBase()
    {
        SpeedVector = GetRandomSpeedVector();
        Size = GetRandomSize();
    }

    public MovingFigureBase(SpeedVector? speedVector = null, FigureSize? figureSize = null)
    {
        SpeedVector = speedVector ?? GetRandomSpeedVector();
        Size = figureSize ?? GetRandomSize();
    }

    private FigureSize GetRandomSize()
    {
        return new FigureSize(RandomHelper.GetSizeVectorRandomNumbers());
    }

    private SpeedVector GetRandomSpeedVector()
    {
        return new SpeedVector(RandomHelper.GetMoveVectorRandomNumbers());
    }

    public virtual void Move(Canvas myCanvas)
    {
        if (Figure is null) return;

        Position.X += SpeedVector.dX;
        Position.Y += SpeedVector.dY;

        if (Position.X <= 0 || Position.X >= myCanvas.ActualWidth - Size.Width)
        {
            CheckOutOfPanel(Position.X, SpeedVector.dX, myCanvas.ActualWidth - Size.Width);
            SpeedVector.dX = -SpeedVector.dX;
        }

        if (Position.Y <= 0 || Position.Y >= myCanvas.ActualHeight - Size.Height)
        {
            CheckOutOfPanel(Position.Y, SpeedVector.dY, myCanvas.ActualHeight - Size.Height);
            SpeedVector.dY = -SpeedVector.dY;
        }

        Canvas.SetLeft(Figure, Position.X);
        Canvas.SetTop(Figure, Position.Y);
    }

    private void CheckOutOfPanel(double position, double speed, double maxPoint)
    {
        if (IsFigureOutOfPanel(position, speed, maxPoint))
        {
            throw new Exception<FigureOutOfPanelException>(
                new FigureOutOfPanelException(FigureName, Position.ToString()),
                "The figure appeared outside the panel");
        }
    }

    public abstract void Draw(Panel panel);
    public abstract void SetResourceName();

    protected Position GetRandomPanelPosition(Panel panel)
    {
        return new Position(
            RandomHelper.GetNaturalRandomNumberInDiapason(
                1,
                Convert.ToUInt32(panel.ActualWidth - Size.Width)),
            RandomHelper.GetNaturalRandomNumberInDiapason(
                1,
                Convert.ToUInt32(panel.ActualHeight - Size.Height)));
    }
    
    #region Collision events

    protected virtual void OnNewCollision(NewCollisionEventArgs e)
    {
        e.Raise(this, ref NewCollision);
    }

    public void SimulateNewCollision(String from, String to, String subject, String point)
    {
        NewCollisionEventArgs e = new NewCollisionEventArgs(from, to, subject, point);
        OnNewCollision(e);
    }
    
    #endregion
    
    #region Figure physics

    public Rect GetBoundingRect()
    {
        return new Rect(Position.X, Position.Y, Size.Width, Size.Height);
    }

    private bool IsFigureOutOfPanel(double position, double speed, double maxPoint)
    {
        position += -speed;

        return position > maxPoint;
    }
    
    public void ReturnFigureToThePanel(Panel panel)
    {
        if (IsFigureOutOfPanel(Position.X, SpeedVector.dX, panel.ActualWidth - Size.Width))
        {
            Position.X = panel.ActualWidth - Size.Width;
            Canvas.SetLeft(Figure, Position.X);
        }
        
        if (IsFigureOutOfPanel(Position.Y, SpeedVector.dY, panel.ActualHeight - Size.Height))
        {
            Position.Y = panel.ActualHeight - Size.Height;
            Canvas.SetTop(Figure, Position.Y);
        }
    }
    
    #endregion

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}