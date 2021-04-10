using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using VMusic.Commands;
using VMusic.Views.Autorization;

namespace VMusic.ViewModels
{
    class AutorizationModel : INotifyPropertyChanged
    {
        private string login;
        private string password;

        
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

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        private Command loginCommand;
        private Command loginAsAdminCommand;
        private Command exitCommand;
        
        public Command LoginCommand 
        {
            get 
            {
                return loginCommand ?? ( loginCommand = new Command((obj)=> {
                    closeAutorizationWindows();
                }));
            }
        }
        public Command LoginAsAdminCommand
        {
            get
            {
                return loginAsAdminCommand ?? (loginAsAdminCommand = new Command((obj) => {
                    closeAutorizationWindows();
                }));
            }
        }
  
        public Command ExitCommand
        {
            get 
            {
                return exitCommand ?? (exitCommand = new Command((obj)=> {
                    closeAutorizationWindows();
                }));
            }
        }

        private  void closeAutorizationWindows()
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
