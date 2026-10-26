using System.Diagnostics;

namespace EmployeeManagementAPI.Services;

public static class PerformanceMeasurement
{
    public static async Task RunAsync()
    {
        var stopwatch = new Stopwatch();

        stopwatch.Start();

        await Task.Delay(1000);
        await Task.Delay(1500);

        stopwatch.Stop();

        var sequentialTime = stopwatch.ElapsedMilliseconds;

        stopwatch.Restart();

        await Task.WhenAll(
            Task.Delay(1000),
            Task.Delay(1500));

        stopwatch.Stop();

        var parallelTime = stopwatch.ElapsedMilliseconds;

        Console.WriteLine(
            $"Sequential Time: {sequentialTime} ms");

        Console.WriteLine(
            $"Parallel Time: {parallelTime} ms");
    }
}