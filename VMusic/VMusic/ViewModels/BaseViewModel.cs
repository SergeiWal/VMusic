using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using VMusic.Commands;

namespace VMusic.ViewModels
{
    class BaseViewModel: INotifyPropertyChanged
    {

        protected Window owner;

        public BaseViewModel(Window owner)
        {
            this.owner = owner;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        protected void SwitchTo(Window window, Window owner)
        {
            window.Show();
            owner.Close();
        }

        private Command exitCommand;
        public Command ExitCommand
        {
            get
            {
                return exitCommand ?? (exitCommand = new Command((obj) => {
                    owner.Close();
                }));
            }
        }
    }
}
