using System.Collections.Generic;
using System.Text.Json.Serialization;
using RestWith.NET.Hypermedia;
using RestWith.NET.Hypermedia.Abstract;

namespace RestWith.NET.Data.VO
{
    public class PersonVoOutput: ISupportsHyperMedia
    {  
        
        public long Id{get;set;}
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Adress { get; set; }
        public string Gender { get; set; }
        public bool Enable { get; set; }

        public List<HyperMediaLink> Links {get;set; } = new List<HyperMediaLink>();
    }
}
