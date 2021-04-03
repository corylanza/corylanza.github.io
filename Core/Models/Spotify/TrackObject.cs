using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Core.Models.Spotify
{
    [DataContract]
    public class TrackObject
    {
        [DataMember(Name = "album"), NotNull]
        public SimplifiedAlbumObject? Album { get; set; }

        [DataMember(Name = "artists"), NotNull]
        public IEnumerable<ArtistObject>? Artists { get; set; }

        [DataMember(Name = "available_markets"), NotNull]
        public IEnumerable<string>? AvailableMarkets { get; set; }

        [DataMember(Name = "id"), NotNull]
        public string? Id { get; set; }

        [DataMember(Name = "name"), NotNull]
        public string? Name { get; set; }

        [DataMember(Name = "uri"), NotNull]
        public string? Uri { get; set; }

        [DataMember(Name = "preview_url")]
        public string? PreviewUrl { get; set; }
    }
}
