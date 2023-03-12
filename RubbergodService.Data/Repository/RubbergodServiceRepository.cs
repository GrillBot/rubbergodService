using Microsoft.EntityFrameworkCore;
using RubbergodService.Data.Entity;

namespace RubbergodService.Data.Repository;

public sealed class RubbergodServiceRepository : IDisposable, IAsyncDisposable
{
    private RubbergodServiceContext Context { get; set; }

    public RubbergodServiceRepository(RubbergodServiceContext context)
    {
        Context = context;

        Karma = new KarmaRepository(Context);
        MemberCache = new MemberCacheRepository(Context);
        Statistics = new StatisticsRepository(Context);
    }

    public KarmaRepository Karma { get; }
    public MemberCacheRepository MemberCache { get; }
    public StatisticsRepository Statistics { get; }

    public Task AddAsync<TEntity>(TEntity entity) where TEntity : class
        => Context.Set<TEntity>().AddAsync(entity).AsTask();

    public Task AddCollectionAsync<TEntity>(IEnumerable<TEntity> collection) where TEntity : class
        => Context.Set<TEntity>().AddRangeAsync(collection);

    public void Remove<TEntity>(TEntity entity) where TEntity : class
        => Context.Set<TEntity>().Remove(entity);

    public void RemoveCollection<TEntity>(IEnumerable<TEntity> collection) where TEntity : class
        => Context.Set<TEntity>().RemoveRange(collection);

    public Task<int> CommitAsync()
        => Context.SaveChangesAsync();

    public void ProcessMigrations()
    {
        if (Context.Database.GetPendingMigrations().Any())
            Context.Database.Migrate();
    }

    public void Dispose()
    {
        Context.ChangeTracker.Clear();
        Context.Dispose();
        Context = null!;
    }

    public async ValueTask DisposeAsync()
    {
        Context.ChangeTracker.Clear();
        await Context.DisposeAsync();
        Context = null!;
    }
}
