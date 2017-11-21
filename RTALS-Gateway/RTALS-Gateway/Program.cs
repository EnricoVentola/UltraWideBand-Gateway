using System;
using Microsoft.Owin.Hosting;
using RTALSGatewayRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace RTALS_Gateway
{
    class Program
    {

        public IConfiguration Configuration { get; }
        public static GatewayContext context;

        static void Main(string[] args)
        {
            // This will *ONLY* bind to localhost, if you want to bind to all addresses
            // use http://*:8080 to bind to all addresses. 
            // See http://msdn.microsoft.com/en-us/library/system.net.httplistener.aspx 
            // for more information.
            string url = "http://localhost:8080";
            using (WebApp.Start(url))
            {
                Console.WriteLine("Server running on {0}", url);
                PerformDatabaseInit(url);
                Console.ReadLine();
            }


        }

        public static void PerformDatabaseInit(string url)
        {

            var optionsBuilder = new DbContextOptionsBuilder<GatewayContext>();
            optionsBuilder.UseSqlServer(Properties.Settings.Default.RTALSDatabase);

            using (context = new GatewayContext(optionsBuilder.Options))
            {
                var init = new GatewayInit
                {
                    URL = url,
                    StartUp = DateTime.Now
                };

                context.GatewayInitialisation.Add(init);
                context.SaveChanges();
            }
            Console.WriteLine("Database Connected Established, Logged Startup Variables");
            Console.WriteLine("Handing over to SignalR Hub");
        }
    }
}