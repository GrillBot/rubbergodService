﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RubbergodService.Data.Entity;

public class Karma
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [JsonPropertyName("member_ID")]
    [StringLength(32)]
    public string MemberId { get; set; } = null!;

    [JsonPropertyName("karma")]
    public int KarmaValue { get; set; }
    
    [JsonPropertyName("positive")]
    public int Positive { get; set; }
    
    [JsonPropertyName("negative")]
    public int Negative { get; set; }
}
