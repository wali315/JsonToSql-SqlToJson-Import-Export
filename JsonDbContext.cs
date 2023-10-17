using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace JsontTask3
{
    public class JsonDbContext : DbContext
    {
        public DbSet<JsonModel> JsonModels { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            MyConfiguration config = new MyConfiguration();
            var connection = config.GetConnectionString();
            optionsBuilder.UseSqlServer(connection);
        }
    }
}
