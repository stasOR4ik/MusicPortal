using MusicPortal.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MusicPortal.Repo;
using System;
using System.IO;
using System.Linq;
using System.Xml;

namespace MusicPortal.Mp3_Searcher
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
            MusicContext db = serviceProvider.GetService<MusicContext>();
            XmlDocument mp3Document = new XmlDocument();
            mp3Document.Load("C:/Users/s.kovalevsky/source/repos/MusicPortal/Mp3_Searcher/App.config");
            XmlElement configuration = mp3Document.DocumentElement;
            XmlNode mp3 = configuration.FirstChild;
            foreach (XmlNode link in mp3)
            {
                string connectionMp3 = link.InnerText;
                DirectoryInfo directoryInfo = new DirectoryInfo(connectionMp3);
                foreach (FileInfo file in directoryInfo.GetFiles("*.mp3"))
                {
                    (string, string) artistAndTrack = SeparateTrack(file.Name);
                    Track track = db.Tracks.FirstOrDefault(p => p.Name == artistAndTrack.Item2);
                    if (track != null)
                    {
                        File.Copy(file.FullName, "C:\\Users\\s.kovalevsky\\source\\repos\\MusicPortal\\MusicPortal\\wwwroot\\audio\\" + file.Name, true);
                        track.Mp3Path = "../../audio/" + file.Name;
                        db.SaveChanges();
                    }
                }
            }
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
           serviceCollection.AddDbContext<MusicContext>(options =>
                options.UseSqlServer("Server =.\\SQLEXPRESS; Database = MusicDB; Trusted_Connection = True"));
        }

        public static (string, string) SeparateTrack(string track)
        {
            track = track.Replace('_', ' ');
            track = track.Remove(track.Length - 4);
            track = track.Split('\\').Last();
            string[] p = track.Split('-');
            return (p[0], p[1]);
        }

    }
}
