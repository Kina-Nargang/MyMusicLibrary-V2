using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMusicLibrary.Model
{
    public class MusicManager
    {
        private static List<Music> getMusic()
        {
            var musics = new List<Music>();

            musics.Add(new Music("Downfall", MusicCategory.Classical));
            musics.Add(new Music("Lesser Faith", MusicCategory.Classical));
            musics.Add(new Music("Phase2", MusicCategory.Classical));

            musics.Add(new Music("Brain ID 1270", MusicCategory.Country));
            musics.Add(new Music("Squirrel Fever", MusicCategory.Country));
            musics.Add(new Music("The Meat Rack Jan 2016 LIVE", MusicCategory.Country));

            musics.Add(new Music("Shipping Lanes", MusicCategory.Others));
            musics.Add(new Music("The Stork", MusicCategory.Others));
            musics.Add(new Music("Towel Defence Sad Ending", MusicCategory.Others));

            musics.Add(new Music("Say Goodnight", MusicCategory.Rock));
            musics.Add(new Music("Storybook", MusicCategory.Rock));
            musics.Add(new Music("Up North Classic", MusicCategory.Rock));

            return musics;

        }

        public static void GetAllMusic(ObservableCollection<Music> musics)
        {
            var allMusics = getMusic();
            musics.Clear();
            allMusics.ForEach(m => musics.Add(m));
        }

        public static void GetMusicByCategory(ObservableCollection<Music> musics, MusicCategory category)
        {
            var allMusic = getMusic();
            var filteredMusic = allMusic.Where(m => m.Category == category).ToList();
            musics.Clear();
            filteredMusic.ForEach(m => musics.Add(m));
        }

 /*       
        public static void GetNewMusic(ObservableCollection<Music> musics,string musicname, MusicCategory category)
        {
            var allMusics = getMusic();
            musics.Clear();
            allMusics.Add(new Music(musicname, category));
            
        }
 */
    }
}
