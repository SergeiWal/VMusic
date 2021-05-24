using System.Collections.ObjectModel;
using VMusic.Commands;
using VMusic.Controller.Admin;
using VMusic.Models;

namespace VMusic.ViewModels.Admin
{
    class UserPageViewModel: BaseViewModel
    {
        private User admin;
        private UserPageController controller;
        public ObservableCollection<UserViewModel> Users { get; set; }

        private UserViewModel selectedUser = null;

        public UserPageViewModel(User admin, ObservableCollection<UserViewModel> users)
        {
            this.admin = admin;
            controller = new UserPageController();
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
                        bool isBlocked =  controller.BlockingUser(user.Id);
                        user.IsBlocked = UserViewModel.BoolStatusToStr(isBlocked);
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
                        controller.TransferAdminStatus(user.Id,admin.Id);
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
                        controller.DeleteUserFromDb(user.Id);
                        Users.Remove(user);
                        CorrectUserIndexAfterDelete();
                    }
                }));
            }
        }

        private void CorrectUserIndexAfterDelete()
        {
            int newCount = 0;
            for (int i = 0;i<Users.Count;++i)
            {
                var user = Users[i];
                user.Index = ++newCount;
            }
        }
    }
}
