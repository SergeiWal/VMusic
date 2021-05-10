using System;
using System.ComponentModel;
using System.Linq;
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

        public static string LIKE_SONG_LIST_NAME = "Избраное";

        private User user;
        private bool isPlayed = false;
        private bool isEnded = false;
        private MediaPlayer player;
        private double progress = 0;
        private double duration;
        private DispatcherTimer timer;

        private HomePage homePage;
        private CreatePlaylistPage createPlaylistPage;
        private SettingPage settingPage;
        private HomePage topMusicPage;
        private PlaylistsPage playlistsPage;
        private SinglePlaylistPage singlePlaylistPage;
        private HomePage currentSongListPage;
        private HomePage findSongPage;

        private string findSongString = "";
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

            PagesInit();
            CurrentPage = homePage;

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += tickCallback;
            timer.Start();
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



        private Command switchToHomePage;
        private Command switchToCreatePlaylistPage;
        private Command switchToSettingPage;
        private Command switchToTopMusicPage;
        private Command switchToPlaylistsPage;
        private Command switchToCurrentSongList;
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
                Close?.Invoke();
                login.Show();
            }
        }

        private void OnPlaylistPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedPlaylist")
            {
                var playlistsViewModel = playlistsPage.DataContext as PlaylistsPageViewModel;
                singlePlaylistPage.DataContext = new SinglePlaylistViewModel(playlistsViewModel.SelectedPlaylist, songContent, user);
                CurrentPage = singlePlaylistPage;
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

        private void PagesInit()
        {
            homePage = CreateHomePage(songContent);
            playlistsPage = CreatePlaylistsPage(this.user);
            createPlaylistPage = CreateAddPlaylistPage(this.user, (PlaylistsPageViewModel)this.playlistsPage.DataContext);
            settingPage = CreateSettingPage(this.user);
            topMusicPage = CreateTopMusicPage(songContent);
            singlePlaylistPage = new SinglePlaylistPage();
            findSongPage = new HomePage();
            currentSongListPage = new HomePage();
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
            createPlaylistPage.DataContext = new CreatePlaylistViewModel(playlistsPageViewModel, user);
            return createPlaylistPage;
        }

        private SettingPage CreateSettingPage(User user)
        {
            SettingPage settingPage = new SettingPage();
            SettingViewModel settingViewModel = new SettingViewModel(user);
            settingViewModel.PropertyChanged += OnSettingPropertyChanged;
            settingPage.DataContext = new SettingViewModel(user);
            return settingPage;
        }

        private HomePage CreateTopMusicPage(SongContent songContent)
        {
            HomePage topMusicPage = new HomePage();
            topMusicPage.DataContext = new TopSongListViewModel(songContent);
            return topMusicPage;
        }

        private PlaylistsPage CreatePlaylistsPage(User user)
        {
            PlaylistsPage playlistsPage = new PlaylistsPage();
            PlaylistsPageViewModel playlistsPageViewModel = new PlaylistsPageViewModel(user);
            playlistsPageViewModel.PropertyChanged += OnPlaylistPropertyChanged;
            playlistsPage.DataContext = playlistsPageViewModel;
            return playlistsPage;
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
            isPlayed = true;
            isEnded = false;
        }
    }
}
