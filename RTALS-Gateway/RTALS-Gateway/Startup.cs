using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using RTALS_Gateway.Hubs;
using Microsoft.AspNet.SignalR;
using Owin;

[assembly: OwinStartup(typeof(RTALS_Gateway.Startup))]

namespace RTALS_Gateway
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Map("/signalr", map =>
            {
                // Setup the CORS middleware to run before SignalR.
                // By default this will allow all origins. You can 
                // configure the set of origins and/or http verbs by
                // providing a cors options with a different policy.
                map.UseCors(CorsOptions.AllowAll);
                var hubConfiguration = new HubConfiguration
                {
                    // You can enable JSONP by uncommenting line below.
                    // JSONP requests are insecure but some older browsers (and some
                    // versions of IE) require JSONP to work cross domain
                    // EnableJSONP = true
                  
                };
                hubConfiguration.EnableDetailedErrors = true;
               // hubConfiguration.EnableJavaScriptProxies = false;

                // Run the SignalR pipeline. We're not using MapSignalR
                // since this branch already runs under the "/signalr"
                // path.
                GlobalHost.HubPipeline.AddModule(new ErrorHandlingPipeline());

                map.RunSignalR(hubConfiguration);

            });
            }
    }
}
