using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using CQRS.Core.Events;

namespace Nemo.Query.Infratructure.Coverters
{
    public class EventJsonConverter:JsonConverter<BaseEvent>
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
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, BaseEvent value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}

