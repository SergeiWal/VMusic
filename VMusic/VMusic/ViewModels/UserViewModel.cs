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

        public bool IsAdmin
        {
            get => user.IsAdmin;
            set
            {
                user.IsAdmin = value;
                OnPropertyChanged("IsAdmin");
            }
        }

        public bool IsBlocked
        {
            get => user.IsBlocked;
            set
            {
                user.IsBlocked = value;
                OnPropertyChanged("IsBlocked");
            }
        }
    }
}
