using System.Windows;
using System.Windows.Controls;

namespace KarginScales.Views;

public static class PasswordBoxHelper
{
    public static readonly DependencyProperty PasswordProperty =
        DependencyProperty.RegisterAttached(
            "Password",
            typeof(string),
            typeof(PasswordBoxHelper),
            new FrameworkPropertyMetadata(string.Empty, OnPasswordPropertyChanged));

    public static string GetPassword(DependencyObject d)
    {
        return (string)d.GetValue(PasswordProperty);
    }

    public static void SetPassword(DependencyObject d, string value)
    {
        d.SetValue(PasswordProperty, value);
    }

    private static void OnPasswordPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is PasswordBox passwordBox)
        {
            passwordBox.PasswordChanged -= PasswordBox_PasswordChanged;
            passwordBox.Password = (string)e.NewValue;
            passwordBox.PasswordChanged += PasswordBox_PasswordChanged;
        }
    }

    private static void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
    {
        if (sender is PasswordBox passwordBox)
        {
            SetPassword(passwordBox, passwordBox.Password);
        }
    }
}
