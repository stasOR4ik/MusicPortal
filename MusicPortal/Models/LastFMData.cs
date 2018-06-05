using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MusicPortal.Models
{
    public class LastFMData
    {
        private string commonUrl = "http://ws.audioscrobbler.com/2.0/?api_key=6406cb88807bffe5b2492343145f8451&format=json&";

        public List<Artist> GetTopArtists(int page, int limit)
        {
            string artistsUrl = commonUrl + "method=chart.gettopartists&page=" + page + "&limit=" + limit;
            List<Artist> artists = new List<Artist>();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(artistsUrl);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string jsonFromLastFM = new StreamReader(response.GetResponseStream(), Encoding.UTF8).ReadToEnd();
            JObject jsonObject = JObject.Parse(jsonFromLastFM);
            foreach (JToken singer in jsonObject["artists"]["artist"])
            {
                Artist artist = new Artist(singer.SelectToken("name").ToString());
                artist.SetPictureLink(singer["image"][2].SelectToken("#text").ToString());
                artists.Add(artist);
            }
            return artists;
        }
    }
}
