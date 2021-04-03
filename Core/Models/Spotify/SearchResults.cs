using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Core.Models.Spotify
{
    [DataContract]
    public class SearchResults
    {
        [DataMember(Name = "tracks")]
        public PagingObject<TrackObject>? Tracks { get; set; }
    }
}
