https://learn.microsoft.com/ru-ru/dotnet/csharp/asynchronous-programming/

var tasks = new List<Task>();

string baseName = "file_n";
int tasksCount = 2;
for (int i = 0; i < tasksCount; i++)
{
    tasks.Add(Task.Run(() => Save(baseName+"_"+i)));
}

Task _tasks = Task.WhenAll(tasks);
try
{
    _tasks.Wait();
}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString()); 
}
if (_tasks.Status == TaskStatus.RanToCompletion)
    Console.WriteLine("Все задачи завершены");
else if (_tasks.Status == TaskStatus.Faulted)
{

    Console.WriteLine("Ошибка ебать");
    for (int i = 0; i < tasksCount; i++)
    {
        try 
        {
            if (File.Exists(baseName+"_"+i))
                File.Delete(baseName+"_"+i);
        }   
        catch { }
    }
}

static void Save(string baseName)
{
    try
    {
        File.Create(baseName);
        throw new Exception("NOT SAVED");
    }
    catch (Exception ex)
    {
        throw;
    }
}


Console.ReadLine();
