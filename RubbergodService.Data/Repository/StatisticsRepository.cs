using Microsoft.EntityFrameworkCore;
using RubbergodService.Data.Entity;

namespace RubbergodService.Data.Repository;

public class StatisticsRepository : RepositoryBase
{
    public StatisticsRepository(RubbergodServiceContext context) : base(context)
    {
    }

    public async Task<Dictionary<string, long>> GetStatisticsAsync()
    {
        return new Dictionary<string, long>
        {
            { nameof(Context.Karma), await Context.Karma.CountAsync() },
            { nameof(Context.MemberCache), await Context.MemberCache.CountAsync() }
        };
    }
}
