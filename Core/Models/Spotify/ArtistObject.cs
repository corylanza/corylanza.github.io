using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Core.Models.Spotify
{
    [DataContract]
    public class SimplifiedArtistObject
    {
        [DataMember(Name = "href"), NotNull]
        public string? Href { get; set; }

        [DataMember(Name = "id"), NotNull]
        public string? Id { get; set; }

        [DataMember(Name = "name"), NotNull]
        public string? Name { get; set; }

        [DataMember(Name = "type"), NotNull]
        public string? Type { get; set; }

        [DataMember(Name = "uri"), NotNull]
        public string? Uri { get; set; }
    }

    public class ArtistObject : SimplifiedArtistObject
    {
        
    }
}
