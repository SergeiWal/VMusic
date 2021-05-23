using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using MaterialDesignThemes.Wpf;
using VMusic.Commands;
using VMusic.Controller.Client.PagesController;
using VMusic.Controller.Client.Player;
using VMusic.Models;
using VMusic.Repository;
using VMusic.Views.Autorization;
using VMusic.Views.Client;

namespace VMusic.ViewModels.Client
{
    class ClientMainViewModel: BaseWindowViewModel
    {

        public static string LIKE_SONG_LIST_NAME = "Избраное";

        private User user;
        private string findSongString = "";

        private PackIconKind playStopButton = PackIconKind.Play;
        private PackIconKind volumeButton = PackIconKind.VolumeHigh;

        public Player Player { get; set; }
        private SongViewModel currentSong;

        private PageDispatcher pageDispatcher;
       
        private UnitOfWork dbWorker;

        public ClientMainViewModel(User user)
        {
            this.user = user;
            dbWorker = new UnitOfWork();
            Player = new Player();
            Player.PropertyChanged += OnSessionSongPropertyChanged;
            Player.PropertyChanged += OnIsButtonsPropertyChanged;


            pageDispatcher = new PageDispatcher();
            pageDispatcher.HomePage.DataContext = ViewModelCreator.CreateHomePageViewModel(Player);
            pageDispatcher.CreatePlaylistPage.DataContext = ViewModelCreator.CreateAddPlaylistPageViewModel(user,
                (PlaylistsPageViewModel)pageDispatcher.PlaylistsPage.DataContext, OnPlaylistCreateOrDeletePropertyChanged);
            pageDispatcher.SettingPage.DataContext = ViewModelCreator.CreateSettingViewModel(user, OnSettingPropertyChanged);
            pageDispatcher.TopMusicPage.DataContext = ViewModelCreator.CreateTopMusicViewModel(Player);
        }

        public string FindSongString
        {
            get => findSongString;
            set
            {
                findSongString = value;
                OnPropertyChanged("FindSongString");
            }
        }

        public Page CurrentPage
        {
            get => pageDispatcher.CurrentPage;
            set
            {
                pageDispatcher.CurrentPage = value;
                OnPropertyChanged("CurrentPage");
            }
        }

        public SongViewModel CurrentSong
        {
            get => currentSong;
            set
            {
                currentSong = value;
                OnPropertyChanged("CurrentSong");
            }
        }

        public PackIconKind PlayStopButton
        {
            get => playStopButton;
            set
            {
                playStopButton = value;
                OnPropertyChanged("PlayStopButton");
            }
        }

        public PackIconKind VolumeButton
        {
            get => volumeButton;
            set
            {
                volumeButton = value;
                OnPropertyChanged("VolumeButton");
            }
        }


        private Command switchToHomePage;
        private Command switchToCreatePlaylistPage;
        private Command switchToSettingPage;
        private Command switchToTopMusicPage;
        private Command switchToPlaylistsPage;
        private Command switchToCurrentSongList;
        private Command switchToGenrePage;
        private Command stopAndPlay;
        private Command likeSong;
        private Command nextSong;
        private Command prevSong;
        private Command volumeOnOff;
        private Command findSong;

        public Command SwitchToHomePage
        {
            get
            {
                return switchToHomePage??(switchToHomePage = new Command((obj) =>
                {
                    CurrentPage = pageDispatcher.HomePage;
                }));
            }
        }

        public Command SwitchToCreatePlaylistPage
        {
            get
            {
                return switchToCreatePlaylistPage ?? (switchToCreatePlaylistPage = new Command((obj) =>
                {
                    CurrentPage = pageDispatcher.CreatePlaylistPage;
                }));
            }
        }

        public Command SwitchToSettingPage
        {
            get
            {
                return  switchToSettingPage ??(switchToSettingPage = new Command((obj) =>
                {
                    CurrentPage = pageDispatcher.SettingPage;
                }));
            }
        }

        public Command SwitchToTopMusicPAge
        {
            get
            {
                return switchToTopMusicPage ?? (switchToTopMusicPage = new Command((obj) =>
                {
                    CurrentPage = pageDispatcher.TopMusicPage;
                }));
            }
        }

        public Command SwitchToPlaylistsPage
        {
            get
            {
                return switchToPlaylistsPage ?? (switchToPlaylistsPage = new Command((obj) =>
                {
                    pageDispatcher.PlaylistsPage.DataContext =
                        ViewModelCreator.CreatePlaylistsPageViewModel(user, OnPlaylistPropertyChanged);
                    CurrentPage = pageDispatcher.PlaylistsPage;
                }));
            }
        }

        public Command SwitchToCurrentSongList
        {
            get
            {
                return switchToCurrentSongList ?? (switchToCurrentSongList = new Command((obj) =>
                {
                    if (Player.CurrentPlaylist!=null)
                    {
                        pageDispatcher.CurrentSongListPage.DataContext = new CurrentSongListViewModel(Player);
                    }

                    CurrentPage = pageDispatcher.CurrentSongListPage;
                }));
            }
        }

        public Command SwitchToGenrePage
        {
            get
            {
                return switchToGenrePage ?? (switchToGenrePage = new Command((obj) =>
                {
                    pageDispatcher.GenrePage.DataContext = ViewModelCreator.CreateGenreViewModel(OnGenrePropertyChanged);
                    CurrentPage = pageDispatcher.GenrePage;
                }));
            }
        }

