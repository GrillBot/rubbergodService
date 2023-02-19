using Microsoft.EntityFrameworkCore;
using RubbergodService.Data.Entity;

namespace RubbergodService.Data.Repository;

public class MemberCacheRepository : RepositoryBase
{
    public MemberCacheRepository(RubbergodServiceContext context) : base(context)
    {
    }

    public async Task<MemberCacheItem?> FindMemberByIdAsync(string memberId)
    {
        return await Context.MemberCache
            .FirstOrDefaultAsync(o => o.UserId == memberId);
    }
}
