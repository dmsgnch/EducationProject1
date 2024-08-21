using EducationProject1.Models.FigureModels.Abstract;
using EducationProject1.Models.SaveObjects;
using EducationProject1.Services.FigureSaveListCreatorServices.Abstract;

namespace EducationProject1.Services.FigureSaveListCreatorServices;

public class FigureSaveListCreatorService : FigureSaveListCreatorServiceBase
{
    public override List<FigureSave> GetFigureSaves(ICollection<MovingFigureBase> figures)
    {
        return figures.Select(f => new FigureSave(
            f.Position,
            f.SpeedVector,
            f.Size,
            f.GetType().FullName
        )).ToList();
    }
}