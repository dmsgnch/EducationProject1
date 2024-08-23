namespace EducationProject1.Components.Exceptions;

[Serializable]
public sealed class FigureOutOfPanelException : ExceptionArgs
{
    private String FigureName { get; init; }
    private String FigurePositionString { get; init; }

    public FigureOutOfPanelException(String figureName, String figurePositionString)
    {
        FigureName = figureName;
        FigurePositionString = figurePositionString;
    }

    public override String Message => $"{FigureName} in point {FigurePositionString}";
}