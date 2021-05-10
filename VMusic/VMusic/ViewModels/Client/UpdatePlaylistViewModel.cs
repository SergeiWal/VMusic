using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using VMusic.Commands;
using VMusic.Converters;
using VMusic.Models;
using VMusic.Repository;

namespace VMusic.ViewModels.Client
{
    class UpdatePlaylistViewModel: BaseViewModel
    {
        private const string FIELDS_NOT_EMPTY = "Заполнены не все данные ...";
        private const string UPDATE_PLAYLIST_SUCCESS = "Плэйлист изменён ...";
        private const string NOT_SELECT_SONG = "Выбирите трек ...";
        private const string ADD_SONG_SUCCESS = "Трек добавлен ...";
        private const string SONG_REPEAT = "Трек был уже добавлен ...";
        private const string SONG_DELETE = "Трек удалён из плэйлиста ...";
        private const string SONG_NOT_FOUND_IN_PLAYLIST = "Плэйлист не содержит данный трек ...";

        private bool isFinish = false;

        private string findSongName = "";
        private string infoMessage = "";
        private SongViewModel selectedSong;
        private Playlist playlist;
        private UnitOfWork dbWorker;
        public ObservableCollection<SongViewModel> SongLocalList { get; set; }

        public UpdatePlaylistViewModel(PlaylistViewModel selectedPlaylist)
        {
            dbWorker = new UnitOfWork();
            this.playlist = dbWorker.Playlist.GetByPredicate(p=>p.Id == selectedPlaylist.Id);
            SongLocalList = new ObservableCollection<SongViewModel>(playlist.Songs.Select(s=>new SongViewModel(s)));
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

        public SongViewModel SelectedSong
        {
            get => selectedSong;
            set
            {
                selectedSong = value;
                OnPropertyChanged("SelectedSong");
            }
        }

        public string InfoMessage
        {
            get => infoMessage;
            set
            {
                infoMessage = value;
                OnPropertyChanged("InfoMessage");
            }
        }

        public bool IsFinish
        {
            get => isFinish;
            set
            {
                isFinish= value;
                OnPropertyChanged("IsFinish");
            }
        }

        private Command addImage;
        private Command savePlaylist;
        private Command findSong;
        private Command addSong;
        private Command deleteSong;
        private Command viewCurrentSongs;
        private Command deletePlaylist;

        public Command AddImage
        {
            get
            {
                return addImage ?? (addImage = new Command((obj) =>
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.Filter = "Image files (*.png;*.jpg)|*.png;*.jpg|All files (*.*)|*.*";
                    openFileDialog.InitialDirectory = @"D:\";

                    if (openFileDialog.ShowDialog() == true)
                    {
                        PlaylistImage = ImageConverter.GetImageByByteArray(System.IO.File.ReadAllBytes(openFileDialog.FileName));
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
                    if (IsFieldsNotEmpty())
                    {
                        UpdatePlaylist();
                        IsFinish = IsFinish != true;
                    }
                    else
                    {
                        InfoMessage = FIELDS_NOT_EMPTY;
                    }
                }));
            }
        }

        public Command DeletePlaylist
        {
            get
            {
                return deletePlaylist ??(deletePlaylist = new Command((obj) =>
                {
                    dbWorker.Playlist.Delete(playlist.Id);
                    dbWorker.Save();
                    IsFinish = IsFinish != true;
                }));
            }
        }

        public Command DeleteSong
        {
            get
            {
                return deleteSong ?? (deleteSong = new Command((obj) =>
                {
                    if (SelectedSong != null)
                    {
                        DeleteSongFromPlaylist(SelectedSong.song);
                    }
                    else
                    {
                        InfoMessage = NOT_SELECT_SONG;
                    }
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
                        var songs = dbWorker.Songs.GetAllObject()
                            .Where(s => s.Name.Contains(FindSongName) || s.Author.Contains(FindSongName) || s.Album.Contains(FindSongName))
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

        public Command AddSong
        {
            get
            {
                return addSong ?? (addSong = new Command((obj) =>
                {
                    if (SelectedSong != null)
                    {
                        AddSongToPlaylist(SelectedSong.song);
                    }
                    else
                    {
                        InfoMessage = NOT_SELECT_SONG;
                    }
                }));
            }
        }

        public Command ViewCurrentSongs
        {
            get
            {
                return viewCurrentSongs ?? (viewCurrentSongs = new Command((obj) =>
                {
                    var songs = playlist.Songs.Select(s => new SongViewModel(s)); ;
                    SongLocalList.Clear();

                    foreach (var c in songs)
                    {
                        SongLocalList.Add(c);
                    }
                }));
            }
        }

        private void AddSongToPlaylist(Song song)
        {
            if (IsSongNotRepeat(song))
            {
                playlist.Songs.Add(song);
                InfoMessage = ADD_SONG_SUCCESS;
            }
            else
            {
                InfoMessage = SONG_REPEAT;
            }
        }

        private void DeleteSongFromPlaylist(Song song)
        {
            if (!IsSongNotRepeat(song))
            {
                var plist = dbWorker.Playlist.GetByPredicate(p => p.Id == playlist.Id);
                plist.Songs.Remove(song);
                SongLocalList.Remove(SelectedSong);
                InfoMessage = SONG_DELETE;
            }
            else
            {
                InfoMessage = SONG_NOT_FOUND_IN_PLAYLIST;
            }
        }

        private bool IsFieldsNotEmpty()
        {
            return !string.IsNullOrEmpty(PlaylistName) && PlaylistImage != null;
        }

        private void UpdatePlaylist()
        {
            var plist = dbWorker.Playlist.GetByPredicate(p => p.Id == playlist.Id);
            plist.Name = playlist.Name;
            plist.Image = playlist.Image;
            plist.Songs = playlist.Songs;
            dbWorker.Save();
            InfoMessage = UPDATE_PLAYLIST_SUCCESS;
        }

        private bool IsSongNotRepeat(Song song)
        {
            var obj = playlist.Songs.FirstOrDefault(s =>
                s.Name == song.Name && s.Author == song.Author && s.Album == song.Album);
            return obj == null;
        }
    }
} 

