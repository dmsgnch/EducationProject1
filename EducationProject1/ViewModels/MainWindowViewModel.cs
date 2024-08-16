using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows.Shapes;
using EducationProject1.Commands;
using EducationProject1.Models.Abstract;
using EducationProject1.Models.SecondaryModels;

namespace EducationProject1.ViewModels;

public class MainWindowViewModel : INotifyPropertyChanged
{
    public ObservableCollection<MovingFigureBase> Figures { get; set; } = new();
    
    internal Language[] Languages { get; private set; } = new[]
    {
        new Language("English", "en-US"),
        new Language("Українська", "uk-UA"),
    };
    
    public RelayCommand ToggleFigureMovementCommand { get; }
    
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
    
    private MovingFigureBase? _selectedFigure;
    public MovingFigureBase? SelectedFigure
    {
        get => _selectedFigure;
        set
        {
            if (_selectedFigure?.Figure is not null) RemoveHighlightFigure(_selectedFigure.Figure);

            _selectedFigure = value;
            
            if (_selectedFigure?.Figure is not null) HighlightFigure(_selectedFigure.Figure);
            
            OnPropertyChanged();
            ToggleFigureMovementCommand.RaiseCanExecuteChanged();
        }
    }
    
    public string ButtonsGroupHeader => Localization.Resources.Resources.ButtonsGroupHeader;
    private void RaiseButtonsGroupHeaderChanged() => OnPropertyChanged(nameof(ButtonsGroupHeader));
    
    public string StopFigureButtonText
    {
        get
        {
            if (SelectedFigure is null || !SelectedFigure.SpeedVector.IsStopped)
            {
                return Localization.Resources.Resources.StopFigureButtonText;
            }
            else if (SelectedFigure is not null && SelectedFigure.SpeedVector.IsStopped)
            {
                return Localization.Resources.Resources.MoveFigureButtonText;
            }
            else
            {
                throw new InvalidOperationException("Unacceptable behavior");
            }
        }
    }
    public void RaiseStopFigureButtonTextChanged() => OnPropertyChanged(nameof(StopFigureButtonText));

    public MainWindowViewModel()
    {
        ToggleFigureMovementCommand = new RelayCommand((param) => ToggleFigureMovement(), CanToggleFigureMovementExecute);
    }

    public void ToggleFigureMovement()
    {
        SelectedFigure.SpeedVector.IsStopped = !SelectedFigure.SpeedVector.IsStopped;

        RaiseStopFigureButtonTextChanged();
    }

    private bool CanToggleFigureMovementExecute() => SelectedFigure is not null;
    
    #region Resources refreshing

    public void RefreshResources()
    {
        RaiseButtonsGroupHeaderChanged();
        RaiseStopFigureButtonTextChanged();

        UpdateFiguresName();
    }

    private void UpdateFiguresName()
    {
        foreach (var figure in Figures)
        {
            figure.SetResourceName();
        }
    }
    
    #endregion
    
    #region Figure highlighting

    private void HighlightFigure(Shape shape)
    {
        shape.StrokeThickness = 2;
    }
    
    private void RemoveHighlightFigure(Shape shape)
    {
        shape.StrokeThickness = 0;
    }
    
    #endregion
    
    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}