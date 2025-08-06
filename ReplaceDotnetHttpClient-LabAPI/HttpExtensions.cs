using System.Net;
using System.Reflection;
using System.Security.Authentication;

namespace ReplaceDotnetHttpClient_LabAPI;

public static class HttpExtensions
{
    public static void ReplaceDotnetHttpClient()
    {
        var field = typeof(DotnetHttp)
            .GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
            .FirstOrDefault(f => f.FieldType == typeof(HttpClient));

        if (field == null)
            throw new Exception("DotnetHttp.Client field not found.");

        var attr = typeof(FieldInfo).GetField("m_fieldAttributes", BindingFlags.NonPublic | BindingFlags.Instance);
        if (attr != null)
        {
            var attrs = (FieldAttributes)attr.GetValue(field)!;
            attr.SetValue(field, attrs & ~FieldAttributes.InitOnly);
        }

        field.SetValue(null, CreatePatchedClient());
    }

    private static HttpClient CreatePatchedClient()
    {
        var handler = new HttpClientHandler
        {
            UseProxy = true,
            Proxy = WebRequest.DefaultWebProxy,
            SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls13,
            AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
        };

        var client = new HttpClient(handler)
        {
            Timeout = TimeSpan.FromSeconds(15)
        };
        client.DefaultRequestHeaders.Add("User-Agent", "SCPSL");
        return client;
    }
    
}