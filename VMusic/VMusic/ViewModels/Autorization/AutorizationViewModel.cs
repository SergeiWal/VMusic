using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using VMusic.Commands;
using VMusic.Hasher;
using VMusic.Repository;
using VMusic.Views.Admin;
using VMusic.Views.Autorization;
using VMusic.Views.Client;

namespace VMusic.ViewModels.Autorization
{
    class AutorizationViewModel : BaseWindowViewModel
    {
        private UserRepository userRepository;
        private string infoMessage = "";

        private string login;
        private string password;

        public AutorizationViewModel(Window owner): base(owner)
        {
            userRepository = new UserRepository();
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

        public string InfoMessage
        {
            get => infoMessage;
            set
            {
                infoMessage = value;
                OnPropertyChanged("InfoMessage");
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
                return loginCommand ?? ( loginCommand = new Command((obj)=>
                {
                    var passBox = obj as PasswordBox;
                    Password = passBox.Password;
                    LoginFunc();
                }));
            }
        }
        public Command LoginAsAdminCommand
        {
            get
            {
                return loginAsAdminCommand ?? (loginAsAdminCommand = new Command((obj) => {
                    var passBox = obj as PasswordBox;
                    Password = passBox.Password;
                    LoginAsAdminFunc();
                }));
            }
        }

        private void LoginFunc()
        {
            if (IsFieldsNotEmpty())
            {
                string passwordHash = PasswordHasher.GetHash(Password);
                var obj = userRepository.GetAllObject().FirstOrDefault(s => s.Login == Login && s.Password == passwordHash);
                if (obj != null)
                {
                    if (!obj.IsBlocked)
                    {
                        SwitchTo(new ClientMainWindow(), owner);
                    }
                    else
                    {
                        InfoMessage = "Пользователь заблокирован!!!";
                    }
                }
                else
                {
                    InfoMessage = "Данные не верны!!!";
                }
            }
            else
            {
                InfoMessage = "Поля не заполнены!!!";
            }
        }

        private void LoginAsAdminFunc()
        {
            if (IsFieldsNotEmpty())
            {
                string passwordHash = PasswordHasher.GetHash(Password);
                var obj = userRepository.GetAllObject().FirstOrDefault(s => s.Login == Login && s.Password == passwordHash);
                if (obj != null && obj.IsAdmin)
                {
                    SwitchTo(new AdminMainWindow(), owner);
                }
                else
                {
                    InfoMessage = "Данные не верны!!!";
                }
            }
            else
            {
                InfoMessage = "Поля не заполнены!!!";
            }
        }

        private bool IsFieldsNotEmpty()
        {
            return !string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(Login);
        }

    }
}
