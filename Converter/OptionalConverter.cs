using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace dotnet.Converter
{
    public class OptionalConverter<T> : JsonConverter<Optional<T>> where T : new()
    {
        public override Optional<T> ReadJson(JsonReader reader, Type objectType, Optional<T> existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            return Optional<T>.Of(new T());
        }

        public override void WriteJson(JsonWriter writer, Optional<T> value, JsonSerializer serializer)
        {
            var optionalObject = (JObject)JToken.FromObject(value.Get());
            optionalObject.WriteTo(writer);
        }

    }
}