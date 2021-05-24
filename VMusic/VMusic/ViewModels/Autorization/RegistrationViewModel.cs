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
        private const int PASSWORD_MIN_LENGTH = 4;
        private const string DUPLICATE_USER_DATA = "Пользаватель с таким именем или логином уже существует!!!";
        private const string PASSWORDS_NOT_EQUEL = "Пароли не совпадают!!!";
        private const string MIN_PASSWORD_LENGTH_ERROR = "Минимальная длина пароля 4 символа!!!";
        private const string FIELDS_EMPTY = "Заполнены не все поля!!!";


        private UnitOfWork dbWorker;

        private string name;
        private string login;
        private string password;
        private string repeatPassword;

        private string infoMessage = string.Empty;
        private bool isPasswordValid = false;

        public RegistrationViewModel()
        {
            dbWorker = new UnitOfWork();
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
                if (value.Length >= PASSWORD_MIN_LENGTH)
                {
                    password = PasswordHasher.GetHash(value);
                    isPasswordValid = true;
                    InfoMessage = string.Empty;
                }
                else
                {
                    isPasswordValid = false;
                    InfoMessage = MIN_PASSWORD_LENGTH_ERROR;
                }
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
                    SwitchTo(new Login());
                }));
            }
        }

        public Command SwitchToLoginAsAdminCommand
        {
            get
            {
                return switchToLoginAsAdminCommand ?? (switchToLoginAsAdminCommand = new Command((obj) => {
                    SwitchTo(new LoginAsAdmin());
                }));
            }
        }


        private void Registration()
        {
            if (IsFieldNotEmpty() && isPasswordValid)
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
                        dbWorker.Users.Create(user);
                        dbWorker.Save();
                        SwitchTo(new Login());
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
            IEnumerable<User> users = dbWorker.Users.GetAllObject().Where(s => s.Name == Name || s.Login == Login);
            if (users.Count() > 0)
            {
                return false;
            }
            return true;
        }

    }
}
