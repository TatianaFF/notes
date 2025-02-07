using System.IO;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

public async Task<MemoryStream> ConvertRtfToPdf(string rtfFilePath)
{
    // Читаем RTF файл
    string rtfContent = await File.ReadAllTextAsync(rtfFilePath);
    
    // Создаем MemoryStream для PDF
    var pdfStream = new MemoryStream();
    
    // Создаем документ PDF
    using (PdfWriter writer = new PdfWriter(pdfStream))
    {
        using (PdfDocument pdf = new PdfDocument(writer))
        {
            Document document = new Document(pdf);
            // Здесь вы должны добавить логику для преобразования RTF в элементы PDF.
            // Это может быть сложным и требует дополнительных библиотек.
            // Например, вы можете использовать библиотеку для парсинга RTF и добавления элементов в документ.
            
            // Пример добавления текста (замените на логику преобразования RTF)
            document.Add(new Paragraph(rtfContent));
        }
    }
    
    pdfStream.Position = 0; // Сброс позиции потока
    return pdfStream;
}


@page "/displaypdf"
@inject IJSRuntime JSRuntime

<button @onclick="DisplayPdf">Показать PDF</button>

@code {
    private async Task DisplayPdf()
    {
        var pdfStream = await ConvertRtfToPdf("path/to/your/file.rtf");

        // Создаем Blob URL
        var buffer = pdfStream.ToArray();
        var base64 = Convert.ToBase64String(buffer);
        var blobUrl = $"data:application/pdf;base64,{base64}";

        // Открываем PDF в новом окне
        await JSRuntime.InvokeVoidAsync("open", blobUrl, "_blank");
    }

    private async Task<MemoryStream> ConvertRtfToPdf(string rtfFilePath)
    {
        // Ваш метод преобразования RTF в PDF
    }
}


