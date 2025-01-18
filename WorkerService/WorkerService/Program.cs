using WorkerService;
using WorkerService.FileHanglers;
using WorkerService.Interfaces;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();

CreateHostBuilder(args).Build().Run();

static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                    //добавить IFileProcessingService
                    services.AddTransient<IFileProcessor, CsvFileProcessor>();
                });