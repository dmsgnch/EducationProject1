namespace EducationProject1.Models.SecondaryModels;

public class Language
{
    public string Name { get; set; }
    public string Code { get; set; }

    public Language(string name, string code)
    {
        Name = name;
        Code = code;
    }

    public override string ToString() => $"{Name} ({Code})";
}