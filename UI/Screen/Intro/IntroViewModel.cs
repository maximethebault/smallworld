using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace UI.Screen.Intro
{
    class IntroViewModel : ViewModelBase
    {
        public event IntroEndEventHandler OnIntroEnd;
        public delegate void IntroEndEventHandler(IntroViewModel i, EventArgs e);

        public ICommand IntroCompletedCommand { get; private set; }

        public IntroViewModel()
        {
            IntroCompletedCommand = new DelegateCommand(o => NextScreen());
        }

        private void NextScreen()
        {
            if (OnIntroEnd == null) return;
            OnIntroEnd(this, null);
        }
    }
}