        public Command FindSong
        {
            get
            {
                return findSong ?? (findSong = new Command((obj) =>
                {
                    if (!string.IsNullOrEmpty(FindSongString))
                    {
                        pageDispatcher.FindSongPage.DataContext = new FindSongViewModel(FindSongString, Player);
                        CurrentPage = pageDispatcher.FindSongPage;
                    }
                }));
            }
        }

        public Command PrevSong
        {
            get
            {
                return prevSong ?? (prevSong = new Command((obj) =>
                {
                    if (CurrentSong != null)
                    {
                        Player.Prev();
                        Player.StartPlay(CurrentSong.Source);
                    }
                }));
            }
        }

        public Command StopAndPlay
        {
            get
            {
                return stopAndPlay ?? (stopAndPlay = new Command((obj) =>
                {
                    if (Player.IsPlayed)
                    {
                        Player.Stop();
                    }
                    else if(currentSong != null)
                    {
                       Player.Play();
                    }
                }));
            }
        }

        public Command NextSong
        {
            get
            {
                return nextSong ?? (nextSong = new Command((obj) =>
                {
                    if (CurrentSong != null)
                    {
                        Player.Next();
                        Player.StartPlay(CurrentSong.Source);
                    }
                    
                }));
            }
        }

        public Command LikeSong
        {
            get
            {
                return likeSong ?? (likeSong = new Command((obj) =>
                {
                    if (CurrentSong != null)
                    {
                        if (!IsHasLikeSongList())
                        {
                            CreateLikeSongList();
                        }
                       
                        AddLikeSongInList();
                    }
                }));
            }
        }

        public Command VolumeOnOff
        {
            get
            {
                return volumeOnOff ?? (volumeOnOff = new Command((obj) =>
                {
                    if (Player.Volume > 0)
                    {
                        Player.Volume = 0;
                    }
                    else
                    {
                        Player.Volume = 0.5;
                    }
                }));
            }
        }


        private void OnSessionSongPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "CurrentSong")
            {
                this.CurrentSong = Player.CurrentSong;
                if (this.CurrentSong != null)
                {
                    Player.StartPlay(CurrentSong.Source);
                }
            }
        }

        private void OnSettingPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsExit")
            {
                Login login = new Login();
                if (Player.IsPlayed)
                {
                    Player.Stop();
                }
                Close?.Invoke();
                login.Show();
            }
        }

        private void OnPlaylistPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedPlaylist")
            {
                var playlistsViewModel = pageDispatcher.PlaylistsPage.DataContext as PlaylistsPageViewModel;
                pageDispatcher.SinglePlaylistPage.DataContext =
                    ViewModelCreator.CreateSinglePlaylistViewModel
                        (playlistsViewModel.SelectedPlaylist, Player, user, OnPlaylistUpdatePropertyChanged);
                CurrentPage = pageDispatcher.SinglePlaylistPage;
            }
        }

        private void OnPlaylistUpdatePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsUpdate")
            {
                var playlistsViewModel = pageDispatcher.PlaylistsPage.DataContext as PlaylistsPageViewModel;
                pageDispatcher.UpdatePlaylistPage.DataContext = ViewModelCreator.CreateUpdatePlaylistViewModel
                    (playlistsViewModel.SelectedPlaylist, OnPlaylistCreateOrDeletePropertyChanged);
                CurrentPage = pageDispatcher.UpdatePlaylistPage;
            }
        }

        private void OnPlaylistCreateOrDeletePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsFinish")
            {
               SwitchToPlaylistsPage.Execute(new object());
            }
        }


        private void OnIsButtonsPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsPlayed")
            {
                PlayStopButton = Player.IsPlayed == true ? PackIconKind.Stop : PackIconKind.Play;
            }
            if (e.PropertyName == "Volume")
            {
                VolumeButton = Player.Volume == 0? PackIconKind.VolumeLow : PackIconKind.VolumeHigh;
            }
        }

        private void OnGenrePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedGenre")
            {
                GenreViewModel genreViewModel = pageDispatcher.GenrePage.DataContext as GenreViewModel;
                if (genreViewModel != null)
                {
                    CurrentPage = PageDispatcher.CreateCurrentPage(genreViewModel.SelectedGenre, Player);
                }
            }
        }


        private bool IsHasLikeSongList()
        {
            var plist = dbWorker.Playlist.GetByPredicate(p=>p.UserId ==user.Id && p.Name == LIKE_SONG_LIST_NAME);
            return plist != null;
        }

        private void CreateLikeSongList()
        {
            Playlist playlist = new Playlist()
            {
                Name = LIKE_SONG_LIST_NAME,
                UserId = user.Id
            };
            dbWorker.Playlist.Create(playlist);
            dbWorker.Save();
            PlaylistsUpdate(playlist);
        }

        private void AddLikeSongInList()
        {
            var playlist = dbWorker.Playlist.GetByPredicate(p => p.UserId == user.Id && p.Name == LIKE_SONG_LIST_NAME);
            var song = dbWorker.Songs.GetById(CurrentSong.Id);
            var songFromList = playlist.Songs.FirstOrDefault(s => s.Id == CurrentSong.Id);
            if (song != songFromList)
            {
                playlist.Songs.Add(song);
                dbWorker.Save();
                SongRatingUp();
            }
        }

        private void SongRatingUp()
        {
            ++CurrentSong.Rating;
            var song = dbWorker.Songs.GetById(CurrentSong.Id);
            dbWorker.Songs.RatingUpdate(song);
            dbWorker.Save();
        }

        private void PlaylistsUpdate(Playlist playlist)
        {
            var obj = pageDispatcher.PlaylistsPage.DataContext as PlaylistsPageViewModel;
            if (obj != null)
            {
                obj.PlaylistsDataUpdate(playlist);
            }
        }

    }
}
