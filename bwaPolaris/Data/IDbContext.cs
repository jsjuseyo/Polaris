using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace bwaPolaris.Data
{
    public interface IDbContext
    {
        // Definisikan metode yang Anda butuhkan, contohnya:
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        // Tambahkan metode lain yang relevan dengan operasi database Anda.
    }
}
