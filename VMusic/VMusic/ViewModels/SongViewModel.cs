using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;
using VMusic.Converters;
using VMusic.Models;

namespace VMusic.ViewModels
{
    class SongViewModel: BaseViewModel
    {
        private Song song;

        public SongViewModel(Song song)
        {
            this.song = song;
        }

        public int Id
        {
            get => song.Id;
        }

        public string Name
        {
            get => song.Name;
            set
            {
                song.Name = value;
                OnPropertyChanged("Name");
            }
        }

        public string Author
        {
            get => song.Author;
            set
            {
                song.Author = value;
                OnPropertyChanged("Author");
            }
        }

        public string Album
        {
            get => song.Album;
            set
            {
                song.Album = value;
                OnPropertyChanged("Album");
            }
        }

        public int Rating
        {
            get => song.Rating;
            set
            {
                song.Rating = value;
                OnPropertyChanged("Rating");
            }
        }

        public string Genre
        {
            get => GenreConverter.GenreToString(song.Genre);
            set
            {
                song.Genre =  GenreConverter.StringToGenre(value);
                OnPropertyChanged("Genre");
            }
        }

        public string Source
        {
            get => song.Source;
            set
            {
                song.Source = value;
                OnPropertyChanged("Source");
            }
        }

        public BitmapImage Image
        {
            get => ImageConverter.GetImageByByteArray(song.Image);
            set
            {
                song.Image = ImageConverter.BitmapImageToArray(value);
                OnPropertyChanged("Image");
            }
        }

    }
} 
