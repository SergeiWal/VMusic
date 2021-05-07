using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMusic.Commands;
using VMusic.Hasher;
using VMusic.Models;
using VMusic.Repository;

namespace VMusic.ViewModels.Client
{
    class SettingViewModel: BaseViewModel
    {

        private const string CHANDE_PASS_SUCCESS = "Пароль изменён успешно ...";
        private const string OLD_PASS_FAILED = "Текущий пароль не верен ...";
        private const string PASS_NOT_EQUAL = "Пароли не совпадают ...";
        private const string FIELDS_EMPTY = "Заполнены не все поля ...";
        private const string USER_IS_ADMIN = "Невозможно удалить аккаунт админа ...";
        private const string DELETE_USER_SUCCES = "Ваш аккаунт удалён ...";

        private UnitOfWork dbWorker;

        private string resultString = "";
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string RepeatPassword { get; set; }

        public UserViewModel User { get; set; }

        public SettingViewModel(User user)
        {
            dbWorker = new UnitOfWork();
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

        private Command changePasswordCommand;
        private Command deleteUserCommand;

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
                                ChangePassword();
                                ResultString = CHANDE_PASS_SUCCESS;
                            }
                            else
                            {
                                ResultString = OLD_PASS_FAILED;
                            }
                        }
                        else
                        {
                            ResultString = PASS_NOT_EQUAL;
                        }
                    }
                    else
                    {
                        ResultString =  FIELDS_EMPTY;
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
                    DeleteUser();
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

        private void ChangePassword()
        {
            var user = dbWorker.Users.GetById(User.Id);
            if (user != null)
            {
                user.Password = PasswordHasher.GetHash(NewPassword);
                dbWorker.Save();
            }
        }

        private void DeleteUser()
        {
            var user = dbWorker.Users.GetById(User.Id);
            if (user != null)
            {
                if (!user.IsAdmin)
                {
                    dbWorker.Users.Delete(user.Id);
                    dbWorker.Save();
                    ResultString = DELETE_USER_SUCCES;
                }
                else
                {
                    ResultString = USER_IS_ADMIN;
                }
            }
        }
    }
}
