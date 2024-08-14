using System.Windows;
using System.Windows.Threading;
using EducationProject1.Models;
using EducationProject1.Models.Abstract;
using EducationProject1.ViewModels;
using Rectangle = EducationProject1.Models.Rectangle;

namespace EducationProject1.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private DispatcherTimer _timer;
    
    public MainWindow()
    {
        InitializeComponent();

        DataContext = new MainWindowViewModel();

        InitializeAndStartTimer();
    }

    private void InitializeAndStartTimer()
    {
        _timer = new DispatcherTimer();
        _timer.Interval = TimeSpan.FromMilliseconds(16); // Near 60 FPS
        _timer.Tick += OnTimerTick;
        _timer.Start();
    }
    
    private void OnTimerTick(object? sender, EventArgs e)
    {
        var figures = ((MainWindowViewModel)DataContext).Figures;
        
        foreach (var figure in figures)
        {
            figure.Move(MyCanvas);
        }
    }
    
    private void OnTriangleButton_Click(object sender, RoutedEventArgs e)
    {
        CreateNewFigure(new Triangle());
    }
    
    private void OnRectangleButton_Click(object sender, RoutedEventArgs e)
    {
        CreateNewFigure(new Rectangle());
    }
    
    private void OnCircleButton_Click(object sender, RoutedEventArgs e)
    {
        CreateNewFigure(new Circle());
    }

    private void CreateNewFigure(FigureBase figure)
    {
        figure.Draw(MyCanvas);
        
        ((MainWindowViewModel)DataContext).Figures.Add(figure);
    }
}