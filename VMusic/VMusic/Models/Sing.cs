using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMusic.Models
{
    class Sing
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Album { get; set; }
        public int Rating { get; set; }
        public string Source { get; set; }
        public MusicGenre Genre { get; set; }
        public byte[] Image { get; set; }

        public ICollection<Playlist> Playlists { get; set; }

        public Sing()
        {
            Playlists = new List<Playlist>();
        }
    }
}
