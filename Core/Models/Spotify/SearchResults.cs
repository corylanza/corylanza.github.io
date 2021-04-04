using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Core.Models.Spotify
{
    [DataContract]
    public class SearchResults
    {
        [DataMember(Name = "tracks"), NotNull]
        public PagingObject<TrackObject>? Tracks { get; set; }

        [DataMember(Name = "artists"), NotNull]
        public PagingObject<ArtistObject>? Artists { get; set; }

        [DataMember(Name = "albums"), NotNull]
        public PagingObject<SimplifiedAlbumObject>? Albums { get; set; }
    }
}
