//using Microsoft.EntityFrameworkCore;

//namespace BabyCiao.Models
//{
//    public partial class BabyCiaoContext : DbContext
//    {
//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//                IConfigurationRoot Configuration=new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json").Build();
//                optionsBuilder.UseSqlServer(Configuration.GetConnectionString("Babyciao"));
//            }
//        }
//    }
//}
