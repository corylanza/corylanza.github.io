using Core.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            var httpClient = new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };
            builder.Services.AddTransient(sp => httpClient);
            builder.Services.AddSingleton<ISpotifyService, SpotifyService>();

            await builder.Build().RunAsync();
        }

        private static Dictionary<string, string> ParseHashParams(Uri uri)
        {
            var results = new Dictionary<string, string>();

            foreach(var part in uri.Fragment.TrimStart('#').Split('&'))
            {
                var parts = part.Split('=');
                results.Add(parts[0], parts[1]);
            }
            return results;
        }

        public static bool TryGetHashParameter<T>(this NavigationManager navManager, string key, [NotNullWhen(true)] out T? value)
        {
            var uri = navManager.ToAbsoluteUri(navManager.Uri);
            
            if (ParseHashParams(uri).TryGetValue(key, out var valueFromQueryString))
            {
                if (typeof(T) == typeof(int) && int.TryParse(valueFromQueryString, out var valueAsInt))
                {
                    value = (T)(object)valueAsInt;
                    return true;
                }

                if (typeof(T) == typeof(string))
                {
                    value = (T)(object)valueFromQueryString.ToString();
                    return true;
                }

                if (typeof(T) == typeof(decimal) && decimal.TryParse(valueFromQueryString, out var valueAsDecimal))
                {
                    value = (T)(object)valueAsDecimal;
                    return true;
                }
            }

            value = default;
            return false;
        }

        public static bool TryGetQueryString<T>(this NavigationManager navManager, string key, [NotNullWhen(true)] out T? value)
        {
            var uri = navManager.ToAbsoluteUri(navManager.Uri);

            if (QueryHelpers.ParseQuery(uri.Query).TryGetValue(key, out var valueFromQueryString))
            {
                if (typeof(T) == typeof(int) && int.TryParse(valueFromQueryString, out var valueAsInt))
                {
                    value = (T)(object)valueAsInt;
                    return true;
                }

                if (typeof(T) == typeof(string))
                {
                    value = (T)(object)valueFromQueryString.ToString();
                    return true;
                }

                if (typeof(T) == typeof(decimal) && decimal.TryParse(valueFromQueryString, out var valueAsDecimal))
                {
                    value = (T)(object)valueAsDecimal;
                    return true;
                }
            }

            value = default;
            return false;
        }

        public static async Task SetCookie<T>(this IJSRuntime js, string cname, T? value, int expiration_seconds)
        {
            await js.InvokeVoidAsync("blazorHelpers.writeCookie", new object[] { cname, value, expiration_seconds  });
        }

        public static async Task<T?> GetCookie<T>(this IJSRuntime js, string cname)
        {
            string? value = await js.InvokeAsync<string?>("blazorHelpers.readCookie", new string[] { cname });

            if (typeof(T) == typeof(int) && int.TryParse(value, out var valueAsInt))
            {
                return (T)(object)valueAsInt;
            }

            if (typeof(T) == typeof(string))
            {
                return (T)(object)value.ToString();
            }

            if (typeof(T) == typeof(decimal) && decimal.TryParse(value, out var valueAsDecimal))
            {
                return (T)(object)valueAsDecimal;
            }
            return default;
        }

        public static async Task ScrollToAsync(this IJSRuntime jsRuntime, string query)
        {
            await jsRuntime.InvokeVoidAsync("blazorHelpers.scrollToElement", query);
        }
    }
}
