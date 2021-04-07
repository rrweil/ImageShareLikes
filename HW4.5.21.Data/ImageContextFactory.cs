using System.IO;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace HW4._5._21.Data
{
    public class ImageContextFactory : IDesignTimeDbContextFactory<ImageDbContext>
    {
        public ImageDbContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), $"..{Path.DirectorySeparatorChar}HW4.5.21.Web"))
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true).Build();

            return new ImageDbContext(config.GetConnectionString("ConStr"));
        }
    }
    {
    }
}
