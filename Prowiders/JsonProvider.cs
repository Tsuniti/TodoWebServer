using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace TodoWebServer.Prowiders;

public static class JsonProvider
{
    private static readonly JsonSerializerOptions _options;

    static JsonProvider()
    {
        _options = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
        };
    }
    public static string Serialize<T>(T obj) => JsonSerializer.Serialize(obj, _options);
    public static T Deserialize<T>(string jsonString) => JsonSerializer.Deserialize<T>(jsonString, _options);
}

