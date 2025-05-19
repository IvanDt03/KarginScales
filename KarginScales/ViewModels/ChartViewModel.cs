using KarginScales.Models;
using LiveChartsCore;
using LiveChartsCore.Kernel;
using LiveChartsCore.Kernel.Sketches;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System.Collections.ObjectModel;

namespace KarginScales.ViewModels;

public class ChartViewModel : Notifier
{
    private ObservableCollection<ISeries> _series;
    private ObservableCollection<ICartesianAxis> _xAxis;
    private ObservableCollection<ICartesianAxis> _yAxis;

    public ChartViewModel()
    {
        _series = new ObservableCollection<ISeries>()
        {
            new LineSeries<DataPoint>
            {
                Values = new ObservableCollection<DataPoint>(),
                Mapping = (point, index) => new Coordinate(point.Temperature, point.Gamma),
                Fill = null
            },

            new LineSeries<DataPoint>
            {
                Values = new ObservableCollection<DataPoint>(),
                Mapping = (point, index) => new Coordinate(point.Temperature, point.Gamma),
                Fill = null
            }
        };

        _xAxis = new ObservableCollection<ICartesianAxis>()
        {
            new Axis
            {
                Name = "Температура, °C",
                NamePaint = new SolidColorPaint(SKColors.Black),
                LabelsPaint = new SolidColorPaint(SKColors.Black)

            }
        };

        _yAxis = new ObservableCollection<ICartesianAxis>()
        {
            new Axis
            {
                Name = "Гамма (деформация)",
                NamePaint = new SolidColorPaint(SKColors.Black),
                LabelsPaint = new SolidColorPaint(SKColors.Black),
            }
        };
    }

    public ObservableCollection<ISeries> Series
    {
        get { return _series; }
        set
        {
            SetValue(ref _series, value, nameof(Series));
        }
    }

    public ObservableCollection<ICartesianAxis> XAxis
    {
        get { return _xAxis; }
        private set { SetValue(ref _xAxis, value, nameof(XAxis)); }
    }

    public ObservableCollection<ICartesianAxis> YAxis
    {
        get { return _yAxis; }
        private set { SetValue(ref _yAxis, value, nameof(YAxis)); }
    }

    public void UpdateChart(Polymer selected)
    {
        if (selected == null)
            return;

        var dataForTeacher = Series[0] as LineSeries<DataPoint>;
        var measuredData = Series[1] as LineSeries<DataPoint>;

        if (dataForTeacher != null)
            dataForTeacher.Values = selected.Data;

        if (measuredData != null)
            measuredData.Values = selected.MeasuredData;


        OnPropertyChanged(nameof(Series));
    }
}

