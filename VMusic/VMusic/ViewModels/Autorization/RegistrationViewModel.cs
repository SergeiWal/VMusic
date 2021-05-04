using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using VMusic.Commands;
using VMusic.Views.Autorization;
using VMusic.Hasher;
using VMusic.Models;
using VMusic.Repository;


namespace VMusic.ViewModels.Autorization
{
    class RegistrationViewModel : BaseWindowViewModel
    {

        private const string DUPLICATE_USER_DATA = "Пользаватель с таким именем или логином уже существует!!!";
        private const string PASSWORDS_NOT_EQUEL = "Пароли не совпадают!!!";
        private const string FIELDS_EMPTY = "Заполнены не все поля!!!";


        private UserRepository userRepository;

        private string name;
        private string login;
        private string password;
        private string repeatPassword;

        private string infoMessage = "";

        public RegistrationViewModel(Window owner): base(owner)
        {
            userRepository = new UserRepository();
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
                password = PasswordHasher.GetHash(value);
                OnPropertyChanged("Password");
            }
        }
        public string RepeatPassword
        {
            get => repeatPassword;
            set
            {
                repeatPassword = PasswordHasher.GetHash(value);
                OnPropertyChanged("RepeatPassword");
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

        private Command registrationCommand;
        private Command switchToLoginCommand;
        private Command switchToLoginAsAdminCommand;
        
        public Command RegistrationCommand
        {
            get
            {
                return registrationCommand ?? (registrationCommand = new Command((obj) => {
                    Registration();
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


        private void Registration()
        {
            if (IsFieldNotEmpty())
            {
                if (IsRepeatPasswordEquals())
                {
                    if (DuplicateCheck())
                    {
                        User user = new User()
                        {
                            Name = this.Name,
                            Login = this.Login,
                            Password = this.Password,
                            IsAdmin = false,
                            IsBlocked = false
                        };
                        userRepository.Create(user);
                        userRepository.Save();
                        SwitchTo(new Login(), owner);
                    }
                    else
                    {
                        InfoMessage = DUPLICATE_USER_DATA;
                    }
                }
                else
                {
                    InfoMessage = PASSWORDS_NOT_EQUEL;
                }
            }
            else
            {
                InfoMessage = FIELDS_EMPTY;
            }
        }

        private bool IsFieldNotEmpty()
        {
            return !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Login) && !string.IsNullOrEmpty(Password)
                   && !string.IsNullOrEmpty(RepeatPassword);
        }

        private bool IsRepeatPasswordEquals()
        {
            return Password == RepeatPassword;
        }

        private bool DuplicateCheck()
        {
            IEnumerable<User> users = userRepository.GetAllObject().Where(s => s.Name == Name || s.Login == Login);
            if (users.Count() > 0)
            {
                return false;
            }
            return true;
        }

    }
}
