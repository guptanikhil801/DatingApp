using Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.DataAccess
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        /// <summary>
        /// It creates Table of AppUser in Database
        /// </summary>
        public DbSet<AppUser> Users { get; set; }
    }
}
