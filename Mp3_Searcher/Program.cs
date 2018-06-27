using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using MusicPortal;
using Repo;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Xml;

namespace Mp3_Searcher
{
    class Program
    {
        static void Main(string[] args)
        {
            //var optionsBuilder = new DbContextOptionsBuilder();
            //optionsBuilder.UseSqlServer(Startup.);
            //var dbContext = new GeekDinnerDbContext(optionsBuilder.Options);
            //var controller = new DinnersController(dbContext);
            //var result = (ViewResult)controller.Index();
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
            serviceProvider.GetService<MusicContext>().Artists.FirstOrDefault();

            //MusicContext context = new MusicContext(serviceProvider.G);
            //context.Artists.FirstOrDefault(p => p.);

            //var container = new Container();
            //container.Configure(config =>
            //{
            //    // Register stuff in container, using the StructureMap APIs...
            //    config.Scan(_ =>
            //    {
            //        _.AssemblyContainingType(typeof(Program));
            //        _.WithDefaultConventions();
            //    });
            //    // Populate the container using the service collection
            //    config.Populate(services);
            //});

            //serviceProvider = container.GetInstance<IServiceProvider>();

            //XmlDocument mp3Document = new XmlDocument();
            //mp3Document.Load("C:/Users/s.kovalevsky/source/repos/MusicPortal/Mp3_Searcher/App.config");
            //XmlElement configuration = mp3Document.DocumentElement;
            //XmlNode mp3 = configuration.FirstChild;
            //foreach (XmlNode link in mp3)
            //{
            //    string connectionMp3String = link.InnerText;
            //    string[] p = Directory.GetFiles(connectionMp3String, "*.mp3");
            //}
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
           serviceCollection.AddDbContext<MusicContext>(options =>
                options.UseSqlServer("Server =.\\SQLEXPRESS; Database = MusicDB; Trusted_Connection = True"));
        }
    }
}
