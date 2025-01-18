using WorkerService.Services;

namespace WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly FileProcessingService _fileProcessingService;


        public Worker(ILogger<Worker> logger, FileProcessingService fileProcessingService)
        {
            _logger = logger;
            _fileProcessingService = fileProcessingService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    string inputDirectory = "path_to_input_directory";
                    string outputFilePath = "path_to_output_file/output.csv";

                    _fileProcessingService.Process(inputDirectory, outputFilePath);
                    _logger.LogInformation("Files processed successfully.");
                }
                catch (Exception ex) {
                    _logger.LogError(ex, "An error occurred while processing files.");
                }

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
    }
}
