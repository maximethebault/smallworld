using System.Windows;

namespace UI
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var window = new MainWindowView { DataContext = new MainWindowViewModel() };
            window.Show();
        }
    }
}
