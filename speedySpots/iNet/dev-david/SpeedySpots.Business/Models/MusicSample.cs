using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpeedySpots.Business.Models
{
    public class MusicSampleWithRating
    {
        public int IAMusicID { get; set; }
        public string FileNameDisplay { get; set; }
        public string Filename { get; set; }
        public int IAMusicGroupID { get; set; }
        public string IAMusicGroupName { get; set; }
        public int IAMusicTempoID { get; set; }
        public string IAMusicTempoName { get; set; }
        public int LengthSecs { get; set; }
        public string LengthBeds { get; set; }
        public string Path { get; set; }
        public int Rating { get; set; }
    }

    public class MusicSampleBasic
    {
        public int IAMusicID { get; set; }
        public string FileNameDisplay { get; set; }
        public string Filename { get; set; }
        public int IAMusicGroupID { get; set; }
        public int IAMusicTempoID { get; set; }       
        public int LengthSecs { get; set; }
        public string LengthBeds { get; set; }
        public string Path { get; set; }
    }
}