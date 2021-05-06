using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using VMusic.Commands;
using VMusic.Converters;
using VMusic.Models;
using VMusic.Repository;

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

        private UnitOfWork dbWorker;
        private string path;
        private byte[] img;

        public AddMusicViewModel(ObservableCollection<SongViewModel> localSongList)
        {
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
                                Rating = 0,
                                Source = this.path
                            };

                            if (!IsRepeat(new SongViewModel(song)))
                            {
                                dbWorker.Songs.Create(song);
                                dbWorker.Save();
                                AddSongToLocalCollection(song);
                                ResultString = "Добавлено успешно!!!";
                                ClearField();
                            }
                            else
                            {
                                ResultString = "Трек уже содержиться в базе!!!";
                            }

                        }
                        catch(SqlException e)
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
                return  addSource ??(addSource = new Command((obj) =>
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

        private bool IsFieldsNotEmpty()
        {
            return !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Author) && !string.IsNullOrEmpty(Album) &&
                   !string.IsNullOrEmpty(Genre.Text) && !string.IsNullOrEmpty(path);
        }

        private void AddSongToLocalCollection(Song song)
        {
            var song_db = dbWorker.Songs.GetById(song.Id);
            LocalSongList.Add(new SongViewModel(song_db));
        }

        private bool IsRepeat(SongViewModel song)
        {
            return LocalSongList.Contains(song);
        }

        private void ClearField()
        {
            Name = "";
            Author = "";
            Album = "";
        }

    }
}
