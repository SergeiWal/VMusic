using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMusic.Controller.Client.Messagers
{
    static class CreatePlaylistMessager
    {
        public static string FIELDS_NOT_EMPTY = "Заполнены не все данные ...";
        public static string CREATE_PLAYLIST_SUCCESS = "Плэйлист сохранён ...";
        public static string NOT_SELECT_SONG = "Выбирите трек ...";
        public static string ADD_SONG_SUCCESS = "Трек добавлен ...";
        public static string SONG_REPEAT = "Трек был уже добавлен ...";
        public static string PLAYLIST_REPEAT = "Плэйлист с таким именем существует ...";
        public static string SIZE_IMAGE_ERROR = "Превышен допустимый размер изображения ...";
        public static string ADD_IMAGE_SUCCESS = "Изображение добавлено успешно ...";
    }
}

