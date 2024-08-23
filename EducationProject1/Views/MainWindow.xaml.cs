using System.Globalization;
using System.Windows;
using System.Windows.Media;
using EducationProject1.Components.Exceptions;
using EducationProject1.Components.Helpers;
using EducationProject1.Components.Loggers;
using EducationProject1.Components.Loggers.Abstract;
using EducationProject1.Components.Saves.FileSavers;
using EducationProject1.Components.Saves.FileSavers.Abstract;
using EducationProject1.Models.FigureModels;
using EducationProject1.Models.FigureModels.Abstract;
using EducationProject1.Models.SaveObjects;
using EducationProject1.Models.SecondaryModels;
using EducationProject1.Services.FigureSaveListCreatorServices.Abstract;
using EducationProject1.Services.FigureSaveListLoaderServices.Abstract;
using EducationProject1.Services.SaveLoaderSelectorServices.Abstract;
using EducationProject1.ViewModels;
using Microsoft.Extensions.Logging;
using Microsoft.Win32;
using Rectangle = EducationProject1.Models.FigureModels.Rectangle;

namespace EducationProject1.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    internal readonly MainWindowViewModel MainWindowViewModel;
    private readonly LogMachineBase _logger;
    private readonly SaveLoaderSelectorServiceBase _saveLoader;
    private readonly FigureSaveListCreatorServiceBase _saveCreator;
    private readonly FigureSaveListLoaderServiceBase _figuresSaveLoader;

    public MainWindow(
        LogMachineBase logger,
        SaveLoaderSelectorServiceBase saveLoader,
        FigureSaveListCreatorServiceBase saveCreator,
        FigureSaveListLoaderServiceBase figuresSaveLoader)
    {
        InitializeComponent();

        _logger = logger;
        _saveLoader = saveLoader;
        _saveCreator = saveCreator;
        _figuresSaveLoader = figuresSaveLoader;
        MainWindowViewModel = new MainWindowViewModel();

        DataContext = MainWindowViewModel;

        AddRenderEventHandler();

        SetLanguages();
    }

    private void AddRenderEventHandler()
    {
        CompositionTarget.Rendering += MoveFigures;
        CompositionTarget.Rendering += CollisionCheck;
    }

    private void DeleteRenderEventHandler()
    {
        CompositionTarget.Rendering -= MoveFigures;
        CompositionTarget.Rendering -= CollisionCheck;
    }

    private void SetLanguages()
    {
        LanguageComboBox.ItemsSource = MainWindowViewModel.Languages;
        MainWindowViewModel.SelectedLanguage = MainWindowViewModel.Languages[0];
    }

    private void MoveFigures(object? sender, EventArgs e)
    {
        foreach (var figure in MainWindowViewModel.Figures)
        {
            ExecuteFigureMoving(figure);
        }
    }

    private void ExecuteFigureMoving(MovingFigureBase figure)
    {
        try
        {
            figure.Move(MyCanvas);
        }
        catch(Exception<FigureOutOfPanelException> ex)
        {
            _logger.LogToFile(LogType.WARNING, ex.Message);
            figure.ReturnFigureToThePanel(MyCanvas);
        }
        catch(Exception ex)
        {
            _logger.LogToFile(LogType.ERROR, ex.Message);
            throw new Exception($"Unexpected exception: {ex.Message}");
        }
    }

    private List<FigurePair> PreviousCollisions { get; set; } = new();

    private void CollisionCheck(object? sender, EventArgs e)
    {
        var collisions = GetCollisions();

        var previousCollisions = PreviousCollisions;
        PreviousCollisions = new();

        foreach (var collision in collisions)
        {
            if (!previousCollisions.Contains(collision))
            {
                RaiseCollisionEvent(collision);
            }

            PreviousCollisions.Add(collision);
        }
    }

    private List<FigurePair> GetCollisions()
    {
        List<FigurePair> figureCollisions = new();

        var groupedFigures = MainWindowViewModel.Figures.GroupBy(f => f.GetType());

        foreach (var group in groupedFigures)
        {
            var figureArray = group.ToArray();


            for (int i = 0; i < figureArray.Length; i++)
            {
                for (int j = 0; j < figureArray.Length; j++)
                {
                    if(i.Equals(j)) continue;
                    
                    if (figureArray[i].GetBoundingRect().IntersectsWith(figureArray[j].GetBoundingRect()))
                    {
                        figureCollisions.Add(new FigurePair(figureArray[i], figureArray[j]));
                    }
                }
            }
        }

        return figureCollisions;
    }

    private void RaiseCollisionEvent(FigurePair figuresPair)
    {
        figuresPair.First.SimulateNewCollision(
            figuresPair.First.FigureName,
            figuresPair.Second.FigureName,
            Localization.Resources.Resources.Collision,
            CollisionPointCalculateHelper.GetFiguresCollisionPoint(figuresPair.First, figuresPair.Second).ToString());
    }

    #region Figures spawn

    private void OnTriangleButton_Click(object sender, RoutedEventArgs e) => CreateNewFigure(new Triangle());
    private void OnRectangleButton_Click(object sender, RoutedEventArgs e) => CreateNewFigure(new Rectangle());
    private void OnCircleButton_Click(object sender, RoutedEventArgs e) => CreateNewFigure(new Circle());

    internal void CreateNewFigure(MovingFigureBase movingFigure)
    {
        movingFigure.Draw(MyCanvas);

        ((MainWindowViewModel)DataContext).Figures.Add(movingFigure);
    }

    #endregion

    #region Nav panel elements

    private void OnLanguageComboBox_SelectionChanged(object sender, RoutedEventArgs e)
    {
        if (LanguageComboBox.SelectedItem is Language language)
        {
            CultureInfo culture = new CultureInfo(language.Code);
            CultureInfo.CurrentUICulture = culture;
            CultureInfo.CurrentCulture = culture;

            MainWindowViewModel.RefreshResources();
        }
    }

    private void OnStopAllFiguresButton_Click(object sender, RoutedEventArgs e)
    {
        MainWindowViewModel.IsAllStopped = !MainWindowViewModel.IsAllStopped;

        if (MainWindowViewModel.IsAllStopped)
        {
            StopAllFiguresAndRendering();
        }
        else
        {
            StartAllFiguresAndRendering();
        }

        MainWindowViewModel.RaiseStopAllFiguresButtonChanged();
    }

    private void StopAllFiguresAndRendering()
    {
        SetAllFiguresIsStopped(true);
        DeleteRenderEventHandler();
    }

    private void StartAllFiguresAndRendering()
    {
        SetAllFiguresIsStopped(false);
        AddRenderEventHandler();
    }

    private void SetAllFiguresIsStopped(bool isStopped)
    {
        foreach (var figure in MainWindowViewModel.Figures)
        {
            figure.SpeedVector.IsStopped = isStopped;
        }
    }

    #endregion

    #region Menu items event handlers

    private async void OnMenuItemOpen_Click(object sender, RoutedEventArgs e)
    {
        OpenFileDialog openFileDialog = new OpenFileDialog
        {
            Filter =
                "All supported files (*.bin, *.xml, *.json)|*.bin;*.xml;*.json|Binary files (*.bin)|*.bin|XML files (*.xml)|*.xml|JSON files (*.json)|*.json",
            Title = Localization.Resources.Resources.SelectFile,
            Multiselect = false
        };
        if (openFileDialog.ShowDialog() == true)
        {
            string selectedFilePath = openFileDialog.FileName;

            var figuresSaves = await _saveLoader.GetSaveLoader(selectedFilePath)
                .GetSaveDataOrNullAsync<List<FigureSave>>(selectedFilePath);

            if (figuresSaves is not null)
            {
                _figuresSaveLoader.LoadFiguresSave(figuresSaves, this);
            }
        }
    }

    private async void OnMenuItemSaveBinary_Click(object sender, RoutedEventArgs e)
    {
        FileSaverBase fileSaver = new FileSaverBinary();

        await SaveFigureListAsync(fileSaver);
    }

    private async void OnMenuItemSaveXml_Click(object sender, RoutedEventArgs e)
    {
        FileSaverBase fileSaver = new FileSaverXml();

        await SaveFigureListAsync(fileSaver);
    }

    private async void OnMenuItemSaveJson_Click(object sender, RoutedEventArgs e)
    {
        FileSaverBase fileSaver = new FileSaverJson();

        await SaveFigureListAsync(fileSaver);
    }

    private async Task SaveFigureListAsync(FileSaverBase fileSaver)
    {
        if (MainWindowViewModel.Figures.Count.Equals(0))
        {
            MessageBox.Show(
                Localization.Resources.Resources.MessageAddAtLeastOneFigure,
                Localization.Resources.Resources.CaptionWarning);
            return;
        }

        //Stop updating figures position
        DeleteRenderEventHandler();

        await fileSaver.SaveInFile(_saveCreator.GetFigureSaves(MainWindowViewModel.Figures));

        AddRenderEventHandler();
    }

    #endregion
}