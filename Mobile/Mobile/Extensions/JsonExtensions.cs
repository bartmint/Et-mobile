using Mobile.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Mobile.Extensions
{
    public static class JsonExtensions
    {
        public static T Deserialize<T>(this string json)
        {
            return JsonSerializer.Deserialize<T>(json, JsonOptions.DeserializeCamelCase);
        }

        public static T DeserializeCustom<T>(this string json, JsonSerializerOptions settings)
        {
            return JsonSerializer.Deserialize<T>(json, settings);
        }

        public static T DeserializeCamelCase<T>(this string json)
        {
            return JsonSerializer.Deserialize<T>(json, JsonOptions.DeserializeCamelCase);
        }
    }
}
