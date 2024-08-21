namespace EducationProject1.Components.Saves;

public static class SaveInfo
{
    public const string Signature = "MyEdApp";
    public static SaveVersion ActualVersion { get; } = new SaveVersion(1, 1, 0);

    public static bool IsCompatibleVersion(SaveVersion version)
    {
        return !(version.Major < ActualVersion.Major || version.Minor < ActualVersion.Minor);
    }
}