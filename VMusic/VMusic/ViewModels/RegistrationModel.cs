using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using VMusic.Commands;
using VMusic.Views.Autorization;

namespace VMusic.ViewModels
{
    class RegistrationModel: INotifyPropertyChanged
    {
        private string name;
        private string login;
        private string password;
        private string repeatPassword;


        public string Name
        {
            get => name;
            set 
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        public string Login
        {
            get => login;
            set
            {
                login = value;
                OnPropertyChanged("Login");
            }
        }

        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }
        public string RepeatPassword
        {
            get => repeatPassword;
            set
            {
                repeatPassword = value;
                OnPropertyChanged("RepeatPassword");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        private Command registerCommand;
        private Command exitCommand;

        public Command RegisterCommand
        {
            get
            {
                return registerCommand ?? (registerCommand = new Command((obj) => {
                    closeAutorizationWindows();
                }));
            }
        }
        public Command ExitCommand
        {
            get
            {
                return exitCommand ?? (exitCommand = new Command((obj) => {
                    closeAutorizationWindows();
                }));
            }
        }

        private void closeAutorizationWindows()
        {
            var windows = Application.Current.Windows;
            foreach (var window in windows)
            {
                var currentWindow = window as Window;
                if (currentWindow is Autorization)
                {
                    currentWindow.Close();
                }
            }
        }
    }
}
