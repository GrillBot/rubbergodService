using Microsoft.EntityFrameworkCore;
using RubbergodService.Data.Entity;

namespace RubbergodService.Data.Repository;

public sealed class RubbergodServiceRepository : IDisposable, IAsyncDisposable
{
    private RubbergodServiceContext Context { get; set; }
    private List<RepositoryBase> Repositories { get; set; } = new();

    public RubbergodServiceRepository(RubbergodServiceContext context)
    {
        Context = context;
    }

    public KarmaRepository Karma => GetOrCreateRepository<KarmaRepository>();
    public MemberCacheRepository MemberCache => GetOrCreateRepository<MemberCacheRepository>();

    private TRepository GetOrCreateRepository<TRepository>() where TRepository : RepositoryBase
    {
        var repository = Repositories.OfType<TRepository>().FirstOrDefault();
        if (repository != null)
            return repository;

        repository = Activator.CreateInstance(typeof(TRepository), Context) as TRepository;
        if (repository == null)
            throw new InvalidOperationException($"Error while creating repository {typeof(TRepository).Name}");

        Repositories.Add(repository);

        return repository;
    }

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

        Repositories.Clear();
        Repositories = null!;
    }

    public async ValueTask DisposeAsync()
    {
        Repositories.Clear();
        Repositories = null!;

        Context.ChangeTracker.Clear();
        await Context.DisposeAsync();
        Context = null!;
    }
}
