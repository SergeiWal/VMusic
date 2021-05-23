using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using VMusic.ViewModels;

namespace VMusic.Controller.Client.Player
{
    interface IPlayer: INotifyPropertyChanged
    {
        double Progress { get; set; }
        double Duration { get; set; }
        double Volume { get; set; }
        bool IsPlayed { get; set; }
        bool IsEnded { get; set; }
        SongViewModel CurrentSong { get; set; }
        ObservableCollection<SongViewModel> CurrentPlaylist { get; set; }
        void Load(SongViewModel song, ObservableCollection<SongViewModel> songs);
        void Next();
        void Prev();
        void Play();
        void Stop();
        void StartPlay(string path);
    }
}
