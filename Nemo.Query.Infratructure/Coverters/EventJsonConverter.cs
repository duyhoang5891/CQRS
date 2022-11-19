using CQRS.Core.Events;
using Nemo.Common.Events;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Nemo.Query.Infratructure.Coverters
{
    public class EventJsonConverter : JsonConverter<BaseEvent>
    {
        public EventJsonConverter()
        {
        }

        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert.IsAssignableFrom(typeof(BaseEvent));
        }

        public override BaseEvent? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if(!JsonDocument.TryParseValue(ref reader, out var doc))
            {
                throw new JsonException($"Failed to parse {nameof(JsonDocument)}!");
            }

            if(!doc.RootElement.TryGetProperty("Type", out var type))
            {
                throw new JsonException("Could not detect the Type discriminator property!");
            }

            var typeDisciminator = type.GetString();

            var json = doc.RootElement.GetRawText();

            return typeDisciminator switch
            {
                nameof(CreatedItemEvent) => JsonSerializer.Deserialize<CreatedItemEvent>(json, options),
                nameof(DeleteItemEvent) => JsonSerializer.Deserialize<DeleteItemEvent>(json, options),
                nameof(UpdateItemEvent) => JsonSerializer.Deserialize<UpdateItemEvent>(json, options),
                _ => throw new JsonException($"{typeDisciminator} not support yet!")
            };
        }

        public override void Write(Utf8JsonWriter writer, BaseEvent value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}

