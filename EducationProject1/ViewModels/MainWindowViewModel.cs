using System.Collections.ObjectModel;
using EducationProject1.Commands;
using EducationProject1.Models;
using EducationProject1.Models.Abstract;

namespace EducationProject1.ViewModels;

public class MainWindowViewModel
{
    public ObservableCollection<FigureBase> Figures { get; set; } = new();

    public MainWindowViewModel()
    { }
}