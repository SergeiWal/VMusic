using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using VMusic.Commands;
using VMusic.Models;
using VMusic.Repository;
using VMusic.Views.Autorization;
using VMusic.Views.Client;

namespace VMusic.ViewModels.Client
{
    class ClientMainViewModel: BaseWindowViewModel
    {
        private User user;
        private bool isPlayed = false;
        private bool isEnded = false;
        private MediaPlayer player;
        private double progress = 0;
        private double duration;
        private DispatcherTimer timer;

        private HomePage homePage;
        private HomePageViewModel homePageViewModel;
        private CreatePlaylistPage createPlaylistPage;
        private CreatePlaylistViewModel createPlaylistViewModel;
        private SettingPage settingPage;
        private SettingViewModel settingViewModel;
        private HomePage topMusicPage;
        private PlaylistsPage playlistsPage;
        

        private Page currentPage;
        private SongViewModel currentSong;
        private SongContent songContent;
        private UnitOfWork dbWorker;

        public ClientMainViewModel(User user)
        {
            this.user = user;
            dbWorker = new UnitOfWork();
            player = new MediaPlayer();
            player.MediaEnded += endAudioCallback;

            songContent = new SongContent();
            songContent.PropertyChanged += OnSessionSongPropertyChanged;

            homePage = new HomePage();
            homePageViewModel = new HomePageViewModel(songContent);
            homePage.DataContext = homePageViewModel;
            createPlaylistPage = new CreatePlaylistPage();
            createPlaylistViewModel = new CreatePlaylistViewModel(this.user);
            createPlaylistPage.DataContext = createPlaylistViewModel;
            settingPage = new SettingPage();
            settingViewModel = new SettingViewModel(user);
            settingViewModel.PropertyChanged += OnSettingPropertyChanged;
            settingPage.DataContext = settingViewModel ;
            topMusicPage = new HomePage();
            topMusicPage.DataContext = new TopSongListViewModel(songContent);
            playlistsPage = new PlaylistsPage();
            playlistsPage.DataContext = new PlaylistsPageViewModel(user);

            CurrentPage = homePage;

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += tickCallback;
            timer.Start();
        }

        public Page CurrentPage
        {
            get => currentPage;
            set
            {
                currentPage = value;
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

        public double Volume
        {
            get => player.Volume;
            set
            {
                player.Volume = value;
                OnPropertyChanged("Volume");
            }
        }

        public double Progress
        {
            get => progress;
            set
            {
                progress = value;
                OnPropertyChanged("Progress");
            }
        }

        public double Duration
        {
            get => duration;
            set
            {
                duration = value;
                OnPropertyChanged("Duration");
            }
        }



        private Command switchToHomePage;
        private Command switchToCreatePlaylistPage;
        private Command switchToSettingPage;
        private Command switchToTopMusicPage;
        private Command switchToPlaylistsPage;
        private Command stopAndPlay;
        private Command likeSong;
        private Command nextSong;
        private Command prevSong;
        private Command volumeOnOff;

        public Command SwitchToHomePage
        {
            get
            {
                return switchToHomePage??(switchToHomePage = new Command((obj) =>
                {
                    CurrentPage = homePage;
                }));
            }
        }

        public Command SwitchToCreatePlaylistPage
        {
            get
            {
                return switchToCreatePlaylistPage ?? (switchToCreatePlaylistPage = new Command((obj) =>
                {
                    CurrentPage = createPlaylistPage;
                }));
            }
        }

        public Command SwitchToSettingPage
        {
            get
            {
                return  switchToSettingPage ??(switchToSettingPage = new Command((obj) =>
                {
                    CurrentPage = settingPage;
                }));
            }
        }

        public Command SwitchToTopMusicPAge
        {
            get
            {
                return switchToTopMusicPage ?? (switchToTopMusicPage = new Command((obj) =>
                {
                    CurrentPage = topMusicPage;
                }));
            }
        }

        public Command SwitchToPlaylistsPage
        {
            get
            {
                return switchToPlaylistsPage ?? (switchToPlaylistsPage = new Command((obj) =>
                {
                    CurrentPage = playlistsPage;
                }));
            }
        }

        public Command PrevSong
        {
            get
            {
                return prevSong ?? (prevSong = new Command((obj) =>
                {
                    if (songContent.Prev())
                    {
                        PlaySong(CurrentSong.Source);
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
                    if (isPlayed)
                    {
                        player.Pause();
                        isPlayed = false;
                    }
                    else if(currentSong != null)
                    {
                        player.Play();
                        isPlayed = true;
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
                    if (songContent.Next())
                    {
                        PlaySong(CurrentSong.Source);
                    }
                }));
            }
        }

        public Command LikeSong
        {
            get
            {
                return likeSong??(likeSong  =new Command((obj) =>
                {
                    if (CurrentSong != null)
                    {
                        ++CurrentSong.Rating;
                        var song = dbWorker.Songs.GetById(CurrentSong.Id);
                        dbWorker.Songs.RatingUpdate(song);
                        dbWorker.Save();
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
                    if (player.Volume > 0)
                    {
                        Volume = 0;
                    }
                    else
                    {
                        Volume = 0.5;
                    }
                }));
            }
        }


        private void OnSessionSongPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "CurrentSong")
            {
                this.CurrentSong = songContent.CurrentSong;
                if (this.CurrentSong != null)
                {
                    PlaySong(CurrentSong.Source);
                }
            }
        }

        private void OnSettingPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsExit")
            {
                Login login = new Login();
                Close?.Invoke();
                login.Show();
            }
        }

        private void tickCallback(object sender, EventArgs e)
        {
            if (isEnded && songContent.Next())
            {
                PlaySong(CurrentSong.Source);
            }
            if (player.Source != null && player.NaturalDuration.HasTimeSpan)
            {
                Duration = player.NaturalDuration.TimeSpan.TotalSeconds;
                Progress = player.Position.TotalSeconds;
            }
        }

        private void endAudioCallback(object sender, EventArgs e)
        {
            isPlayed = false;
            isEnded = true;
        }


        private void PlaySong(string path)
        {
            player.Open(new Uri(path, UriKind.Absolute));
            player.Play();
            isPlayed = true;
            isEnded = false;
        }
    }
}
