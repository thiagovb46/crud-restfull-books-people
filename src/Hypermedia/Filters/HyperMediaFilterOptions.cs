using System.Collections.Generic;
using RestWith.NET.Hypermedia.Abstract;

namespace RestWith.NET.Hypermedia.Filters
{
    public class HyperMediaFilterOptions
    {
        public List<IResponseEnricher> 
        ContentReponseEnricherList{get;set;} = new List<IResponseEnricher>();

    }
}