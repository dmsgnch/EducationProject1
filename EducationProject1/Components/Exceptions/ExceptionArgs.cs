namespace EducationProject1.Components.Exceptions;

[Serializable]
public abstract class ExceptionArgs {
    public virtual String Message => String.Empty;
}