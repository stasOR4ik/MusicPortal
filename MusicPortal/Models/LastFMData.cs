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

        public List<Artist> GetTopArtists(int page, int limit, int imageSize)
        {
            string artistsUrl = commonUrl + "method=chart.gettopartists&page=" + page + "&limit=" + limit;
            List<Artist> artists = new List<Artist>();
            foreach (JToken singer in TakeJObjectFromLastFM(artistsUrl)["artists"]["artist"])
            {
                Artist artist = new Artist(singer.SelectToken("name").ToString());
                artist.SetPictureLink(singer["image"][imageSize].SelectToken("#text").ToString());
                artists.Add(artist);
            }
            return artists;
        }

        public List<Track> GetArtistTopTracks(string name, int page, int limit)
        {
            string tracksUrl = commonUrl + "method=artist.gettoptracks&page=" + page + "&limit=" + limit + "&artist=" + name;
            List<Track> tracks = new List<Track>();
            foreach(JToken song in TakeJObjectFromLastFM(tracksUrl).SelectToken("toptracks")["track"])
            {
                Track track = new Track(song.SelectToken("name").ToString());
                track.SetPictureLink(GetTrackImage(name, track.Name));
                track.SetDurationInMilliseconds(GetTrackDurationInMilliseconds(name, track.Name));
                tracks.Add(track);
            }
            return tracks;
        }

        //public List<Album> GetArtistTopAlbums(string name, int page, int limit)
        //{

        //}

        public string GetTrackDurationInMilliseconds(string artistName, string trackName)
        {
            string trackUrl = commonUrl + "method=track.getInfo&artist=" + artistName + "&track=" + trackName;
            if (TakeJObjectFromLastFM(trackUrl).SelectToken("track") != null &&
                TakeJObjectFromLastFM(trackUrl).SelectToken("track").SelectToken("duration") != null)
                return TakeJObjectFromLastFM(trackUrl).SelectToken("track").SelectToken("duration").ToString();
            else
                return "0";
        }

        public string GetTrackImage(string artistName, string trackName)
        {
            string trackUrl = commonUrl + "method=track.getInfo&artist=" + artistName + "&track=" + trackName; 
            if (TakeJObjectFromLastFM(trackUrl).SelectToken("track") != null &&
                TakeJObjectFromLastFM(trackUrl).SelectToken("track").SelectToken("album") != null &&
                TakeJObjectFromLastFM(trackUrl).SelectToken("track").SelectToken("album").SelectToken("image") != null)
                return TakeJObjectFromLastFM(trackUrl).SelectToken("track").SelectToken("album").SelectToken("image")[0].SelectToken("#text").ToString();
            else
                return "";
        }

        public string GetArtistBiography(string artistName, string biographySize)   // biographySize = {"summary", "content"}
        {
            string artistUrl = commonUrl + "method=artist.getinfo&" + "artist=" + artistName;
            string biography = TakeJObjectFromLastFM(artistUrl).SelectToken("artist")["bio"].SelectToken(biographySize).ToString();
            return ParsingBiography(biography);
        }

        public string ParsingBiography(string biography)
        {
            bool IsRun = true;
            while (IsRun)
            {
                int beginLinkIndex = biography.IndexOf("[http");
                int endLinkIndex = biography.IndexOf("]");
                if (beginLinkIndex != -1 && endLinkIndex > beginLinkIndex)
                {
                    biography = biography.Remove(beginLinkIndex, endLinkIndex - beginLinkIndex + 1);
                }
                else if (beginLinkIndex == -1)
                {
                    IsRun = false;
                }
            }
            biography = biography.Split("http")[0];
            biography = biography.Split(" <a href")[0];
            return biography;
        }

        public JObject TakeJObjectFromLastFM(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string jsonFromLastFM = new StreamReader(response.GetResponseStream(), Encoding.UTF8).ReadToEnd();
            return JObject.Parse(jsonFromLastFM);
        }
    }
}
