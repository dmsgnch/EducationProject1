using EducationProject1.Models.SaveObjects;
using EducationProject1.Views;

namespace EducationProject1.Services.FigureSaveListLoaderServices.Abstract;

public abstract class FigureSaveListLoaderServiceBase
{
    public abstract void LoadFiguresSave(ICollection<FigureSave> figuresSaves, 
        MainWindow mainWindow);
}