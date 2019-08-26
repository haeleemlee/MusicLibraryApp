using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace MusicLibaryApp
{
    public class MusicEntry
    {
        private const string TEXT_FILE_NAME = "Music.txt";
        public string MusicTitle { get; set; }
        public string Album { get; set; }
        public string Singer { get; set; }
        public string ReleaseDate { get; set; }
        public string ImageFilePath { get; set; }
        public string MusicFilePath { get; set; }
        public static void WriteMusicEntry(MusicEntry music)
        {
            var MusicEntryData = $"{music.Album},{music.MusicFilePath},{music.MusicTitle},{music.ReleaseDate},{music.Singer},{music.ImageFilePath}";
            FileHelper.WriteTextFileAsync(TEXT_FILE_NAME, MusicEntryData);
        }

        public async static Task<ICollection<MusicEntry>> GetAllMusicEntriesAsync()
        {
            var musicEntries = new List<MusicEntry>();
            if (!File.Exists(TEXT_FILE_NAME))
            {
                return musicEntries;
            }
            var content = await FileHelper.ReadTextFileAsync(TEXT_FILE_NAME);
            var lines = content.Split('\r', '\n');
            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line))
                    continue;
                var lineParts = line.Split(',');
                var music = new MusicEntry
                {
                    MusicTitle = lineParts[0],
                    Album = lineParts[1],
                    Singer = lineParts[2],
                    ReleaseDate = lineParts[3],
                    ImageFilePath = lineParts[4],
                    MusicFilePath = lineParts[5]
                };
                musicEntries.Add(music);
            }
            return musicEntries;

        }
    }
}
