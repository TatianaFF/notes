string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "in"); // Укажите путь к папке с CSV файлами
string outputFilePath = Path.Combine(Directory.GetCurrentDirectory(), "out", "bjnj.csv"); // Укажите путь для сохранения объединенного файла

DataTable dataTable = new DataTable();

try
{
    using (var reader = new StreamReader(folderPath))
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
