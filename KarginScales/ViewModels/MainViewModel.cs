using KarginScales.Service;
using System.Collections.ObjectModel;
using System.IO;
using System;
using KarginScales.Models;
using KarginScales.Commands;
using System.Threading.Tasks;

namespace KarginScales.ViewModels;

public class MainViewModel : ViewModelBase
{
    #region Fields

    private double _currentTemperature;
    private double _setupTemperature;
    private double _gamma;
    private Polymer _selectedPolymer;
    private ObservableCollection<Polymer> _polymers;

    #endregion

    public MainViewModel()
    {
        string pathFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Content\\D.xlsx");

        _polymers = new ObservableCollection<Polymer>(ExcelDataService.LoadDataFromFile(pathFile));
        Polymers = new ReadOnlyObservableCollection<Polymer>(_polymers);
    }

    #region Propereties

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

    public double Gamma
    {
        get { return _gamma; }
        set
        {
            SetValue(ref _gamma, value, nameof(Gamma));
        }
    }

    public Polymer SelectedPolymer
    {
        get { return _selectedPolymer; }
        set
        {
            SetValue(ref _selectedPolymer, value, nameof(SelectedPolymer));
            CurrentTemperature = _selectedPolymer.MinT;
            SetupTemperature = _selectedPolymer.MinT;
            Gamma = 0.0;
        }
    }

    public ReadOnlyObservableCollection<Polymer> Polymers { get; }

    #endregion


    #region Commands

    private RelayCommand _raiseTemp;
    private RelayCommand _lowerTemp;
    private RelayAsyncCommand _startMeasurement;

    public RelayCommand RaiseTemp
    {
        get { return _raiseTemp ?? (_raiseTemp = new RelayCommand(o => ++SetupTemperature, o => (SetupTemperature + 1) < SelectedPolymer?.MaxT)); }
    }
    public RelayCommand LowerTemp
    {
        get { return _lowerTemp ?? (_lowerTemp = new RelayCommand(o => --SetupTemperature, o => SetupTemperature > SelectedPolymer?.MinT)); }
    }
    public RelayAsyncCommand StartMeasurement
    {
        get { return _startMeasurement ?? (_startMeasurement = new RelayAsyncCommand(StartMeasurementAsync)); }
    }


    private async void StartMeasurementAsync(object o)
    {

        var targetTemp = SetupTemperature;
        var step = targetTemp > CurrentTemperature ? 0.1 : -0.1;

        while (Math.Abs(CurrentTemperature - targetTemp) > 0.05)
        {
            CurrentTemperature += step;
            await Task.Delay(100);
        }

        CurrentTemperature = targetTemp;

        Gamma = InterpolateGamma(SelectedPolymer, CurrentTemperature) ?? 0.0;
    }

    private double? InterpolateGamma(Polymer? polymer, double temperature)
    {
        if (polymer?.Data == null || polymer.Data.Count == 0)
            return null;

        for (int i = 0; i < polymer.Data.Count; i++)
        {
            if (Math.Abs(polymer.Data[i].Temperature - temperature) < 0.0001)
                return polymer.Data[i].Gamma;


            if (polymer.Data[i].Temperature > temperature)
            {
                var prev = polymer.Data[i - 1];
                var current = polymer.Data[i];

                double k = (current.Gamma - prev.Gamma) / (current.Temperature - prev.Temperature);
                return k * (temperature - prev.Temperature) + prev.Gamma;
            }
        }

        return null;
    }

    #endregion

}
