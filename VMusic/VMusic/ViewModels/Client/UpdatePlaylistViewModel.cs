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
    class UpdatePlaylistViewModel: BaseViewModel
    {
        
        private bool isFinish = false;
        private string findSongName = string.Empty;
        private string infoMessage = string.Empty;
        private SongViewModel selectedSong;
        private Playlist playlist;
        private UpdatePlaylistController controller;
        private int itemCount = 0;
        public ObservableCollection<SongViewModel> SongLocalList { get; set; }

        public UpdatePlaylistViewModel(PlaylistViewModel selectedPlaylist)
        {
            controller = new UpdatePlaylistController();
            playlist = controller.GetPlaylist(selectedPlaylist.Id);
            SongLocalList = new ObservableCollection<SongViewModel>(playlist.Songs.Select(s=>new SongViewModel(s)
            {
                Index = ++itemCount
            }));
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

                    var img = controller.GetImage();

                    if (img!=null)
                    {
                        PlaylistImage = img;
                    }
                    else
                    {
                        InfoMessage = UpdatePlaylistMessager.SIZE_IMAGE_ERROR;
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
                        controller.UpdatePlaylist(playlist);
                        InfoMessage = UpdatePlaylistMessager.UPDATE_PLAYLIST_SUCCESS;
                        IsFinish = IsFinish != true;
                    }
                    else
                    {
                        InfoMessage = UpdatePlaylistMessager.FIELDS_NOT_EMPTY;
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
                   controller.DeletePlaylist(playlist.Id);
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
                        if (!IsSongNotRepeat(SelectedSong.song))
                        {
                            controller.DeleteSongFromPlaylist(SelectedSong.song, playlist.Id);
                            SongLocalList.Remove(SelectedSong);
                            InfoMessage = UpdatePlaylistMessager.SONG_DELETE;
                        }
                        else
                        {
                            InfoMessage = UpdatePlaylistMessager.SONG_NOT_FOUND_IN_PLAYLIST;
                        }
                    }
                    else
                    {
                        InfoMessage = UpdatePlaylistMessager.NOT_SELECT_SONG;
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
                        var songs = controller.FindSong(findSongName)
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
                return addSong ?? (addSong = new Command((obj) =>
                {
                    if (SelectedSong != null)
                    {
                        AddSongToPlaylist(SelectedSong.song);
                    }
                    else
                    {
                        InfoMessage = UpdatePlaylistMessager.NOT_SELECT_SONG;
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
                    itemCount = 0;
                    var songs = playlist.Songs.Select(s => new SongViewModel(s){Index = ++itemCount}); ;
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
                InfoMessage = UpdatePlaylistMessager.ADD_SONG_SUCCESS;
            }
            else
            {
                InfoMessage = UpdatePlaylistMessager.SONG_REPEAT;
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

