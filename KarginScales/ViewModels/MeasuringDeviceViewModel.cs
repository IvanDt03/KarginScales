using KarginScales.Models;

namespace KarginScales.ViewModels;

public class MeasuringDeviceViewModel : Notifier
{
    private MeasuringDevice _model;

    public MeasuringDeviceViewModel()
    {
        _model = new MeasuringDevice();
        _model.PropertyChanged += (s, e) => OnPropertyChanged(nameof(e.PropertyName));
    }

    public double CurrentTemperature => _model.CurrentTemperature;
    public double Gamma => _model.Gamma;
    public bool IsRunning => _model.IsRunning;

    public void StartMeasurement(Polymer selected, double currentTemperature, double setupTemperature)
    {
        _model.StartMeasurement(selected, currentTemperature, setupTemperature);
    }
}
