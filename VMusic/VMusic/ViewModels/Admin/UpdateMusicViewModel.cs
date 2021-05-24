using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Controls;
using VMusic.Commands;
using VMusic.Controller.Admin;
using VMusic.Controller.Admin.Messager;
using VMusic.Converters;
using VMusic.Models;

namespace VMusic.ViewModels.Admin
{
    class UpdateMusicViewModel: BaseViewModel
    {
        private SongViewModel song;
        private ObservableCollection<SongViewModel> LocalSongList { get; set; }

        private string name;
        private string author;
        private string album;
        private TextBlock genre;
        private bool isFinish = false;
        private string resultString = string.Empty;
        private string path;
        private byte[] img;
        private bool isChangePath = false;
        private AddUpdateMusicController controller;

        public UpdateMusicViewModel(SongViewModel song, ObservableCollection<SongViewModel> localSongList)
        {
            this.song = song;
            Name = song.Name;
            Album = song.Album;
            Author = song.Author;
            path = song.Source;
            img = ImageConverter.BitmapImageToArray(song.Image);
            controller = new AddUpdateMusicController();
            LocalSongList = localSongList;
        }


        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public string Author
        {
            get => author;
            set
            {
                author = value;
                OnPropertyChanged("Author");
            }
        }

        public string Album
        {
            get => album;
            set
            {
                album = value;
                OnPropertyChanged("Album");
            }
        }

        public TextBlock Genre
        {
            get => genre;
            set
            {
                genre = value;
                OnPropertyChanged("Genre");
            }
        }

        public string ResultString
        {
            get => resultString;
            set
            {
                resultString = value;
                OnPropertyChanged("ResultString");
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


        private Command updateMusic;
        private Command addSource;
        private Command addImage;

        public Command UpdateMusic
        {
            get
            {
                return updateMusic?? (updateMusic = new Command((obj) =>
                {
                    if (IsFieldsNotEmpty())
                    {
                        try
                        {
                            Song newSongData = new Song()
                            {
                                Album = Album,
                                Author = Author,
                                Genre = GenreConverter.StringToGenre(Genre.Text),
                                Name = Name,
                                Image = img,
                                Source = path
                            };
                            if (isChangePath)
                            {
                                controller.DeleteOldFileInLocalDirectory(song.song);
                                newSongData.Source = controller.CopySongToLocalFileRepository(path, song.song);
                                song.Source = newSongData.Source;
                            }
                            controller.SongDataUpdate(song.Id, newSongData);
                            LocalDataUpdate();
                            IsFinish = IsFinish != true;
                        }
                        catch (SqlException e)
                        {
                            ResultString = AddMusicMessager.DB_ERROR;
                        }
                    }
                    else
                    {
                        ResultString = AddMusicMessager.FIELDS_EMPTY;
                    }
                }));
            }
        }

        public Command AddSource
        {
            get
            {
                return addSource ?? (addSource = new Command((obj) =>
                {
                    path = controller.GetSongPath();
                    isChangePath = true;
                }));
            }
        }

        public Command AddImage
        {
            get
            {
                return addImage ?? (addImage = new Command((obj) =>
                {
                    byte[] imgBuf = controller.GetImage();
                    if (imgBuf != null)
                    {
                        img = imgBuf;
                    }
                    else
                    {
                        ResultString = AddMusicMessager.SIZE_IMAGE_ERROR;
                    }
                }));
            }
        }

        private void LocalDataUpdate()
        {
            var localSong = LocalSongList.FirstOrDefault(s => s.Id == this.song.Id);
            if (song != null)
            {
                localSong.Album = Album;
                localSong.Author = Author;
                localSong.Genre = Genre.Text;
                localSong.Name = Name;
                localSong.Image = ImageConverter.GetImageByByteArray(img);
                localSong.Source = path;
            }
        }

        private bool IsFieldsNotEmpty()
        {
            return !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Author) && !string.IsNullOrEmpty(Album) 
                   && !string.IsNullOrEmpty(path) &&  Genre != null && !string.IsNullOrEmpty(Genre.Text);
        }

    }
}
