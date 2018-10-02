using LanguageProcessor.Model;
using System.Data.Entity;

namespace LanguageProcessor.ViewModel
{
    class LanguageDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
    }
}
