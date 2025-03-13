using System.Reflection;

namespace SearchQuery
{
    /// <summary>
    /// Represents a search interface with JSON converters for both Newtonsoft.Json and System.Text.Json.
    /// </summary>
    [Newtonsoft.Json.JsonConverter(
        typeof(NewtonsoftJson.ConditionsConverter))]
    [System.Text.Json.Serialization.JsonConverter(
        typeof(SystemTextJson.ConditionsConverter))]
    public interface ISearch { }
}

namespace SearchQuery.NewtonsoftJson
{
    /// <summary>
    /// JSON converter for the Search class using Newtonsoft.Json.
    /// </summary>
    internal class SearchConverter : Newtonsoft.Json.JsonConverter
    {
        /// <summary>
        /// Determines whether this instance can convert the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns>true if this instance can convert the specified object type; otherwise, false.</returns>
        public override bool CanConvert(Type objectType)
        {
            return typeof(Search).IsAssignableFrom(objectType) || 
                typeof(Query).IsAssignableFrom(objectType);
        }

        /// <summary>
        /// Reads the JSON representation of the object.
        /// </summary>
        /// <param name="reader">The Newtonsoft.Json.JsonReader to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">The existing value of object being read.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>The object value.</returns>
        public override object ReadJson(Newtonsoft.Json.JsonReader reader, 
            Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            var jsonObject = Newtonsoft.Json.Linq.JObject.Load(reader);
            var instance = Activator.CreateInstance(objectType);
            if (instance is null) return null;
            var properties = objectType.GetProperties(
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);

            foreach (var property in properties)
            {
                Newtonsoft.Json.Linq.JToken token;
                if (jsonObject.TryGetValue(property.Name, StringComparison.OrdinalIgnoreCase, out token))
                {
                    object value = token.ToObject(property.PropertyType, serializer);
                    property.SetValue(instance, value);
                }
            }

            return instance;
        }

        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The Newtonsoft.Json.JsonWriter to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(Newtonsoft.Json.JsonWriter writer,
            object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            Type objectType = value.GetType();
            writer.WriteStartObject();
            foreach (var property in objectType
                .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase))
            {
                var propertyName = property.Name;
                var propertyValue = property.GetValue(value);

                writer.WritePropertyName(propertyName);

                if (propertyValue == null)
                {
                    writer.WriteNull();
                }
                else
                {
                    serializer.Serialize(writer, propertyValue, property.PropertyType);
                }
            }
            writer.WriteEndObject();
        }
    }

    internal class ConditionsConverter : Newtonsoft.Json.JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(ISearch);
        }

        public override object ReadJson(Newtonsoft.Json.JsonReader reader, 
            Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            var jsonObject = Newtonsoft.Json.Linq.JObject.Load(reader);
            if (jsonObject.Properties()
                     .Any(p => string.Equals(p.Name, nameof(Query.Conditions), 
                        StringComparison.OrdinalIgnoreCase))) {
                var item = new Query();
                serializer.Populate(jsonObject.CreateReader(), item);
                return item;
            } else {
                var item = new Condition();
                serializer.Populate(jsonObject.CreateReader(), item);
                return item;
            }
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, 
            object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            Type objectType = value.GetType();
            writer.WriteStartObject();
            foreach (var property in objectType
                .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase))
            {
                var propertyName = property.Name;
                var propertyValue = property.GetValue(value);

                writer.WritePropertyName(propertyName);

                if (propertyValue == null)
                {
                    writer.WriteNull();
                }
                else
                {
                    serializer.Serialize(writer, propertyValue, property.PropertyType);
                }
            }
            writer.WriteEndObject();
        }
    }
}

namespace SearchQuery.SystemTextJson
{
    internal class SearchConverter : System.Text.Json.Serialization.JsonConverter<object>
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeof(Search).IsAssignableFrom(typeToConvert) || 
                   typeof(Query).IsAssignableFrom(typeToConvert);
        } 

        public override object? Read(ref System.Text.Json.Utf8JsonReader reader, 
            Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
        {
            using (System.Text.Json.JsonDocument document = 
                System.Text.Json.JsonDocument.ParseValue(ref reader))
            {
                var instance = Activator.CreateInstance(typeToConvert);
                if (instance is null) return null;

                var properties = typeToConvert.GetProperties(
                    BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);

                foreach (var property in properties)
                {
                    if (document.RootElement.TryGetProperty(property.Name, 
                        out System.Text.Json.JsonElement jsonElement))
                    {
                        object? value = System.Text.Json.JsonSerializer
                            .Deserialize(jsonElement.GetRawText(), property.PropertyType, options);
                        property.SetValue(instance, value);
                    }
                }

                return instance;
            }
        }

        public override void Write(System.Text.Json.Utf8JsonWriter writer, 
            object value, System.Text.Json.JsonSerializerOptions options)
        {
            Type objectType = value.GetType();

            writer.WriteStartObject();

            foreach (var property in objectType.GetProperties(
                         BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase))
            {
                var propertyName = property.Name;
                var propertyValue = property.GetValue(value);

                writer.WritePropertyName(propertyName);

                if (propertyValue == null)
                {
                    writer.WriteNullValue();
                }
                else
                {
                    System.Text.Json.JsonSerializer.Serialize(writer, propertyValue, property.PropertyType, options);
                }
            }

            writer.WriteEndObject();
        }
    }

    internal class ConditionsConverter : System.Text.Json.Serialization.JsonConverter<ISearch>
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(ISearch);
        }

        public override ISearch? Read(ref System.Text.Json.Utf8JsonReader reader, 
            Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
        {
            using (System.Text.Json.JsonDocument document = 
                System.Text.Json.JsonDocument.ParseValue(ref reader))
            {
                System.Text.Json.JsonElement root = document.RootElement;

                if (root.EnumerateObject().Any(p => string.Equals(p.Name, 
                    nameof(Query.Conditions), StringComparison.OrdinalIgnoreCase)))
                {
                    return System.Text.Json.JsonSerializer.Deserialize<Query>(root.GetRawText(), options);
                }
                else
                {
                    return System.Text.Json.JsonSerializer.Deserialize<Condition>(root.GetRawText(), options);
                }
            }
        }

        public override void Write(System.Text.Json.Utf8JsonWriter writer, 
            ISearch value, System.Text.Json.JsonSerializerOptions options)
        {
            Type objectType = value.GetType();

            writer.WriteStartObject();

            foreach (var property in objectType.GetProperties(
                         BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase))
            {
                var propertyName = property.Name;
                var propertyValue = property.GetValue(value);

                writer.WritePropertyName(propertyName);

                if (propertyValue == null)
                {
                    writer.WriteNullValue();
                }
                else
                {
                    System.Text.Json.JsonSerializer.Serialize(writer, propertyValue, property.PropertyType, options);
                }
            }

            writer.WriteEndObject();
        }
    }
}