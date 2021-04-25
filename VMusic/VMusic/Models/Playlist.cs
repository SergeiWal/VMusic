using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMusic.Models
{
    class Playlist
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Image { get; set; }

        public int? UserId { get; set; }
        public User User { get; set; }

        public ICollection<Sing> Sings { get; set; }

        public Playlist()
        {
            Sings = new List<Sing>();
        }
    }
}
