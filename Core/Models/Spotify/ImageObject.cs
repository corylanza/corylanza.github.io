using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Core.Models.Spotify
{
    [DataContract]
    public class ImageObject
    {
        [DataMember(Name = "url"), NotNull]
        public string? Url { get; set; }

        [DataMember(Name = "height")]
        public int Height { get; set; }

        [DataMember(Name = "width")]
        public int Width { get; set; }
    }
}
