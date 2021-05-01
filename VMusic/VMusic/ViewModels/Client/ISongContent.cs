using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMusic.Models;

namespace VMusic.ViewModels.Client
{
    interface ISongContent: INotifyPropertyChanged
    {
        SongViewModel CurrentSong { get; set; }
        ObservableCollection<SongViewModel> CurrentPlaylist { get; set; }
    }
}
