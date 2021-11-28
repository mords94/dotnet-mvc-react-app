using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace dotnet.ViewModel.Paging
{
    public class Page<T>
    {
        public Page(IEnumerable<T> content, Pageable pageable, long count)
        {
            Content = content;
            Pageable = pageable;
            TotalElements = count;
            TotalPages = (int)Math.Ceiling(count / (double)pageable.Page);
            Last = TotalPages == Pageable.Page;
            Empty = count == 0;
            Size = Pageable.Size;
            NumberOfElements = content.Count();
        }

        [JsonProperty("content")]
        public IEnumerable<T> Content { get; set; }

        [JsonProperty("pageable")]
        public Pageable Pageable;

        [JsonProperty("totalPages")]

        public int TotalPages { get; set; }

        [JsonProperty("totalElements")]
        public long TotalElements { get; set; }

        [JsonProperty("numberOfElements")]
        public int NumberOfElements { get; set; }

        [JsonProperty("last")]
        public bool Last { get; set; }

        [JsonProperty("size")]
        public int Size { get; set; }

        [JsonProperty("empty")]
        public bool Empty { get; set; }
    }
}