using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace RubbergodService.Data.Models.Common;

/// <summary>
/// Parameters for pagination.
/// </summary>
public class PaginatedParams
{
    /// <summary>
    /// Page.
    /// </summary>
    [Range(0, int.MaxValue, ErrorMessage = "Page number is in invalid range.")]
    public int Page { get; set; }

    /// <summary>
    /// Page size.
    /// </summary>
    [Range(0, int.MaxValue, ErrorMessage = "Page size is in invalid range.")]
    public int PageSize { get; set; } = 25;

    [JsonIgnore]
    public int Skip => Math.Max(Page, 0) * PageSize;
}
