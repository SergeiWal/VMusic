using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMusic.Models;

namespace VMusic.Converters
{
    static class GenreConverter
    {
        public static string GenreToString(MusicGenre genre)
        {
            switch (genre)
            {
                case MusicGenre.CLASSIC:
                    return "Инструментальная";
                case MusicGenre.FOLK:
                    return "Фолк";
                case MusicGenre.INDI:
                    return "Инди";
                case MusicGenre.JAZZ:
                    return "Джаз";
                case MusicGenre.POP:
                    return "Поп";
                case MusicGenre.ROCK:
                    return "Рок";
                case MusicGenre.RAP:
                    return "Реп";
                default:
                    return "";
            }
        }

        public static MusicGenre StringToGenre(string genre)
        {
            switch (genre)
            {
                case "Инструментальная":
                    return MusicGenre.CLASSIC;
                case "Фолк":
                    return MusicGenre.FOLK;
                case "Инди":
                    return MusicGenre.INDI;
                case "Джаз":
                    return MusicGenre.JAZZ;
                case "Поп":
                    return MusicGenre.POP;
                case "Рок":
                    return MusicGenre.ROCK;
                case "Реп":
                    return MusicGenre.RAP;
                default:
                    return MusicGenre.POP;
            }
        }
    }
}
