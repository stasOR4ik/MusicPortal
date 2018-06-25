using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Mp3_Searcher
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlDocument mp3Document = new XmlDocument();
            mp3Document.Load("C:/Users/s.kovalevsky/source/repos/MusicPortal/Mp3_Searcher/App.config");
            XmlElement configuration = mp3Document.DocumentElement;
            XmlNode mp3 = configuration.FirstChild;
            foreach (XmlNode link in mp3)
            {
                string connectionMp3String = link.InnerText;
                string[] p = Directory.GetFiles(connectionMp3String, "*.mp3");
            }
        }
    }
}
