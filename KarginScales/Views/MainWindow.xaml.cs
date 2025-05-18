using KarginScales.Service;
using KarginScales.ViewModels;
using System.Windows;
using System.Windows.Threading;


namespace KarginScales.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        DataContext = new MainViewModel(new MessageBoxDialogService(), new ExcelDataService());
    }
}