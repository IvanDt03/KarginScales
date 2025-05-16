using KarginScales.Service;
using System.Collections.ObjectModel;
using System.IO;
using System;
using KarginScales.Models;

namespace KarginScales.ViewModels;

public class MainViewModel : ViewModelBase
{
    private double _currentTemperature;
    private double _setupTemperature;
    private Polymer _selectedPolymer;

    private ObservableCollection<Polymer> _polymers;
    public ReadOnlyObservableCollection<Polymer> Polymers { get; }

    public MainViewModel()
    {
        string pathFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Content\\D.xlsx");

        _polymers = new ObservableCollection<Polymer>(ExcelDataService.LoadDataFromFile(pathFile));
        Polymers = new ReadOnlyObservableCollection<Polymer>(_polymers);
    }

    public double CurrentTemperature
    {
        get { return _currentTemperature; }
        set
        {
            SetValue(ref _currentTemperature, value, nameof(CurrentTemperature));
        }
    }

    public double SetupTemperature
    {
        get { return _setupTemperature; }
        set
        {
            SetValue(ref _setupTemperature, value, nameof(SetupTemperature));
        }
    }

    public Polymer SelectedPolymer
    {
        get { return _selectedPolymer; }
        set
        {
            SetValue(ref _selectedPolymer, value, nameof(SelectedPolymer));
            CurrentTemperature = _selectedPolymer.MinT;
        }
    }
}
