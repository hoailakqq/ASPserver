using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApplication23.Entities;
using WebApplication23.Models.Table;

namespace WebApplication23.Models
{
    public class DataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            var configuration = builder.Build();
            optionsBuilder.UseMySql(configuration["ConnectionStrings:DefaultConnection"]);
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categorys { get; set; }
        public DbSet<Friend> Friends { get; set; }
        public DbSet<Lession> Lessions { get; set; }
        public DbSet<Overview> Overviews { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<QuestionModel> Questions { get; set; }
        public DbSet<Rank> ranks { get; set; }
        public DbSet<role_user> role_users { get; set; }
        public DbSet<Streak> Streaks { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<Logsys> Logsys { get; set; }
        public DbSet<Dictionary> Dictionaries { get; set; }

    }
}
