using System.Text.Json;
using System.Text.Json.Serialization;

namespace ImportExport
{
    public class JsonDataImporter : DataImporter
    {
        protected override ImportResult ParseData(string rawData)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            options.Converters.Add(new JsonStringEnumConverter());
            
            return JsonSerializer.Deserialize<ImportResult>(rawData, options);
        }
    }
}