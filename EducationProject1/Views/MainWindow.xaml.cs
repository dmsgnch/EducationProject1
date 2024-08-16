using System.Globalization;
using System.Windows;
using System.Windows.Threading;
using EducationProject1.Models;
using EducationProject1.Models.Abstract;
using EducationProject1.Models.SecondaryModels;
using EducationProject1.ViewModels;
using Rectangle = EducationProject1.Models.Rectangle;

namespace EducationProject1.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly MainWindowViewModel _mainWindowViewModel;
    private readonly DispatcherTimer _timer;
    
    public MainWindow()
    {
        InitializeComponent();
        _timer = new DispatcherTimer();
        
        _mainWindowViewModel = new MainWindowViewModel();
        DataContext = _mainWindowViewModel;

        InitializeAndStartTimer();
        SetLanguages();
    }

    private void SetLanguages()
    {
        LanguageComboBox.ItemsSource = _mainWindowViewModel.Languages;
        _mainWindowViewModel.SelectedLanguage = _mainWindowViewModel.Languages[0];
    }

    private void InitializeAndStartTimer()
    {
        _timer.Interval = TimeSpan.FromMilliseconds(16); // Near 60 FPS
        _timer.Tick += OnTimerTick;
        _timer.Start();
    }
    
    private void OnTimerTick(object? sender, EventArgs e)
    {
        var figures = _mainWindowViewModel.Figures;
        
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

    private void OnComboBox_SelectionChanged(object sender, RoutedEventArgs e)
    {
        if(LanguageComboBox.SelectedItem is Language language) 
        {
            CultureInfo culture = new CultureInfo(language.Code);
            CultureInfo.CurrentUICulture = culture;
            CultureInfo.CurrentCulture = culture;

            _mainWindowViewModel.RefreshResources();
        }
    }
}