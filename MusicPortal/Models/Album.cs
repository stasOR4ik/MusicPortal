using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicPortal.Models
{
    public class Album
    {
        public int Id { get; set; }
        public string Name { get; set; }
        private string PictureLink { get; set; }
        public Artist Artist { get; set; }

        public Album() { }

        public Album(string name)
        {
            Name = name;
        }

        public void SetPictureLink(string pictureLink)
        {
            string defaultPictureLink = "images/track.png";
            PictureLink = pictureLink == "" ? defaultPictureLink : pictureLink;
        }

        public string GetPictureLink()
        {
            return PictureLink;
        }
    }
}
