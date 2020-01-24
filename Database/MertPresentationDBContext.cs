using MertPresentation.Models;
using Microsoft.EntityFrameworkCore;

namespace MertPresentation.Database
{
    public class MertPresentationDBContext : DbContext
    {
        public MertPresentationDBContext(DbContextOptions<MertPresentationDBContext> options) : base(options)
        {
        }

        public DbSet<Missions> Missions { get; set; }
        public DbSet<People> People { get; set; }
    }
}