using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMusic.Controller.Authorization.Messagers
{
    static class AuthorizationMessager
    {
        public static string DUPLICATE_USER_DATA = "Пользаватель с таким именем или логином уже существует!!!";
        public static string PASSWORDS_NOT_EQUEL = "Пароли не совпадают!!!";
        public static string MIN_PASSWORD_LENGTH_ERROR = "Минимальная длина пароля 4 символа!!!";
        public static string FIELDS_EMPTY = "Заполнены не все поля!!!";
        public static string LOGIN_DATA_FAILED = "Данные не верны!!!";
        public static string USER_BLOCKED = "Пользователь заблокирован!!!";
    }
}
