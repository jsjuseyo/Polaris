using bwaPolaris.Shared;
using Microsoft.EntityFrameworkCore;

namespace bwaPolaris.Data
{
    public class PolarisDbContext : DbContext, IDbContext
    {
        public PolarisDbContext(DbContextOptions options) : base(options) { }
        public DbSet<T0Form> T0Form { get; set; }
        public DbSet<T1AtributForm> T1AtributForm { get; set; }
        public DbSet<T0Jabatan> T0Jabatan { get; set; }
        public DbSet<T1Staf> T1Staf { get; set; }
        public DbSet<T5Jabatan_Staf> T5Jabatan_Staf { get; set; }
        public DbSet<T9User> T9User { get; set; }
        public DbSet<T9Privileges> T9Privileges { get; set; }
        public DbSet<T9DataOption> T9DataOption { get; set; }

        public override DbSet<TEntity> Set<TEntity>()
        {
            return base.Set<TEntity>();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
