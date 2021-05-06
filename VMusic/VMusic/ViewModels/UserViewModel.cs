using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMusic.Models;

namespace VMusic.ViewModels
{
    class UserViewModel: BaseViewModel
    {
        private const string ADMIN_ROLE = "Admin";
        private const string USER_ROLE = "User";
        private const string ACTIVITY_USER = "Активен";
        private const string BLOCKED_USER = "Заблокирован";

        private User user;

        public UserViewModel(User user)
        {
            this.user = user;
        }

        public int Id
        {
            get => user.Id;
        }

        public string Name
        {
            get => user.Name;
            set
            {
                user.Name = value;
                OnPropertyChanged("Name");
            }
        }

        public string Login
        {
            get => user.Login;
            set
            {
                user.Login = value;
                OnPropertyChanged("Login");
            }
        }

        public string Password
        {
            get => user.Password;
            set
            {
                user.Password = value;
                OnPropertyChanged("Password");
            }
        }

        public string IsAdmin
        {
            get => BoolRoleToString(user.IsAdmin);
            set
            {
                user.IsAdmin = StrRoleToBool(value);
                OnPropertyChanged("IsAdmin");
            }
        }

        public string IsBlocked
        {
            get => BoolStatusToStr(user.IsBlocked);
            set
            {
                user.IsBlocked = StrStatusToBool(value);
                OnPropertyChanged("IsBlocked");
            }
        }

        public static  string BoolRoleToString(bool value)
        {
            if (value)
            {
                return ADMIN_ROLE;
            }
            else
            {
                return USER_ROLE;
            }
        }

        public static bool StrRoleToBool(string str)
        {
            if (str == ADMIN_ROLE)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string BoolStatusToStr(bool value)
        {
            if (value)
            {
                return BLOCKED_USER;
            }
            else
            {
                return ACTIVITY_USER;
            }
        }

        public static bool StrStatusToBool(string str)
        {
            if (str == BLOCKED_USER)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
