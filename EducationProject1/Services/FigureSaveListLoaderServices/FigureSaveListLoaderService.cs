using System.Reflection;
using EducationProject1.Models.FigureModels.Abstract;
using EducationProject1.Models.SaveObjects;
using EducationProject1.Models.SecondaryModels;
using EducationProject1.Services.FigureSaveListLoaderServices.Abstract;
using EducationProject1.Views;

namespace EducationProject1.Services.FigureSaveListLoaderServices;

public class FigureSaveListLoaderService : FigureSaveListLoaderServiceBase
{
    public override void LoadFiguresSave(
        ICollection<FigureSave> figuresSaves, 
        MainWindow mainWindow)
    {
        var figures = GetFigures(figuresSaves);

        mainWindow.MainWindowViewModel.Figures.Clear();
        mainWindow.MyCanvas.Children.Clear();
        figures.ForEach(f => mainWindow.CreateNewFigure(f));
    }
    
    private List<MovingFigureBase> GetFigures(ICollection<FigureSave> figuresSaves)
    {
        return figuresSaves
            .Select(fs => 
            {
                ConstructorInfo constructor = GetFigureConstructor(fs.FigureStringType);
                var figure = (MovingFigureBase)constructor.Invoke(new object[] { fs.SpeedVector, fs.Size });

                figure.Position = fs.Position;
                return figure;
            })
            .ToList();
    }

    private ConstructorInfo GetFigureConstructor(string figureTypeName)
    {
        Type? figureType = Type.GetType(figureTypeName);
        if (figureType is null)
        {
            throw new ArgumentException($"Type '{figureTypeName}' not found.");
        }

        // Получение конструктора с параметром SpeedVector
        Type[] parameterTypes = new Type[] { typeof(SpeedVector), typeof(FigureSize)};
        ConstructorInfo? constructor = figureType.GetConstructor(parameterTypes);
        if (constructor is null)
        {
            throw new InvalidOperationException($"Type '{figureTypeName}' does not have a constructor with the specified parameters.");
        }

        return constructor;
    }
}