using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerService.Interfaces;

namespace WorkerService.FileHanglers
{
    public class CsvFileProcessor : IFileProcessor
    {
        public string GetSupportedExtension()
        {
            return ".csv";
        }

        public void ProcessFiles(string inputDirectory, string outputFilePath)
        {
            var csvFiles = Directory.GetFiles(inputDirectory, "*.csv");
            using var outputFile = new StreamWriter(outputFilePath);

            foreach (var file in csvFiles)
            {
                var content = File.ReadAllText(file);
                outputFile.WriteLine(content);
            }
        }
    }
}
