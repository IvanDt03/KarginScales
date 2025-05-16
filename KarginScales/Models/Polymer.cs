using System.Collections.Generic;
using System.Linq;

namespace KarginScales.Models;

public class Polymer
{
    private string _name;
    private double _minT;
    private double _maxT;
    private List<DataPoint> _data;

    public Polymer(string name) 
    {
        _name = name;
        _data = new List<DataPoint>();
    }

    private void Initialize(List<DataPoint> data)
    {
        _data = data;
        _minT = data.Min(p => p.Temperature);
        _maxT = data.Max(p => p.Temperature);
    }

    public string Name
    {
        get { return _name; }
    }
    public double MinT
    {
        get { return _minT; }
    }
    public double MaxT
    {
        get { return _maxT; }
    }
    public IReadOnlyList<DataPoint> Data
    {
        get { return _data; }
    }

    public void Update(List<DataPoint> data)
    {
        if (_data.Count != 0)
            return;
        Initialize(data);
    }
}
