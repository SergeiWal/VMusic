using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMusic.Controller.Client.Messagers
{
    static class SettingMessager
    {
        public static string CHANDE_PASS_SUCCESS = "Пароль изменён успешно ...";
        public static string OLD_PASS_FAILED = "Текущий пароль не верен ...";
        public static string PASS_NOT_EQUAL = "Пароли не совпадают ...";
        public static string FIELDS_EMPTY = "Заполнены не все поля ...";
        public static string USER_IS_ADMIN = "Невозможно удалить аккаунт админа ...";
        public static string DELETE_USER_SUCCES = "Ваш аккаунт удалён ...";
    }
}
