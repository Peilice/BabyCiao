using Microsoft.EntityFrameworkCore;

namespace BabyCiao.Models
{
    public partial class BabyCiaoContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("BabyCiaoContext", options =>
            {
                options.EnableRetryOnFailure(
                    maxRetryCount: 5, // 重试次数
                    maxRetryDelay: TimeSpan.FromSeconds(10), // 每次重试的延迟
                    errorNumbersToAdd: null); // 你可以在这里添加 SQL 错误编号
            });
        
           if (!optionsBuilder.IsConfigured)
           {
               IConfigurationRoot Configuration = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json").Build();
                optionsBuilder.UseSqlServer(Configuration.GetConnectionString("Babyciao"));
           }
        }
    }
}
