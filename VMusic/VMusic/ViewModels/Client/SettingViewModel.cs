using VMusic.Commands;
using VMusic.Controller.Client;
using VMusic.Controller.Client.Messagers;
using VMusic.Hasher;
using VMusic.Models;

namespace VMusic.ViewModels.Client
{
    class SettingViewModel: BaseViewModel
    {

        private SettingController controller;

        private string resultString = string.Empty;
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string RepeatPassword { get; set; }

        public UserViewModel User { get; set; }

        private bool isExit = false;

        public SettingViewModel(User user)
        {
            controller = new SettingController();
            User = new UserViewModel(user);
        }

        
        public string ResultString
        {
            get => resultString;
            set
            {
                resultString = value;
                OnPropertyChanged("ResultString");
            }
        }

        public bool IsExit
        {
            get => isExit;
            set
            {
                isExit = value;
                OnPropertyChanged("IsExit");
            }
        }

        private Command changePasswordCommand;
        private Command deleteUserCommand;
        private Command exitCommand;

        public Command ChangePasswordCommand
        {
            get
            {
                return changePasswordCommand ?? (changePasswordCommand = new Command((obj) =>
                {
                    if (IsFieldsNotEmpty())
                    {
                        if (IsNewPasswordsEquals())
                        {
                            if (IsValidOldPassword())
                            {
                                controller.ChangePassword(User.Id, NewPassword);
                                ResultString = SettingMessager.CHANDE_PASS_SUCCESS;
                            }
                            else
                            {
                                ResultString = SettingMessager.OLD_PASS_FAILED;
                            }
                        }
                        else
                        {
                            ResultString = SettingMessager.PASS_NOT_EQUAL;
                        }
                    }
                    else
                    {
                        ResultString = SettingMessager.FIELDS_EMPTY;
                    }
                }));
            }
        }

        public Command DeleteUserCommand
        {
            get
            {
                return deleteUserCommand ??(deleteUserCommand = new Command((obj) =>
                {
                    if (controller.DeleteUser(User.Id))
                    {
                        ResultString = SettingMessager.DELETE_USER_SUCCES;
                    }
                    else
                    {
                        ResultString = SettingMessager.USER_IS_ADMIN;
                    }
                }));
            }
        }

        public Command ExitCommand
        {
            get
            {
                return exitCommand ?? (exitCommand = new Command((obj) =>
                {
                    IsExit = isExit != true;
                }));
            }
        }

        private bool IsFieldsNotEmpty()
        {
            return !string.IsNullOrEmpty(OldPassword) && !string.IsNullOrEmpty(NewPassword) &&
                   !string.IsNullOrEmpty(RepeatPassword);
        }

        private bool IsNewPasswordsEquals()
        {
            return PasswordHasher.GetHash(NewPassword) == PasswordHasher.GetHash(RepeatPassword);
        }

        private bool IsValidOldPassword()
        {
            return PasswordHasher.GetHash(OldPassword) == User.Password;
        }
    }
}
