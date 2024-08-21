using EducationProject1.Models.FigureModels.Abstract;
using EducationProject1.Models.SaveObjects;

namespace EducationProject1.Services.FigureSaveListCreatorServices.Abstract;

public abstract class FigureSaveListCreatorServiceBase
{
    public abstract List<FigureSave> GetFigureSaves(ICollection<MovingFigureBase> figures);
}