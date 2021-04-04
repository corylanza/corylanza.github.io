using Core.Models.Spotify;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface ISpotifyService
    {
        Task<T> GetUrl<T>(string url);
        Task<SearchResults> SearchFor(string query);
        Task<PagingObject<ArtistObject>> UsersTopPlayedArtists();
        Task<PagingObject<TrackObject>> UsersTopPlayedTracks();
    }

    public class SpotifyService : ISpotifyService
    {
        private HttpClient Http { get; set; }
        private const string BasePath = "https://api.spotify.com/v1";
        private string? AuthToken { get; set; }
        private Func<Task<string>> GetAuthToken { get; init; }

        const string AuthUrl = "https://accounts.spotify.com/authorize";
        const string ClientId = "b61eb6c4aebf42bd9f1115f8f6bde54e";
        const string Scopes = "user-top-read";
        const string CookieName = "spotify-token";

        public SpotifyService(HttpClient http, IJSRuntime js, NavigationManager nav)
        {
            Http = http;
            GetAuthToken = async () => {
                string? cookie = await js.GetCookie<string>(CookieName);
                if(string.IsNullOrEmpty(cookie))
                {
                    var redirectUri = nav.BaseUri + "spotify-callback";
                    var authUrl = $"{AuthUrl}?response_type=token&client_id={ClientId}&redirect_uri={redirectUri}&scope={Scopes}";
                    nav.NavigateTo(authUrl, forceLoad: true) ;
                    return "";
                } else
                {
                    return cookie;
                }
            };
        }

        public async Task<T> GetUrl<T>(string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            AuthToken ??= await GetAuthToken();
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", AuthToken);

            var response = await Http.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<T>();
        }

        private async Task<T> Get<T>(string query) => await GetUrl<T>(BasePath + query);

        public async Task<SearchResults> SearchFor(string query) =>
            await Get<SearchResults>($"/search?q={query}&type=album,artist,playlist,track,show,episode");

        public async Task<PagingObject<ArtistObject>> UsersTopPlayedArtists() =>
            await Get<PagingObject<ArtistObject>>($"/me/top/artists");

        public async Task<PagingObject<TrackObject>> UsersTopPlayedTracks() =>
            await Get<PagingObject<TrackObject>>($"/me/top/tracks");
    }
}
