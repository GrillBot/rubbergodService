using Microsoft.EntityFrameworkCore;
using RubbergodService.Data.Entity;
using RubbergodService.Data.Models.Common;

namespace RubbergodService.Data.Repository;

public class KarmaRepository : RepositoryBase
{
    public KarmaRepository(RubbergodServiceContext context) : base(context)
    {
    }

    public async Task<Karma?> FindKarmaByMemberIdAsync(string memberId)
    {
        return await Context.Karma
            .FirstOrDefaultAsync(o => o.MemberId == memberId);
    }

    public async Task<PaginatedResponse<Karma>> GetKarmaPageAsync(PaginatedParams parameters)
    {
        return await PaginatedResponse<Karma>.CreateWithEntityAsync(Context.Karma.AsNoTracking(), parameters);
    }
}
