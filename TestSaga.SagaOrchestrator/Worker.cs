namespace TestSaga.SagaOrchestrator;

public class Worker(ILogger<Worker> logger) : BackgroundService
{
	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		var nextLogTime = DateTimeOffset.Now;

		while (!stoppingToken.IsCancellationRequested)
		{
			if (DateTimeOffset.Now >= nextLogTime)
			{
				if (logger.IsEnabled(LogLevel.Information))
				{
					logger.LogInformation("Orchestrator Worker running at: {time}", DateTimeOffset.Now);
				}
				nextLogTime = DateTimeOffset.Now.AddSeconds(30); // Log every 10 seconds
			}
			await Task.Delay(1000, stoppingToken);
		}
	}
}
