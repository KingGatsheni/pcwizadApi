using Microsoft.EntityFrameworkCore;
namespace Models
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
            
        }

        public static object Products { get; internal set; }
        public DbSet<Models.Products> products  { get; set; }
        public DbSet<Models.UserInfo> userInfos { get; set; }
    }
}