
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsCleanApi.Infrastructure.Persistence;

namespace ProductsCleanApi.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PerformanceController : ControllerBase
{
    private readonly IServiceScopeFactory _scopeFactory;

    public PerformanceController(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    [HttpGet("compare")]
    public async Task<IActionResult> Compare(
        CancellationToken cancellationToken)
    {
        var stopwatch = new Stopwatch();

        stopwatch.Start();

        await GetEmployeeCountAsync(cancellationToken);
        await GetEmployeeCountAsync(cancellationToken);

        stopwatch.Stop();
        var sequentialTime = stopwatch.ElapsedMilliseconds;

        stopwatch.Restart();

        await Task.WhenAll(
            GetEmployeeCountAsync(cancellationToken),
            GetEmployeeCountAsync(cancellationToken));

        stopwatch.Stop();
        var parallelTime = stopwatch.ElapsedMilliseconds;

        var improvement = sequentialTime > 0
            ? (sequentialTime - parallelTime) * 100.0 / sequentialTime
            : 0;

        return Ok(new
        {
            SequentialTimeMs = sequentialTime,
            ParallelTimeMs = parallelTime,
            ImprovementPercentage = Math.Round(improvement, 2)
        });
    }

    private async Task<int> GetEmployeeCountAsync(
        CancellationToken cancellationToken)
    {
        await using var scope = _scopeFactory.CreateAsyncScope();

        var context = scope.ServiceProvider
            .GetRequiredService<AppDbContext>();

        return await context.Employees.CountAsync(cancellationToken);
    }
}
