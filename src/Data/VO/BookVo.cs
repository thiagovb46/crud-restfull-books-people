using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using RestWith.NET.Hypermedia;
using RestWith.NET.Hypermedia.Abstract;

namespace RestWith.NET.Data.VO
{
    public class BookVo: ISupportsHyperMedia
    {
        [JsonIgnore]
        public long Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }
        public DateTime LaunchDate { get; set; }
        [JsonIgnore]
        public List<HyperMediaLink> Links { get;set;}  = new List<HyperMediaLink>();
    }
}