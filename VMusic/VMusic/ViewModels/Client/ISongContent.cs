using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMusic.ViewModels.Client
{
    interface ISongContent: INotifyPropertyChanged
    {
        SongViewModel CurrentSong { get; set; }
    }
}
