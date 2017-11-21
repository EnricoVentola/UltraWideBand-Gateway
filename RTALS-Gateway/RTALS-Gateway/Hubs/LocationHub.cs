using Microsoft.AspNet.SignalR;
using Microsoft.EntityFrameworkCore;
using RTALSGatewayRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTALS_Gateway.Hubs
{
    public class LocationHub : Hub
    {
     
public async Task Send(string message)
        {
            Console.WriteLine("Receievd message");

            Console.WriteLine(Context.ConnectionId.ToString());
            Console.WriteLine(Context.Headers.ToString());
            Console.WriteLine(Context.QueryString.ToString());
            Console.WriteLine(Context.Request.ToString());
            Console.WriteLine(Context.RequestCookies.ToString());
            Console.WriteLine(Context.User.ToString());

            await this.Clients.All.RecieveMessage(message);
        }

        private int Users = 0;

        public async Task BroadcastNumberOfUsers(int nbUser)
        {
            await this.Clients.All.OnUserConnected(nbUser);
        }

        public override async Task OnConnected()
        {

            var optionsBuilder = new DbContextOptionsBuilder<GatewayContext>();
            optionsBuilder.UseSqlServer(Properties.Settings.Default.RTALSDatabase);

            using (var context = new GatewayContext(optionsBuilder.Options))
            {
                var init = new Connection
                {
                    UserAgent = "",
                    Connected = true,
                    ConnectionID = Context.ConnectionId,
                    ConnTimestamp = DateTime.Now
                };
                context.Connections.Add(init);
                context.SaveChanges();
            }

            Users++;
            await BroadcastNumberOfUsers(Users);
            await base.OnConnected();
        }

        public override async Task OnDisconnected(bool stopCalled) {

            var optionsBuilder = new DbContextOptionsBuilder<GatewayContext>();
            optionsBuilder.UseSqlServer(Properties.Settings.Default.RTALSDatabase);

            using (var context = new GatewayContext(optionsBuilder.Options))
            {
                var exisConn = await context.Connections.FindAsync(Context.ConnectionId);
                exisConn.DisConnTimestamp = DateTime.Now;
                exisConn.Connected = false;
               
                context.SaveChanges();
            }

            Users--;
            await BroadcastNumberOfUsers(Users);
            await base.OnDisconnected(stopCalled);
        }
    }
}
