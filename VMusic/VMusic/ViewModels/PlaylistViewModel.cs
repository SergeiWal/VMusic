using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using VMusic.Converters;
using VMusic.Models;

namespace VMusic.ViewModels
{
    class PlaylistViewModel: BaseViewModel
    {
        private Playlist playlist;

        public PlaylistViewModel(Playlist playlist)
        {
            this.playlist = playlist;
        }

        public int Id
        {
            get => playlist.Id;
        }

        public string Name
        {
            get => playlist.Name;
            set
            {
                playlist.Name = value;
                OnPropertyChanged("Name");
            }
        }

        public int? USerId
        {
            get => playlist.UserId;
        }

        public BitmapImage Image
        {
            get => ImageConverter.GetImageByByteArray(playlist.Image);
            set
            {
                playlist.Image = ImageConverter.BitmapImageToArray(value);
                OnPropertyChanged("Image");
            }
        }
    }
}
