using LoadIBKData.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadIBKData.Data
{
    public class APIDbContext : DbContext
    {
        public APIDbContext(DbContextOptions<APIDbContext> options) : base(options)
        {
        }

        public DbSet<Price> Prices { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    //Log from SQL debug even for performance tuning
        //    //optionsBuilder.LogTo(log => Debug.WriteLine(log));
        //    optionsBuilder.UseSqlServer(
        //        "Data Source=localhost;Initial Catalog=IBKData;Trusted_Connection=True;MultipleActiveResultSets=true")
        //        .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name//,
        //                                         //DbLoggerCategory.Database.Transaction.Name
        //                                       },
        //               LogLevel.Information)
        //        .EnableSensitiveDataLogging();
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
