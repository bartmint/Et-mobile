using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Mobile.Helpers
{
    public static class JsonOptions
    {
        public static JsonSerializerOptions DeserializeCamelCase =
             new JsonSerializerOptions(new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
    }
}
