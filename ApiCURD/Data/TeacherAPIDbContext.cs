using ApiCURD.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCURD.Data
{
    public class TeacherAPIDbContext : DbContext
    {
        public TeacherAPIDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Teacher> Teachers { get; set; }
    }
}
