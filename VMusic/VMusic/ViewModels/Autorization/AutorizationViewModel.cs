using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using VMusic.Commands;
using VMusic.Views.Admin;
using VMusic.Views.Autorization;
using VMusic.Views.Client;

namespace VMusic.ViewModels.Autorization
{
    class AutorizationViewModel : BaseViewModel
    {
        private string login;
        private string password;

        public AutorizationViewModel(Window owner): base(owner)
        {
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

        private Command loginCommand;
        private Command loginAsAdminCommand;
        private Command switchToLoginCommand;
        private Command switchToLoginAsAdminCommand;
        private Command switchToRegistrationCommand;
        

        public Command SwitchToLoginCommand 
        {
            get 
            {
                return switchToLoginCommand ?? (switchToLoginCommand = new Command((obj) =>
                {
                   SwitchTo(new Login(), owner);
                }));
            }
        }

        public Command SwitchToLoginAsAdminCommand
        {
            get
            {
                return switchToLoginAsAdminCommand ?? (switchToLoginAsAdminCommand = new Command((obj) => {
                    SwitchTo(new LoginAsAdmin(), owner);
                }));
            }
        }

        public Command SwitchToRegistrationCommand
        {
            get
            {
                return switchToRegistrationCommand ?? (switchToRegistrationCommand = new Command((obj) => {
                    SwitchTo(new Registration(), owner);
                }));
            }
        }

        public Command LoginCommand 
        {
            get 
            {
                return loginCommand ?? ( loginCommand = new Command((obj)=> {
                    SwitchTo(new ClientMainWindow(), owner);
                }));
            }
        }
        public Command LoginAsAdminCommand
        {
            get
            {
                return loginAsAdminCommand ?? (loginAsAdminCommand = new Command((obj) => {
                   SwitchTo(new AdminMainWindow(), owner);
                }));
            }
        }

    }
}
