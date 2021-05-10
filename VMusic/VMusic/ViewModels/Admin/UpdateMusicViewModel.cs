using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Controls;
using Microsoft.Win32;
using VMusic.Commands;
using VMusic.Converters;
using VMusic.Repository;

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
        private string resultString = "";

        private UnitOfWork dbWorker;
        private string path;
        private byte[] img;

        public UpdateMusicViewModel(SongViewModel song, ObservableCollection<SongViewModel> localSongList)
        {
            this.song = song;
            Name = song.Name;
            Album = song.Album;
            Author = song.Author;
            path = song.Source;
            img = ImageConverter.BitmapImageToArray(song.Image);
            dbWorker = new UnitOfWork();
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
                            var dbSong = dbWorker.Songs.GetById(song.Id);
                            dbSong.Album = Album;
                            dbSong.Author = Author;
                            dbSong.Genre = GenreConverter.StringToGenre(Genre.Text);
                            dbSong.Name = Name;
                            dbSong.Image = img;
                            dbSong.Source = path;
                            dbWorker.Save();
                            LocalDataUpdate();
                            IsFinish = IsFinish != true;
                        }
                        catch (SqlException e)
                        {
                            ResultString = "Ошибка при обращении к базе данных!!!";
                        }
                    }
                    else
                    {
                        ResultString = "Заполнены не все поля!!!";
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
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.Filter = "All Supported Audio | *.mp3; *.wma | MP3s | *.mp3 | WMAs | *.wma";
                    openFileDialog.InitialDirectory = @"D:\";

                    if (openFileDialog.ShowDialog() == true)
                    {
                        path = openFileDialog.FileName;
                    }
                }));
            }
        }

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
                        img = System.IO.File.ReadAllBytes(openFileDialog.FileName);
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
                   && !string.IsNullOrEmpty(path);
        }

    }
}
