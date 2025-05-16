using KarginScales.Models;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace KarginScales.ViewModels;

public class PolymerViewModel : ViewModelBase
{
    private Polymer _model;

    private ObservableCollection<DataPoint> _measuredData = new ObservableCollection<DataPoint>();
    public ReadOnlyObservableCollection<DataPoint> MeasuredData;

    public PolymerViewModel(Polymer model)
    {
        _model = model;
        MeasuredData = new ReadOnlyObservableCollection<DataPoint>(_measuredData);
    }

    public bool AddDataPoint(double temperature, double gamma)
    {
        var dataPoint = new DataPoint(temperature, gamma);
        if (_measuredData.Contains(dataPoint))
            return false;

        _measuredData.Add(dataPoint);
        return true;
    }

    public string Name => _model.Name;

    public double MinT => _model.MinT;

    public double MaxT => _model.MaxT;

    public IReadOnlyList<DataPoint> Data
    {
        get { return _model.Data; }
    }

    public void Update(List<DataPoint> data)
    {
        _model.Update(data);
    }
}
