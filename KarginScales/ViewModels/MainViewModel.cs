using KarginScales.Service;
using System.Collections.ObjectModel;
using System.Linq;

namespace KarginScales.ViewModels;

public class MainViewModel : ViewModelBase
{
    private IDataService _dataService;
    private double _currentTemperature;
    private double _setupTemperature;
    private PolymerViewModel _selectedPolymer;

    private ObservableCollection<PolymerViewModel> _polymers;
    public ReadOnlyObservableCollection<PolymerViewModel> Polymers;

    public MainViewModel(IDataService dataService)
    {
        _dataService = dataService;

        Initialize();
    }

    private void Initialize()
    {
        _polymers = new ObservableCollection<PolymerViewModel>(_dataService.LoadNamesPolymer().Select(p => new PolymerViewModel(p)));
        Polymers = new ReadOnlyObservableCollection<PolymerViewModel>(_polymers);
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

    public PolymerViewModel SelectedPolymer
    {
        get { return _selectedPolymer; }
        set
        {
            SetValue(ref _selectedPolymer, value, nameof(SelectedPolymer));
            SelectedPolymer.Update(_dataService.LoadDataPoint(_selectedPolymer.Name));
            CurrentTemperature = _selectedPolymer.MinT;
        }
    }
}
