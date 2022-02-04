using System.Collections.Generic;


namespace RestWith.NET.Hypermedia.Abstract
{
    public interface ISupportsHyperMedia
    {
        List<HyperMediaLink> Links{get;set;}
        
    }
}