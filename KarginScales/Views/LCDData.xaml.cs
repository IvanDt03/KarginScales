using System.Windows;
using System.Windows.Controls;


namespace KarginScales.Views;

public partial class LCDData : UserControl
{
    public static readonly DependencyProperty DataProperty;
    public static readonly DependencyProperty TitleProperty;

    static LCDData()
    {
        FrameworkPropertyMetadata metadataData = new FrameworkPropertyMetadata(default(double));
        DataProperty = DependencyProperty.Register("Data", typeof(double), typeof(LCDData), metadataData);

        TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(LCDData));
    }

    public double Data
    {
        get => (double)GetValue(DataProperty);
        set => SetValue(DataProperty, value);
    }

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public LCDData()
    {
        InitializeComponent();
    }
}
