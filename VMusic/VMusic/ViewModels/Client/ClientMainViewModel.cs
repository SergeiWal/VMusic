﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using VMusic.Commands;
using VMusic.Models;
using VMusic.Repository;
using VMusic.Views.Client;

namespace VMusic.ViewModels.Client
{
    class ClientMainViewModel: BaseWindowViewModel
    {

        private bool isPlayed = false;
        private MediaPlayer player;
        private double progress = 0;
        private double duration;
        private DispatcherTimer timer;

        private HomePage homePage;
        private HomePageViewModel homePageViewModel;

        private Page currentPage;
        private SongViewModel currentSong;
        private SongContent songContent;
        private SongRepository songRepository;

        public ClientMainViewModel(Window owner) : base(owner)
        {
            songRepository = new SongRepository();
            player = new MediaPlayer();

            songContent = new SongContent();
            songContent.PropertyChanged += OnSessionSongPropertyChanged;

            homePage = new HomePage();
            homePageViewModel = new HomePageViewModel(songContent);
            homePage.DataContext = homePageViewModel;

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
        private Command stopAndPlay;
        private Command likeSong;

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

        public Command LikeSong
        {
            get
            {
                return likeSong??(likeSong  =new Command((obj) =>
                {
                    if (CurrentSong != null)
                    {
                        ++CurrentSong.Rating;
                        var song = songRepository.GetById(CurrentSong.Id);
                        songRepository.RatingUpdate(song);
                        songRepository.Save();
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
                    PlaySong(this.CurrentSong.Source);
                }
            }
        }

        private void tickCallback(object sender, EventArgs e)
        {
            if (player.Source != null && player.NaturalDuration.HasTimeSpan)
            {
                Duration = player.NaturalDuration.TimeSpan.TotalSeconds;
                Progress = player.Position.TotalSeconds;
            }
        }


        private void PlaySong(string path)
        {
            player.Open(new Uri(path, UriKind.Absolute));
            player.Play();
            isPlayed = true;
        }
    }
}
