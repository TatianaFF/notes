using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerService.Interfaces
{
    public interface IFileProcessor
    {
        //FileToDataTable
        //DataTables SplitDt
        //SaveFile
        void ProcessFiles(string inputDirectory, string outputFilePath);
        string GetSupportedExtension();
    }
}
