﻿using Microsoft.EntityFrameworkCore;

namespace RubbergodService.Data.Entity;

public class RubbergodServiceContext : DbContext
{
    public RubbergodServiceContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Karma> Karma => Set<Karma>();
    public DbSet<MemberCacheItem> MemberCache => Set<MemberCacheItem>();
}
