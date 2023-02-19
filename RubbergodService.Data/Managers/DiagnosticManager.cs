using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using RubbergodService.Data.Models.Diagnostics;

namespace RubbergodService.Data.Managers;

public class DiagnosticManager
{
    private SemaphoreSlim Semaphore { get; }
    private List<RequestStatistics> Statistics { get; } = new();

    public DiagnosticManager()
    {
        Semaphore = new SemaphoreSlim(1);
    }

    public async Task OnRequestEndAsync(HttpContext context, DateTime startAt)
    {
        var url = context.Request.Path.ToString();
        var duration = Convert.ToInt32((DateTime.Now - startAt).TotalMilliseconds);

        await Semaphore.WaitAsync();
        try
        {
            var statistics = Statistics.Find(o => o.Endpoint == url);
            if (statistics is null)
            {
                statistics = new RequestStatistics { Endpoint = url };
                Statistics.Add(statistics);
            }

            statistics.Count++;
            statistics.LastRequestAt = DateTime.Now;
            statistics.LastTime = duration;
            statistics.TotalTime += duration;
        }
        finally
        {
            Semaphore.Release();
        }
    }

    public DiagnosticInfo GetInfo()
    {
        var process = Process.GetCurrentProcess();

        return new DiagnosticInfo
        {
            Endpoints = Statistics,
            Uptime = Convert.ToInt64((DateTime.Now - process.StartTime).TotalMilliseconds),
            Version = GetType().Assembly.GetName().Version!.ToString(),
            MeasuredFrom = process.StartTime,
            RequestsCount = Statistics.Sum(o => o.Count),
            UsedMemory = process.WorkingSet64
        };
    }
}
