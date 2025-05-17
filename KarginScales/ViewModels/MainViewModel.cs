using KarginScales.Service;
using System.IO;
using System;
using KarginScales.Models;
using KarginScales.Commands;
using System.Collections.Generic;
using System.ComponentModel;

namespace KarginScales.ViewModels;

public class MainViewModel : Notifier
{
    #region Fields

    private double _currentTemperature;
    private double _setupTemperature;
    private double _gamma;
    private MeasuringDevice _device;
    private Polymer _selectedPolymer;
    public List<Polymer> Polymers { get; }
    #endregion

    public MainViewModel()
    {
        string pathFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Content\\D.xlsx");
        Polymers = new List<Polymer>(ExcelDataService.LoadDataFromFile(pathFile));

        _device = new MeasuringDevice();
        _device.PropertyChanged += DeviceOnPropertyChanged;
    }

    private void DeviceOnPropertyChanged(object sender, PropertyChangedEventArgs args)
    {
        switch (args.PropertyName)
        {
            case nameof(_device.CurrentTemperature):
                CurrentTemperature = _device.CurrentTemperature;
                break;
            case nameof(_device.Gamma):
                Gamma = _device.Gamma;
                break;
            case nameof(_device.IsRunning):
                _startMeasurement.RaiseCanExecuteChanged();
                break;
        }
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

    #endregion


    #region Commands

    private RelayCommand _raiseTemp;
    private RelayCommand _lowerTemp;
    private RelayCommand _startMeasurement;

    public RelayCommand RaiseTemp
    {
        get { return _raiseTemp ?? (_raiseTemp = new RelayCommand(o => ++SetupTemperature, o => (SetupTemperature + 1) < SelectedPolymer?.MaxT)); }
    }
    public RelayCommand LowerTemp
    {
        get { return _lowerTemp ?? (_lowerTemp = new RelayCommand(o => --SetupTemperature, o => SetupTemperature > SelectedPolymer?.MinT)); }
    }
    public RelayCommand StartMeasurement
    {
        get { return _startMeasurement ?? 
                (_startMeasurement = new RelayCommand(OnStartMeasurement, o => !_device.IsRunning)); }
    }

    private void OnStartMeasurement(object p)
    {
        _device.StartMeasurement(SelectedPolymer, CurrentTemperature, SetupTemperature);
    }

    #endregion

}
