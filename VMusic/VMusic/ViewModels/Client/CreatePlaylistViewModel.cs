using System.Collections.ObjectModel;
using System.Linq;
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
        private const string FIELDS_NOT_EMPTY = "Заполнены не все данные ...";
        private const string CREATE_PLAYLIST_SUCCESS = "Плэйлист сохранён ...";
        private const string NOT_SELECT_SONG = "Выбирите трек ...";
        private const string ADD_SONG_SUCCESS = "Трек добавлен ...";
        private const string SONG_REPEAT = "Трек был уже добавлен ...";
        private const string PLAYLIST_REPEAT = "Плэйлист с таким именем существует ...";


        private bool isFinish = false;

        private string findSongName = "";
        private string infoMessage = "";
        private SongViewModel selectedSong;
        private PlaylistsPageViewModel playlistsPageViewModel;
        private Playlist playlist;
        private UnitOfWork dbWorker;
        public ObservableCollection<SongViewModel> SongLocalList { get; set; }

        public CreatePlaylistViewModel(PlaylistsPageViewModel playlistsPageViewModel, User user)
        {
            this.playlistsPageViewModel = playlistsPageViewModel;
            SongLocalList = new ObservableCollection<SongViewModel>();
            dbWorker = new UnitOfWork();
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
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.Filter = "Image files (*.png;*.jpg)|*.png;*.jpg|All files (*.*)|*.*";
                    openFileDialog.InitialDirectory = @"D:\";

                    if (openFileDialog.ShowDialog() == true)
                    {
                        byte[] imgBuf = System.IO.File.ReadAllBytes(openFileDialog.FileName);
                        if ((imgBuf.Length / 1024) < 1024)
                        {
                            PlaylistImage = ImageConverter.GetImageByByteArray(imgBuf);
                        }
                        else
                        {
                            InfoMessage = "Превышен допустимый размер изображения ...";
                        }
                       
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
                        AddPlaylist();
                    }
                    else
                    {
                        InfoMessage = FIELDS_NOT_EMPTY;
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
                            .Where(s=>s.Name.Contains(FindSongName) || s.Author.Contains(FindSongName) || s.Album.Contains(FindSongName))
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
                return addSong ??(addSong = new Command((obj) =>
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

        private bool IsFieldsNotEmpty()
        {
            return !string.IsNullOrEmpty(PlaylistName) && PlaylistImage != null;
        }

        private void AddPlaylist()
        {
            if (IsPlaylistNotRepeat())
            {
                dbWorker.Playlist.Create(playlist);
                dbWorker.Save();
                InfoMessage = CREATE_PLAYLIST_SUCCESS;
                IsFinish = IsFinish != true;
            }
            else
            {
                InfoMessage = PLAYLIST_REPEAT;
            }
        }

        private bool IsPlaylistNotRepeat()
        {
            var obj = dbWorker.Playlist.GetAllObject()
                .FirstOrDefault(p => p.Name == playlist.Name && p.UserId == playlist.UserId);

            return obj == null;
        }

        private bool IsSongNotRepeat(Song song)
        {
            var obj = playlist.Songs.FirstOrDefault(s =>
                s.Name == song.Name && s.Author == song.Author && s.Album == song.Album);
            return obj == null;
        }
    }
}
