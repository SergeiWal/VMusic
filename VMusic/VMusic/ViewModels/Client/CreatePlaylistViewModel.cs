using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media.Imaging;
using VMusic.Commands;
using VMusic.Controller.Client;
using VMusic.Controller.Client.Messagers;
using VMusic.Converters;
using VMusic.Models;

namespace VMusic.ViewModels.Client
{
    class CreatePlaylistViewModel: BaseViewModel
    {
        private bool isFinish = false;

        private string findSongName = String.Empty;
        private string infoMessage = String.Empty;
        private SongViewModel selectedSong;
        private CreatePlaylistController controller;
        private Playlist playlist;
        private int itemCount = 0;
        public ObservableCollection<SongViewModel> SongLocalList { get; set; }

        public CreatePlaylistViewModel(User user)
        {
            SongLocalList = new ObservableCollection<SongViewModel>();
            controller = new CreatePlaylistController();
            playlist = new Playlist()
            {
                UserId = user.Id
            };
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
                isFinish = value;
                OnPropertyChanged("IsFinish");
            }
        }

        private Command addImage;
        private Command savePlaylist;
        private Command findSong;
        private Command addSong;

        public Command AddImage
        {
            get
            {
                return  addImage ?? (addImage = new Command((obj) =>
                {
                    var imgBuf = controller.GetImage();
                    if (imgBuf != null)
                    {
                        PlaylistImage = imgBuf;
                        InfoMessage = CreatePlaylistMessager.ADD_IMAGE_SUCCESS;
                    }
                    else
                    {
                        InfoMessage = CreatePlaylistMessager.SIZE_IMAGE_ERROR;
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
                        if (controller.AddPlaylist(playlist))
                        {
                            IsFinish = IsFinish != true;
                            InfoMessage = CreatePlaylistMessager.CREATE_PLAYLIST_SUCCESS;
                        }
                        else
                        {
                            InfoMessage = CreatePlaylistMessager.PLAYLIST_REPEAT;
                        }
                    }
                    else
                    {
                        InfoMessage = CreatePlaylistMessager.FIELDS_NOT_EMPTY;
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
                        itemCount = 0;
                        var songs = controller.FindSong(FindSongName)
                            .Select(s => new SongViewModel(s){Index = ++itemCount});
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
                return addSong ??(addSong = new Command((obj) =>
                {
                    if (SelectedSong != null)
                    {
                        AddSongToPlaylist(SelectedSong.song);
                    }
                    else
                    {
                        InfoMessage = CreatePlaylistMessager.NOT_SELECT_SONG;
                    }
                }));
            }
        }

        private void AddSongToPlaylist(Song song)
        {
            if (IsSongNotRepeat(song))
            {
                playlist.Songs.Add(song);
                InfoMessage = CreatePlaylistMessager.ADD_SONG_SUCCESS;
            }
            else
            {
                InfoMessage = CreatePlaylistMessager.SONG_REPEAT;
            }
        }

        private bool IsFieldsNotEmpty()
        {
            return !string.IsNullOrEmpty(PlaylistName) && PlaylistImage != null;
        }

        private bool IsSongNotRepeat(Song song)
        {
            var obj = playlist.Songs.FirstOrDefault(s =>
                s.Name == song.Name && s.Author == song.Author && s.Album == song.Album);
            return obj == null;
        }
    }
}
