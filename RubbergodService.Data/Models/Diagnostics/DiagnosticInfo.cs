﻿namespace RubbergodService.Data.Models.Diagnostics;

public class DiagnosticInfo
{
    public long UsedMemory { get; set; }
    public long Uptime { get; set; }
    public string Version { get; set; } = null!;
    public long RequestsCount { get; set; }
    public DateTime MeasuredFrom { get; set; }
    public List<RequestStatistics> Endpoints { get; set; } = new();
    public long CpuTime { get; set; }
    public Dictionary<string, long> Database { get; set; } = new();
}
