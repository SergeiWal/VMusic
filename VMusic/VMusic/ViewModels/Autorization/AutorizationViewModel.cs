using System.Linq;
using System.Windows.Controls;
using VMusic.Commands;
using VMusic.Controller.Authorization.Messagers;
using VMusic.Hasher;
using VMusic.Models;
using VMusic.Repository;
using VMusic.ViewModels.Admin;
using VMusic.ViewModels.Client;
using VMusic.Views.Admin;
using VMusic.Views.Autorization;
using VMusic.Views.Client;

namespace VMusic.ViewModels.Autorization
{
    class AutorizationViewModel : BaseWindowViewModel
    {
        private UnitOfWork dbWorker;
        private string infoMessage = string.Empty;

        private string login;
        private string password;

        public AutorizationViewModel()
        {
            dbWorker = new UnitOfWork();
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

        public Command SwitchToRegistrationCommand
        {
            get
            {
                return switchToRegistrationCommand ?? (switchToRegistrationCommand = new Command((obj) => {
                    SwitchTo(new Registration());
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
                var user = dbWorker.Users.GetAllObject().FirstOrDefault(s => s.Login == Login && s.Password == passwordHash);
                if (user != null)
                {
                    if (!user.IsBlocked)
                    {
                        SwitchTo(GetClientMainWindow(user));
                    }
                    else
                    {
                        InfoMessage = AuthorizationMessager.USER_BLOCKED;
                    }
                }
                else
                {
                    InfoMessage = AuthorizationMessager.LOGIN_DATA_FAILED;
                }
            }
            else
            {
                InfoMessage = AuthorizationMessager.FIELDS_EMPTY;
            }
        }

        private void LoginAsAdminFunc()
        {
            if (IsFieldsNotEmpty())
            {
                string passwordHash = PasswordHasher.GetHash(Password);
                var admin = dbWorker.Users.GetAllObject().FirstOrDefault(s => s.Login == Login && s.Password == passwordHash);
                if (admin != null && admin.IsAdmin)
                {
                    SwitchTo(GetAdminMainWindow(admin));
                }
                else
                {
                    InfoMessage = AuthorizationMessager.LOGIN_DATA_FAILED;
                }
            }
            else
            {
                InfoMessage = AuthorizationMessager.FIELDS_EMPTY;
            }
        }

        private bool IsFieldsNotEmpty()
        {
            return !string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(Login);
        }

        //factory methods for window
        private  ClientMainWindow GetClientMainWindow(User user)
        {
            ClientMainWindow clientMainWindow = new ClientMainWindow();
            ClientMainViewModel clientMainViewModel = new ClientMainViewModel(user);
            clientMainWindow.DataContext = clientMainViewModel;
            return clientMainWindow;
        }

        private AdminMainWindow GetAdminMainWindow(User admin)
        {
            AdminMainWindow adminWindow = new AdminMainWindow();
            AdminMainViewModel adminMainViewModel = new AdminMainViewModel(admin);
            adminWindow.DataContext = adminMainViewModel;
            return adminWindow;
        }

    }
}
