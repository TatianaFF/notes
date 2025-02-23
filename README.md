using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        // URL вашего экземпляра Elasticsearch и индекс, к которому вы хотите обратиться
        string elasticsearchUrl = "http://localhost:9200"; // Замените на ваш URL
        string indexName = "your_index_name"; // Замените на имя вашего индекса
        string apiKey = "your_api_key"; // Замените на ваш API-ключ

        using (HttpClient client = new HttpClient())
        {
            // Устанавливаем заголовок для формата JSON
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Добавляем API-ключ в заголовок Authorization
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("ApiKey", apiKey);

            try
            {
                // Формируем URL для запроса (например, получение всех документов из индекса)
                string url = $"{elasticsearchUrl}/{indexName}/_search";

                // Отправляем GET-запрос
                HttpResponseMessage response = await client.GetAsync(url);

                // Проверяем успешность ответа
                response.EnsureSuccessStatusCode();

                // Читаем ответ в виде строки
                string responseBody = await response.Content.ReadAsStringAsync();

                // Выводим результат
                Console.WriteLine(responseBody);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Ошибка запроса: {e.Message}");
            }
        }
    }
}




string searchJson = @"{
    ""query"": {
        ""match_all"": {}
    }
}";

StringContent content = new StringContent(searchJson, System.Text.Encoding.UTF8, "application/json");
HttpResponseMessage response = await client.PostAsync(url, content);
