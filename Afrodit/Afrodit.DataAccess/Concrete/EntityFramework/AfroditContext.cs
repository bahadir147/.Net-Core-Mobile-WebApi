using Afrodit.Core.Helper;
using Afrodit.Repositories.DTOs;
using Afrodit.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace Afrodit.Repositories.Concrete.EntityFramework
{
    public class AfroditContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var appSettingsJson = AppSettingsJson.GetAppSettings();
            var connectionString = appSettingsJson["ConnectionStrings:DefaultConnection"];
            optionsBuilder.UseSqlServer(connectionString);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Follower> Followers { get; set; }
        public DbSet<Photo> Photos { get; set; }

        //View kullanmak için burada tanımla ve repository içinde custom metod ile query çalıştır.
        //public DbQuery<UserHeadersDTO> UserHeadersDTOs { get; set; }
    }
}
