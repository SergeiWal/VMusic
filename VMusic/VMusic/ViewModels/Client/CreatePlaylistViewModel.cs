using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using VMusic.Commands;
using VMusic.Converters;
using VMusic.Models;
using VMusic.Repository;

namespace VMusic.ViewModels.Client
{
    class CreatePlaylistViewModel: BaseViewModel
    {
        private string findSongName = "";
        private Playlist playlist;
        private SongRepository songRepository;
        public ObservableCollection<SongViewModel> SongLocalList { get; set; }

        public CreatePlaylistViewModel()
        {
            SongLocalList = new ObservableCollection<SongViewModel>();
            songRepository = new SongRepository();
            playlist = new Playlist();
        }

        public string PlaylistName
        {
            get => playlist.Name;
            set
            {
                playlist.Name = value;
                OnPropertyChanged("PlaylistName");
            }
        }

        public string FindSongName
        {
            get => findSongName;
            set
            {
                findSongName = value;
                OnPropertyChanged("FindSongName");
            }
        }

        public BitmapImage PlaylistImage
        {
            get => ImageConverter.GetImageByByteArray(playlist.Image);
            set
            {
                playlist.Image = ImageConverter.BitmapImageToArray(value);
                OnPropertyChanged("PlaylistImage");
            }
        }

        private Command addImage;
        private Command savePlaylist;
        private Command findSong;

        public Command AddImage
        {
            get
            {
                return  addImage ?? (addImage = new Command((obj) =>
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.Filter = "Image files (*.png;*.jpg)|*.png;*.jpg|All files (*.*)|*.*";
                    openFileDialog.InitialDirectory = @"D:\";

                    if (openFileDialog.ShowDialog() == true)
                    {
                        playlist.Image = System.IO.File.ReadAllBytes(openFileDialog.FileName);
                    }
                }));
            }
        }

        public Command SavePlaylist
        {
            get
            {
                return savePlaylist ?? (savePlaylist = new Command((obj) =>
                {
                    MessageBox.Show(playlist.Name);
                }));
            }
        }

        public Command FindSong
        {
            get
            {
                return findSong ?? (findSong = new Command((obj) =>
                {
                    if (!string.IsNullOrEmpty(FindSongName))
                    {
                        var songs = songRepository.GetByName(FindSongName)
                            .Select(s => new SongViewModel(s));
                        SongLocalList.Clear();

                        foreach (var c in songs)
                        {
                            SongLocalList.Add(c);   
                        }
                    }
                }));
            }
        }

    }
}
