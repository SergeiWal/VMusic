using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMusic.Commands;
using VMusic.Models;
using VMusic.Repository;

namespace VMusic.ViewModels.Admin
{
    class UserPageViewModel: BaseViewModel
    {
        private User admin;
        private UnitOfWork dbWorker;
        public ObservableCollection<UserViewModel> Users { get; set; }

        private UserViewModel selectedUser = null;

        public UserPageViewModel(User admin, ObservableCollection<UserViewModel> users)
        {
            this.admin = admin;
            dbWorker = new UnitOfWork();
            Users = users;
        }

        public UserViewModel SelectedUser
        {
            get => selectedUser;
            set
            {
                selectedUser = value;
                OnPropertyChanged("SelectedUser");
            }
        }

        private Command blockUnblock;
        private Command transferAdminStatus;
        private Command removeUser;

        public Command BlockUnblock
        {
            get
            {
                return blockUnblock ?? (blockUnblock = new Command((obj) =>
                {
                    var user = obj as UserViewModel;
                    if (user != null)
                    {
                        var dbUser = dbWorker.Users.GetById(user.Id);
                        dbUser.IsBlocked = dbUser.IsBlocked != true;
                        dbWorker.Save();
                        user.IsBlocked = UserViewModel.BoolStatusToStr(dbUser.IsBlocked);
                    }
                }));
            }
        }

        public Command TransferAdminStatus
        {
            get
            {
                return transferAdminStatus ?? (transferAdminStatus = new Command((obj) =>
                {
                    var user = obj as UserViewModel;
                    if (user != null)
                    {
                        var dbUser = dbWorker.Users.GetById(user.Id);
                        var oldAdmin = dbWorker.Users.GetById(admin.Id);
                        dbUser.IsAdmin = true;
                        oldAdmin.IsAdmin = false;
                        dbWorker.Save();
                        user.IsAdmin = UserViewModel.BoolRoleToString(true);
                    }
                }));
            }
        }

        public Command RemoveUser
        {
            get
            {
                return removeUser ?? (removeUser = new Command((obj) =>
                {
                    var user = obj as UserViewModel;
                    if (user != null)
                    {
                        SelectedUser = null;
                        dbWorker.Users.Delete(user.Id);
                        dbWorker.Save();
                        Users.Remove(user);
                    }
                }));
            }
        }
    }
}
