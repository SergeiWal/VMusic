using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Threading;
using VMusic.ViewModels;

namespace VMusic.Controller.Client.Player
{
    class Player: IPlayer
    {
        private double progress = 0;
        private double duration;
        private bool isPlayed = false;
        private bool isEnded = false;
        private MediaPlayer mediaPlayer;
        private DispatcherTimer timer;
        private SongViewModel currentSong;
        private ObservableCollection<SongViewModel> currentPlaylist;


        public Player()
        {
            mediaPlayer = new MediaPlayer();
            mediaPlayer.MediaEnded += EndAudioCallback;

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += TickCallback;
            timer.Start();
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

        public double Volume
        {
            get => mediaPlayer.Volume;
            set
            {
                mediaPlayer.Volume = value;
                OnPropertyChanged("Volume");
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

        public bool IsEnded
        {
            get => isEnded;
            set
            {
                isEnded = value;
                OnPropertyChanged("IsEnded");
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

        public ObservableCollection<SongViewModel> CurrentPlaylist
        {
            get => currentPlaylist;
            set
            {
                currentPlaylist = value;
                OnPropertyChanged("CurrentPlaylist");
            }
        }

        public void Next()
        {
            int i = CurrentPlaylist.IndexOf(CurrentSong);
            ++i;
            if (i < CurrentPlaylist.Count)
            {
                CurrentSong = CurrentPlaylist[i];
            }
            else
            {
                CurrentSong = CurrentPlaylist[0];
            }
        }

        public void Prev()
        {
            int i = CurrentPlaylist.IndexOf(CurrentSong);
            --i;
            if (i >= 0)
            {
                CurrentSong = CurrentPlaylist[i];
            }
            else
            {
                CurrentSong = CurrentPlaylist[CurrentPlaylist.Count - 1];
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void TickCallback(object sender, EventArgs e)
        {
            if (isEnded)
            {
                Next();
                StartPlay(CurrentSong.Source);
            }
            if (mediaPlayer.Source != null && mediaPlayer.NaturalDuration.HasTimeSpan)
            {
                Duration = mediaPlayer.NaturalDuration.TimeSpan.TotalSeconds;
                Progress = mediaPlayer.Position.TotalSeconds;
            }
        }

        private void EndAudioCallback(object sender, EventArgs e)
        {
            IsPlayed = false;
            IsEnded = true;
        }

        public void StartPlay(string path)
        {
            mediaPlayer.Open(new Uri(path, UriKind.Relative));
            mediaPlayer.Play();
            IsPlayed = true;
            isEnded = false;
        }

        public void Play()
        {
            mediaPlayer.Play();
            IsPlayed = true;
        }

        public void Stop()
        {
            mediaPlayer.Pause();
            IsPlayed = false;
        }

        public void Load(SongViewModel song, ObservableCollection<SongViewModel> songs)
        {
            CurrentSong = song;
            CurrentPlaylist = songs;
        }
    }
}
