﻿namespace RubbergodService.Data.Models.Common;

public class User
{
    public string Id { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string Discriminator { get; set; } = null!;
    public string AvatarUrl { get; set; } = null!;
}
