using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using System.Text.Json;

namespace Tennis.Core.Entities.Converter
{
    public class IntListConverter : IPropertyConverter
    {
        public DynamoDBEntry ToEntry(object value)
        {
            if (value == null)
                return new Primitive("[]");

            var json = JsonSerializer.Serialize(value);
            return new Primitive(json);
        }

        public object FromEntry(DynamoDBEntry entry)
        {
            if (entry == null || string.IsNullOrEmpty(entry.AsString()))
                return new List<int>();

            try
            {
                return JsonSerializer.Deserialize<List<int>>(entry.AsString()) ?? new List<int>();
            }
            catch
            {
                return new List<int>();
            }
        }
    }
}
