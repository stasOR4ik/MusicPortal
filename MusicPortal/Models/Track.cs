using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicPortal.Models
{
    public class Track
    {
        public int Id { get; set; }
        public string Name { get; set; }
        private string PictureLink { get; set; }
        private string Duration { get; set; }
        public List<string> Tags { get; set; }

        public Track() { }

        public Track(string name)
        {
            Name = name;
            Tags = new List<string>();
        }

        public void SetPictureLink(string pictureLink)
        {
            string defaultPictureLink = "https://lastfm-img2.akamaized.net/i/u/174s/4128a6eb29f94943c9d206c08e625904";
            PictureLink = pictureLink == "" ? defaultPictureLink : pictureLink;
        }

        public string GetPictureLink()
        {
            return PictureLink;
        }

        public void SetDurationInMilliseconds(string millisecondsDuration)
        {
            if (millisecondsDuration.Length <= 3)
            {
                Duration = "0:00";
            }
            else
            {
                SetDuration(millisecondsDuration.Substring(0, millisecondsDuration.Length - 3));
            }
        }

        public void SetDuration(string secondsDuration)
        {
            int seconds = Convert.ToInt32(secondsDuration);
            int minutes = seconds / 60;
            seconds %= 60;
            Duration = minutes + ":" + (seconds < 10 ? "0" : "") + seconds;
        }

        public string GetDuration()
        {
            return Duration;
        }

    }
}
