using EducationProject1.Models.SecondaryModels;

namespace EducationProject1.Models.Abstract;

public interface IMovable
{
    public MoveVector MoveVector { get; set; }
}