
using Microsoft.EntityFrameworkCore;


namespace RTALSGatewayRepository
{
    public class GatewayContext : DbContext
    {
        private string rTALSDatabase;

        #region Gateway
        public DbSet<User> Users { get; set; }
        public DbSet<Connection> Connections { get; set; }
        public DbSet<GatewayInit> GatewayInitialisation{ get; set; }
        #endregion

        public GatewayContext(DbContextOptions<GatewayContext> options) : base(options)
        {

        }

        public GatewayContext(string rTALSDatabase)
        {
            this.rTALSDatabase = rTALSDatabase;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Gateway
            // TODO: Manually specify table relationships here
            #endregion
        }
    }
}