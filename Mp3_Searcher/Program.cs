using System;
using System.Collections.Generic;
<<<<<<< HEAD
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
=======
using System.Linq;
using System.Text;
using System.Threading.Tasks;
>>>>>>> fdd1e5e9670e8f981558f5fee430fc19196e6141

namespace Mp3_Searcher
{
    class Program
    {
        static void Main(string[] args)
        {
<<<<<<< HEAD
            XmlDocument mp3Document = new XmlDocument();
            mp3Document.Load("C:/Users/s.kovalevsky/source/repos/MusicPortal/Mp3_Searcher/App.config");
            XmlElement configuration = mp3Document.DocumentElement;
            XmlNode mp3 = configuration.FirstChild;
            foreach (XmlNode link in mp3)
            {
                string connectionMp3String = link.InnerText;
                string[] p = Directory.GetFiles(connectionMp3String, "*.mp3");
            }
=======
>>>>>>> fdd1e5e9670e8f981558f5fee430fc19196e6141
        }
    }
}
