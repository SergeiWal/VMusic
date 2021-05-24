using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Windows.Controls;
using VMusic.Commands;
using VMusic.Controller.Admin;
using VMusic.Controller.Admin.Messager;
using VMusic.Converters;
using VMusic.Models;

namespace VMusic.ViewModels.Admin
{
    class AddMusicViewModel: BaseViewModel
    {
        private ObservableCollection<SongViewModel> LocalSongList { get; set; }

        private string name;
        private string author;
        private string album;
        private TextBlock genre; 
        private string resultString = "";

        private AddUpdateMusicController controller;
        private string path;
        private byte[] img;

        public AddMusicViewModel(ObservableCollection<SongViewModel> localSongList)
        {
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


        private Command addMusic;
        private Command addSource;
        private Command addImage;

        public Command AddMusic
        {
            get
            {
                return addMusic ?? (addMusic = new Command((obj) =>
                {
                    if (IsFieldsNotEmpty())
                    {
                        try
                        {
                            Song song = new Song()
                            {
                                Name = this.Name,
                                Album = this.Album,
                                Author = this.Author,
                                Genre = GenreConverter.StringToGenre(this.Genre.Text),
                                Image = this.img,
                                Rating = 0
                            };

                            if (!IsRepeat(new SongViewModel(song)))
                            {
                                string source = controller.CopySongToLocalFileRepository(path, song);
                                if ( source != null)
                                {
                                    song.Source = source;
                                    controller.AddSongToDB(song);
                                    AddSongToLocalCollection(controller.GetSongFromDb(song.Id));
                                    ResultString = AddMusicMessager.ADD_MUSIC_SUCCESS;
                                    ClearField();
                                }
                            }
                            else
                            {
                                ResultString = AddMusicMessager.SONG_HAS_IN_DB;
                            }

                        }
                        catch(SqlException e)
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
                return  addSource ??(addSource = new Command((obj) =>
                {
                    path = controller.GetSongPath();
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

                    if (imgBuf!=null)
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

        private bool IsFieldsNotEmpty()
        {
            return !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Author) && !string.IsNullOrEmpty(Album) &&
                   !string.IsNullOrEmpty(Genre.Text) && !string.IsNullOrEmpty(path);
        }

        private bool IsRepeat(SongViewModel song)
        {
            return LocalSongList.Contains(song);
        }

        private void AddSongToLocalCollection(Song song)
        {
            LocalSongList.Add(new SongViewModel(song) { Index = LocalSongList.Count + 1 });
        }

        private void ClearField()
        {
            Name = string.Empty;
            Author = string.Empty;
            Album = string.Empty;
        }

    }
}
