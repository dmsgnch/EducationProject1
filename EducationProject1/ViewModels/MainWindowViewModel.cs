using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Shapes;
using EducationProject1.Models.Abstract;
using EducationProject1.Models.SecondaryModels;

namespace EducationProject1.ViewModels;

public class MainWindowViewModel : INotifyPropertyChanged
{
    public ObservableCollection<FigureBase> Figures { get; set; } = new();
    
    internal Language[] Languages { get; private set; } = new[]
    {
        new Language("English", "en-US"),
        new Language("Українська", "uk-UA"),
    };
    
    private Language _selectedLanguage;
    public Language SelectedLanguage
    {
        get => _selectedLanguage;
        set
        {
            _selectedLanguage = value;
            OnPropertyChanged();
        }
    }
    
    private FigureBase? _selectedFigure;
    public FigureBase? SelectedFigure
    {
        get => _selectedFigure;
        set
        {
            if (_selectedFigure?.Figure is not null) RemoveHighlightFigure(_selectedFigure.Figure);

            _selectedFigure = value;
            
            if (_selectedFigure?.Figure is not null) HighlightFigure(_selectedFigure.Figure);
            
            OnPropertyChanged();
        }
    }
    
    public string ButtonsGroupHeader => Localization.Resources.Resources.ButtonsGroupHeader;
    private void RaiseButtonsGroupHeaderChanged() => OnPropertyChanged(nameof(ButtonsGroupHeader));
    
    public MainWindowViewModel()
    { }

    public void RefreshResources()
    {
        RaiseButtonsGroupHeaderChanged();

        UpdateFiguresName();
    }

    private void UpdateFiguresName()
    {
        foreach (var figure in Figures)
        {
            figure.SetResourceName();
        }
    }

    private void HighlightFigure(Shape shape)
    {
        shape.StrokeThickness = 2;
    }
    
    private void RemoveHighlightFigure(Shape shape)
    {
        shape.StrokeThickness = 0;
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}