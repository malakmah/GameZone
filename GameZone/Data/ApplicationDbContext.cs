
using GameZone.Models;

namespace GameZone.Data
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {
             
        }
        public DbSet<Game> Games { get; set; }
        public DbSet<Category> Categorys { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<GameDevice> GameDevices { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasData(new Category[]
                {
                    new Category{Id =1,Name="Sppris"},
                    new Category{Id =2,Name="Action"},
                    new Category{Id =3,Name="Adventure"},
                    new Category{Id =4,Name="film"}

                });
            modelBuilder.Entity<Device>()
                .HasData(new Device[]
                {
                    new Device{Id=1,Name="Xbox",Icon="ib ib-pc-display"}
                });
            modelBuilder.Entity<GameDevice>()
              .HasKey(e => new { e.GameId, e.DeviceId });
            base.OnModelCreating(modelBuilder);
        }
    }
}
