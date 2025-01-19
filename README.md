using System;
using System.Data;
using System.Globalization;
using System.IO;
using CsvHelper;

class Program
{
    static void Main(string[] args)
    {
        string csvFilePath = @"C:PathToYourFile.csv"; // Укажите путь к вашему CSV файлу

        DataTable dataTable = new DataTable();

        try
        {
            using (var reader = new StreamReader(csvFilePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                // Чтение заголовков
                csv.Read();
                csv.ReadHeader();
                foreach (var header in csv.HeaderRecord)
                {
                    dataTable.Columns.Add(header);
                }

                // Чтение данных
                while (csv.Read())
                {
                    var row = dataTable.NewRow();
                    foreach (var header in csv.HeaderRecord)
                    {
                        row[header] = csv.GetField(header);
                    }
                    dataTable.Rows.Add(row);
                }
            }

            Console.WriteLine("CSV файл успешно прочитан в DataTable.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Произошла ошибка: " + ex.Message);
        }
    }
}









using System;
using System.Data;
using System.Globalization;
using System.IO;
using CsvHelper;

class Program
{
    static void Main(string[] args)
    {
        // Создаем пример DataTable
        DataTable dataTable = new DataTable("ExampleTable");
        dataTable.Columns.Add("Id", typeof(int));
        dataTable.Columns.Add("Name", typeof(string));
        dataTable.Columns.Add("Age", typeof(int));

        // Добавляем несколько строк
        dataTable.Rows.Add(1, "Alice", 30);
        dataTable.Rows.Add(2, "Bob", 25);
        dataTable.Rows.Add(3, "Charlie", 35);

        // Указываем путь для сохранения CSV файла
        string csvFilePath = @"C:PathToYourFile.csv"; // Замените на нужный путь

        try
        {
            using (var writer = new StreamWriter(csvFilePath))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                // Записываем заголовки
                foreach (DataColumn column in dataTable.Columns)
                {
                    csv.WriteField(column.ColumnName);
                }
                csv.NextRecord();

                // Записываем строки
                foreach (DataRow row in dataTable.Rows)
                {
                    for (int i = 0; i < dataTable.Columns.Count; i++)
                    {
                        csv.WriteField(row[i]);
                    }
                    csv.NextRecord();
                }
            }

            Console.WriteLine("CSV файл успешно сохранен.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Произошла ошибка: " + ex.Message);
        }
    }
}
