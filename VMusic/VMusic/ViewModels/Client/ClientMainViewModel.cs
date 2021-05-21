﻿using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using MaterialDesignThemes.Wpf;
using VMusic.Commands;
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
        private bool isPlayed = false;
        private bool isEnded = false;
        private string findSongString = "";

        private PackIconKind playStopButton = PackIconKind.Play;
        private PackIconKind volumeButton = PackIconKind.VolumeHigh;

        private MediaPlayer player;
        private SongViewModel currentSong;
        private SongContent songContent;

        private double progress = 0;
        private double duration;
        private DispatcherTimer timer;

        private HomePage homePage;
        private CreatePlaylistPage createPlaylistPage;
        private SettingPage settingPage;
        private HomePage topMusicPage;
        private PlaylistsPage playlistsPage;
        private SinglePlaylistPage singlePlaylistPage;
        private UpdatePlaylistPage updatePlaylistPage;
        private HomePage currentSongListPage;
        private HomePage findSongPage;
        private GenrePage genrePage;


        private Page currentPage;

       
        private UnitOfWork dbWorker;

        public ClientMainViewModel(User user)
        {
            this.user = user;
            dbWorker = new UnitOfWork();
            player = new MediaPlayer();
            player.MediaEnded += endAudioCallback;

            songContent = new SongContent();
            songContent.PropertyChanged += OnSessionSongPropertyChanged;

            PagesInit();
            CurrentPage = homePage;

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += TickCallback;
            timer.Start();

            this.PropertyChanged += OnIsButtonsPropertyChanged;
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

        public bool IsPlayed
        {
            get => isPlayed;
            set
            {
                isPlayed = value;
                OnPropertyChanged("IsPlayed");
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
                    playlistsPage.DataContext = CreatePlaylistsPageViewModel(this.user);
                    CurrentPage = playlistsPage;
                }));
            }
        }

        public Command SwitchToCurrentSongList
        {
            get
            {
                return switchToCurrentSongList ?? (switchToCurrentSongList = new Command((obj) =>
                {
                    if (songContent.CurrentPlaylist!=null)
                    {
                        currentSongListPage.DataContext = new CurrentSongListViewModel(songContent);
                    }

                    CurrentPage = currentSongListPage;
                }));
            }
        }

        public Command SwitchToGenrePage
        {
            get
            {
                return switchToGenrePage ?? (switchToGenrePage = new Command((obj) =>
                {
                    CurrentPage = genrePage;
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
                        findSongPage.DataContext = new FindSongViewModel(FindSongString, songContent);
                        CurrentPage = findSongPage;
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
                        songContent.Prev();
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
                        IsPlayed = false;
                    }
                    else if(currentSong != null)
                    {
                        player.Play();
                        IsPlayed = true;
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
                        songContent.Next();
                        PlaySong(CurrentSong.Source);
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
                if (isPlayed)
                {
                    player.Stop();
                }
                Close?.Invoke();
                login.Show();
            }
        }

        private void OnPlaylistPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedPlaylist")
            {
                var playlistsViewModel = playlistsPage.DataContext as PlaylistsPageViewModel;
                SinglePlaylistViewModel singlePlaylistViewModel =
                    new SinglePlaylistViewModel(playlistsViewModel.SelectedPlaylist, songContent, user);
                singlePlaylistViewModel.PropertyChanged += OnPlaylistUpdatePropertyChanged;
                singlePlaylistPage.DataContext = singlePlaylistViewModel ;
                CurrentPage = singlePlaylistPage;
            }
        }

        private void OnPlaylistUpdatePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsUpdate")
            {
                var playlistsViewModel = playlistsPage.DataContext as PlaylistsPageViewModel;
                UpdatePlaylistViewModel updatePlaylistViewModel = new UpdatePlaylistViewModel(playlistsViewModel.SelectedPlaylist);
                updatePlaylistViewModel.PropertyChanged += OnPlaylistCreateOrDeletePropertyChanged;
                updatePlaylistPage.DataContext = updatePlaylistViewModel;
                CurrentPage = updatePlaylistPage;
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
                PlayStopButton = IsPlayed == true ? PackIconKind.Stop : PackIconKind.Play;
            }
            if (e.PropertyName == "Volume")
            {
                VolumeButton = Volume == 0? PackIconKind.VolumeLow : PackIconKind.VolumeHigh;
            }
        }


        private void TickCallback(object sender, EventArgs e)
        {
            if (isEnded)
            {
                songContent.Next();
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
            IsPlayed= false;
            isEnded = true;
        }

        private void PagesInit()
        {
            homePage = CreateHomePage(songContent);
            playlistsPage = new PlaylistsPage();
            createPlaylistPage = CreateAddPlaylistPage(this.user, (PlaylistsPageViewModel)this.playlistsPage.DataContext);
            settingPage = CreateSettingPage(this.user);
            topMusicPage = CreateTopMusicPage(songContent);
            singlePlaylistPage = new SinglePlaylistPage();
            findSongPage = new HomePage();
            currentSongListPage = new HomePage();
            updatePlaylistPage = new UpdatePlaylistPage();
            genrePage = new GenrePage();
        }


        private HomePage CreateHomePage(SongContent songContent)
        {
            HomePage homePage = new HomePage();
            homePage.DataContext = new HomePageViewModel(songContent);
            return homePage;
        }

        private CreatePlaylistPage CreateAddPlaylistPage(User user, PlaylistsPageViewModel playlistsPageViewModel)
        {
            CreatePlaylistPage createPlaylistPage = new CreatePlaylistPage();
            CreatePlaylistViewModel createPlaylistViewModel = new CreatePlaylistViewModel(playlistsPageViewModel, user);
            createPlaylistViewModel.PropertyChanged += OnPlaylistCreateOrDeletePropertyChanged;
            createPlaylistPage.DataContext = createPlaylistViewModel;
            return createPlaylistPage;
        }

        private SettingPage CreateSettingPage(User user)
        {
            SettingPage settingPage = new SettingPage();
            SettingViewModel settingViewModel = new SettingViewModel(user);
            settingViewModel.PropertyChanged += OnSettingPropertyChanged;
            settingPage.DataContext = settingViewModel;
            return settingPage;
        }

        private HomePage CreateTopMusicPage(SongContent songContent)
        {
            HomePage topMusicPage = new HomePage();
            topMusicPage.DataContext = new TopSongListViewModel(songContent);
            return topMusicPage;
        }

        private PlaylistsPageViewModel CreatePlaylistsPageViewModel(User user)
        {
            PlaylistsPageViewModel playlistsPageViewModel = new PlaylistsPageViewModel(user);
            playlistsPageViewModel.PropertyChanged += OnPlaylistPropertyChanged;
            return playlistsPageViewModel;
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
            var obj = playlistsPage.DataContext as PlaylistsPageViewModel;
            if (obj != null)
            {
                obj.PlaylistsDataUpdate(playlist);
            }
        }

        private void PlaySong(string path)
        {
            player.Open(new Uri(path, UriKind.Absolute));
            player.Play();
            IsPlayed = true;
            isEnded = false;
        }   
    }
}
