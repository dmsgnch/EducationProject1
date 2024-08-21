using EducationProject1.Models.SecondaryModels;
using MessagePack;

namespace EducationProject1.Models.SaveObjects;

[MessagePackObject]
[Serializable]
public class FigureSave
{
    [Key(0)]
    public Position Position { get; set; }
    [Key(1)]
    public SpeedVector SpeedVector { get; set; }
    [Key(2)]
    public FigureSize Size { get; set; }
    [Key(3)]
    public string FigureStringType { get; set; }

    public FigureSave()
    { }

    public FigureSave(
        Position position, 
        SpeedVector speedVector, 
        FigureSize size, 
        string figureStringType)
    {
        Position = position;
        SpeedVector = speedVector;
        Size = size;
        FigureStringType = figureStringType;
    }
}