using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMusicLibrary.Model
{
    public enum MusicCategory
    {
        Classical,
        Country,
        International,
        Others,
        Rock
    }
    public class Music
    {
        public string Name { get; set; }
        public MusicCategory Category { get; set; }
        public string MusicFile { get; set; }
        public string ImageFile { get; set; }

        public Music(string name, MusicCategory category)
        {
            Name = name;
            Category = category;

            MusicFile = $"Assets/Music/{category}/{name}.mp3";
            ImageFile = $"Assets/Images/{category}/{name}.jpeg";
        } 


    }
}
