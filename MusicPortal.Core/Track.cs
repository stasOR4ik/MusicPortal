﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicPortal.Core
{
    public class Track
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PictureLink { get; set; }
        public string Duration { get; set; }
        public string Mp3Path { get; set; }
        public Artist Artist { get; set; }
        public Album Album { get; set; }

        public Track() { }

        public Track(string name) => Name = name;

        public void SetPictureLink(string pictureLink)
        {
            string defaultPictureLink = "../images/song.png";
            PictureLink = pictureLink == "" ? defaultPictureLink : pictureLink;
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
    }
}