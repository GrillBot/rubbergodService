using RubbergodService.Data.Entity;

namespace RubbergodService.Data.Repository;

public abstract class RepositoryBase
{
    protected RubbergodServiceContext Context { get; }

    protected RepositoryBase(RubbergodServiceContext context)
    {
        Context = context;
    }
}
