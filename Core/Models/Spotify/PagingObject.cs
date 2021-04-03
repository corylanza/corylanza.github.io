using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Core.Models.Spotify
{
    [DataContract]
    public class PagingObject<T>
    {
        [DataMember(Name = "items"), NotNull]
        public IEnumerable<T>? Items { get; set; }

        [DataMember(Name = "previous")]
        public string? Previous { get; set; }

        [DataMember(Name = "href"), NotNull]
        public string? Href { get; set; }

        [DataMember(Name = "next")]
        public string? Next { get; set; }

        [DataMember(Name = "limit")]
        public int Limit { get; set; }

        [DataMember(Name = "offset")]
        public int Offset { get; set; }

        [DataMember(Name = "total")]
        public int Total { get; set; }
    }
}
