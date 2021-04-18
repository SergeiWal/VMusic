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

namespace VMusic.ViewModels.Autorization
{
    class RegistrationViewModel : BaseViewModel
    {

        private Window owner;

        private string name;
        private string login;
        private string password;
        private string repeatPassword;

        public RegistrationViewModel(Window owner)
        {
            this.owner = owner;
        }


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

        private Command registrationCommand;
        private Command switchToLoginCommand;
        private Command switchToLoginAsAdminCommand;
        private Command exitCommand;

        public Command RegistrationCommand
        {
            get
            {
                return registrationCommand ?? (registrationCommand = new Command((obj) => {
                    owner.Close();
                }));
            }
        }

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
