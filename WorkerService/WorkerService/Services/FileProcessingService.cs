using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerService.Interfaces;

namespace WorkerService.Services
{
    public class FileProcessingService
    {
        private readonly IEnumerable<IFileProcessor> _fileProcessors;

        public FileProcessingService(IEnumerable<IFileProcessor> fileProcessors)
        {
            _fileProcessors = fileProcessors;
        }

        public void Process(string inputDirectory, string outputFilePath)
        {
            var files = Directory.GetFiles(inputDirectory);

            string extension = Path.GetExtension(files[0]).ToLowerInvariant();

            // Проверяем, имеют ли все файлы то же расширение
            bool allSameExtension = files.All(file => Path.GetExtension(file).ToLowerInvariant() == extension);

            if (!allSameExtension) throw new Exception("Все файлы должны быть одного расширения");

            var processor = GetProcessorForExtension(extension);

            if (processor == null) throw new Exception($"обработчик для работы с расширением {extension} не найден");

            processor.ProcessFiles(inputDirectory, outputFilePath);
        }

        private IFileProcessor? GetProcessorForExtension(string extension)
        {
            foreach (var processor in _fileProcessors)
            {
                if (processor.GetSupportedExtension().Equals(extension))
                {
                    return processor;
                }
            }
            return null;
        }
    }
}
