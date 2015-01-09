using System;
using System.Windows;
using System.Windows.Controls;

namespace UI.Screen.Intro
{
    /// <summary>
    /// Logique d'interaction pour IntroView.xaml
    /// </summary>
    public partial class IntroView : UserControl
    {
        public IntroView()
        {
            InitializeComponent();
        }

        private void OnStoryboardCompleted(object sender, EventArgs e)
        {
            var viewModel = DataContext as IntroViewModel;
            if (viewModel != null)
            {
                viewModel.IntroCompletedCommand.Execute(null);
            }
        }
    }
}
