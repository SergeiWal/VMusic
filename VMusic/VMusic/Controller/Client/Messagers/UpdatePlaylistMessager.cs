using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMusic.Controller.Client.Messagers
{
    static class UpdatePlaylistMessager
    {
        public static string FIELDS_NOT_EMPTY = "Заполнены не все данные ...";
        public static string UPDATE_PLAYLIST_SUCCESS = "Плэйлист изменён ...";
        public static string NOT_SELECT_SONG = "Выбирите трек ...";
        public static string ADD_SONG_SUCCESS = "Трек добавлен ...";
        public static string SONG_REPEAT = "Трек был уже добавлен ...";
        public static string SONG_DELETE = "Трек удалён из плэйлиста ...";
        public static string SONG_NOT_FOUND_IN_PLAYLIST = "Плэйлист не содержит данный трек ...";
        public static string SIZE_IMAGE_ERROR = "Превышен допустимый размер изображения ...";
    }
}
