using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Core.Models.Spotify
{
    public class SimplifiedAlbumObject
    {
        [DataMember(Name = "images"), NotNull]
        public IEnumerable<ImageObject>? Images { get; set; }
    }
}
