using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private string name;
        private string author;
        private string album;
        private string genre;
        private string resultString = "";

        private SongRepository repository;
        private string path;
        private byte[] img;

        public AddMusicViewModel()
        {
            repository = new SongRepository();
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

        public string Genre
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
                                Genre = GenreConverter.StringToGenre(this.Genre),
                                Image = this.img,
                                Rating = 0,
                                Source = this.path
                            };
                            repository.Create(song);
                            repository.Save();
                            ResultString = "Добавлено успешно!!!";
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
                   !string.IsNullOrEmpty(Genre);
        }

    }
}
