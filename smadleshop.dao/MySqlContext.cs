using Microsoft.EntityFrameworkCore;
using smadleshop.pojo;
using System;

namespace smadleshop.dao
{
    public class MySqlContext:DbContext
    {
        public MySqlContext(DbContextOptions<DbContext> options):base(options)
        {

        }
        public DbSet<BuyCarCommodity> BuyCarCommodity { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }
        public DbSet<ScoreCommodity> ScoreCommodity { get; set; }
        public DbSet<SiteArticle> SiteArticle { get; set; }
        public DbSet<SiteOrder> SiteOrder { get; set; }
        public DbSet<UserAddress> UserAddress { get; set; }
        public DbSet<SiteUser> SiteUser { get; set; }
        public DbSet<SiteOther> SiteOther { get; set; }
    }
}
