using KarginScales.Service;
using KarginScales.ViewModels;
using System.Windows;

namespace KarginScales.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }


    public void Button_ClickUp(object sender, RoutedEventArgs e)
    {
        setup.Data += 1;
    }

    public void Button_ClickDown(object sender, RoutedEventArgs e)
    {
        setup.Data -= 1;
    }
}