using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMusic.Controller.Admin.Messager
{
    static class AddMusicMessager
    {
        public static string ADD_MUSIC_SUCCESS = "Добавлено успешно!!!";
        public static string SONG_HAS_IN_DB  = "Трек уже содержиться в базе!!!";
        public static string DB_ERROR = "Ошибка при обращении к базе данных!!!";
        public static string FIELDS_EMPTY = "Заполнены не все поля!!!";
        public static string SIZE_IMAGE_ERROR = "Ошибка при добавлении изображения !!!";
        public static string SAVE_SONG_ERROR = "Не удалось сохранить трек !!!";
    }
}
