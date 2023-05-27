using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace QuanLyKhoConsoleApp.Model
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(@"Data Source=.;Initial Catalog=QuanLyKho;Integrated Security=True");
        }

        public DbSet<Aseet> Aseet { get; set; }
        public DbSet<export> export { get; set; }
        public DbSet<warehouse> warehouse { get; set; }
    }
}
