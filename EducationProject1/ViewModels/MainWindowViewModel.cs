using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Media;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using System.Windows.Shapes;
using EducationProject1.Commands;
using EducationProject1.Components.Events.CollisionEvents;
using EducationProject1.Components.Sounds;
using EducationProject1.Localization.Resources;
using EducationProject1.Models.FigureModels.Abstract;
using EducationProject1.Models.SecondaryModels;

namespace EducationProject1.ViewModels;

public class MainWindowViewModel : INotifyPropertyChanged
{
    public RelayCommand ToggleFigureMovementCommand { get; }
    public RelayCommand PlusEventFunctionCommand { get; }
    public RelayCommand MinusEventFunctionCommand { get; }

    public ObservableCollection<MovingFigureBase> Figures { get; set; } = new();

    internal Language[] Languages { get; private set; } = new[]
    {
        new Language("English", "en-US"),
        new Language("Українська", "uk-UA"),
    };

    private bool _isAllStopped = false;

    public bool IsAllStopped
    {
        get => _isAllStopped;
        set
        {
            _isAllStopped = value;
            OnPropertyChanged();
            ToggleFigureMovementCommand.RaiseCanExecuteChanged();
        }
    }

    #region ListItem bindings

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
            PlusEventFunctionCommand.RaiseCanExecuteChanged();
            MinusEventFunctionCommand.RaiseCanExecuteChanged();
        }
    }

    #endregion

    public string ButtonsGroupHeader => Localization.Resources.Resources.ButtonsGroupHeader;
    private void RaiseButtonsGroupHeaderChanged() => OnPropertyChanged(nameof(ButtonsGroupHeader));

    #region Buttons binding

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

    public string StopAllFigureButtonText
    {
        get
        {
            if (!IsAllStopped)
            {
                return Localization.Resources.Resources.StopAllFigureButtonText;
            }
            else if (IsAllStopped)
            {
                return Localization.Resources.Resources.StartAllFigureButtonText;
            }
            else
            {
                throw new InvalidOperationException("Unacceptable behavior");
            }
        }
    }

    public void RaiseStopFigureButtonChanged()
    {
        OnPropertyChanged(nameof(StopFigureButtonText));
    }

    public void RaiseStopAllFiguresButtonChanged()
    {
        OnPropertyChanged(nameof(StopAllFigureButtonText));

        RaiseStopFigureButtonChanged();
    }

    #endregion

    #region MenuItems binding

    public string FileMenuItem => Localization.Resources.Resources.FileMenuItem;
    public string SaveMenuItem => Localization.Resources.Resources.SaveMenuItem;
    public string OpenMenuItem => Localization.Resources.Resources.OpenMenuItem;

    private void RaiseMenuItemsChanged()
    {
        OnPropertyChanged(nameof(FileMenuItem));
        OnPropertyChanged(nameof(SaveMenuItem));
        OnPropertyChanged(nameof(OpenMenuItem));
    }

    #endregion

    public MainWindowViewModel()
    {
        ToggleFigureMovementCommand =
            new RelayCommand((param) => ToggleFigureMovement(), CanToggleFigureMovementExecute);
        PlusEventFunctionCommand = new RelayCommand(
            (param) => SelectedFigure.NewCollision += SimpleCollisionEffect,
            () => SelectedFigure is not null);
        MinusEventFunctionCommand = new RelayCommand(
            (param) => SelectedFigure.NewCollision -= SimpleCollisionEffect, 
            () => SelectedFigure is not null);
    }

    #region Commands actions

    public void ToggleFigureMovement()
    {
        SelectedFigure.SpeedVector.IsStopped = !SelectedFigure.SpeedVector.IsStopped;

        RaiseStopFigureButtonChanged();
    }

    private bool CanToggleFigureMovementExecute() => SelectedFigure is not null && !IsAllStopped;

    private void SimpleCollisionEffect(object? subject, NewCollisionEventArgs e)
    {
        Console.WriteLine(string.Format(Resources.CollisionMessage, e.Subject, e.From, e.To, e.Point));
        SystemSounds.Beep.Play();
    }

    #endregion

    #region Resources refreshing

    public void RefreshResources()
    {
        RaiseButtonsGroupHeaderChanged();
        RaiseStopFigureButtonChanged();
        RaiseStopAllFiguresButtonChanged();
        RaiseMenuItemsChanged();

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