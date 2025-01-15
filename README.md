Для того чтобы вынести код из класса Program и использовать Dependency Injection (DI) в консольном приложении на C#, можно следовать следующей архитектуре. Мы будем использовать встроенный контейнер DI из .NET Core.

▎Шаги для реализации DI в консольном приложении

1. Создайте проект консольного приложения.

2. Добавьте необходимые NuGet пакеты. Если вы используете .NET Core или .NET 5/6, то DI уже встроен. Если нет, установите пакет Microsoft.Extensions.DependencyInjection.

3. Создайте интерфейсы и их реализации. Это позволит вам легко подменять реализации при необходимости.

4. Настройте контейнер DI.

5. Вызовите нужные сервисы в Main методе.

▎Пример структуры проекта

MyConsoleApp/
│
├── Program.cs
├── Services/
│   ├── IMyService.cs
│   └── MyService.cs
└── DependencyInjection/
    └── ServiceCollectionExtensions.cs


▎Пример кода

▎1. Интерфейс сервиса

// Services/IMyService.cs
public interface IMyService
{
    void Execute();
}


▎2. Реализация сервиса

// Services/MyService.cs
public class MyService : IMyService
{
    public void Execute()
    {
        Console.WriteLine("Service is executing...");
    }
}


▎3. Настройка DI

// DependencyInjection/ServiceCollectionExtensions.cs
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddTransient<IMyService, MyService>();
        return services;
    }
}


▎4. Основной класс программы

// Program.cs
using Microsoft.Extensions.DependencyInjection;

class Program
{
    static void Main(string[] args)
    {
        // Настройка DI контейнера
        var serviceProvider = new ServiceCollection()
            .AddApplicationServices()
            .BuildServiceProvider();

        // Получение экземпляра сервиса
        var myService = serviceProvider.GetService<IMyService>();

        // Вызов метода сервиса
        myService.Execute();
    }
}


▎Объяснение кода

• IMyService: Интерфейс, который определяет контракт для сервиса.

• MyService: Реализация интерфейса, которая содержит бизнес-логику.

• ServiceCollectionExtensions: Класс, который расширяет IServiceCollection для регистрации зависимостей.

• Program: В этом классе мы настраиваем DI контейнер, регистрируем сервисы и получаем их экземпляры для использования.

▎Заключение

Использование Dependency Injection позволяет вам легко управлять зависимостями в вашем приложении, улучшает тестируемость и делает код более чистым и поддерживаемым. Вы можете добавлять новые сервисы и менять их реализации без изменения основного кода программы
