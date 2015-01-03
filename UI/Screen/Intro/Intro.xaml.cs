using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace UI.Screen.Intro
{
    /// <summary>
    /// Logique d'interaction pour Home.xaml
    /// </summary>
    public partial class Intro : UserControl
    {
        public event IntroEndEventHandler OnIntroEnd;
        public delegate void IntroEndEventHandler(Intro i, EventArgs e);
        private bool EventSent { get; set; }

        public Intro()
        {
            InitializeComponent();
        }

        private void Animation_Complete(object sender, EventArgs e)
        {
            if (OnIntroEnd == null || EventSent) return;
            EventSent = true;
            OnIntroEnd(this, null);
        }

        private void Animation_Skip(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            Animation_Complete(sender, e);
        }
    }
}
