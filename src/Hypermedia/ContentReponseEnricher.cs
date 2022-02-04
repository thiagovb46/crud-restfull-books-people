using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;
using RestWith.NET.Hypermedia.Abstract;
using src.Hypermedia;

namespace RestWith.NET.Hypermedia
{
    public abstract class ContentReponseEnricher<T> : IResponseEnricher where T : ISupportsHyperMedia
    {
        public bool CanEnrich(Type contentType)
        {
            return contentType == typeof(T) ||  contentType == typeof(List<T>) ||    contentType == typeof(PagedSearchVo<T>);
        }
        protected abstract Task EnrichModel(T content, IUrlHelper urlHelper);

        public async Task Enrich(ResultExecutingContext response)
        {
            var urlHelper = new UrlHelperFactory().GetUrlHelper(response);
            if(response.Result is OkObjectResult okObjectResult)
            {
                if(okObjectResult.Value is T model)
                {
                    await EnrichModel(model, urlHelper);
                }
                else if(okObjectResult.Value is List<T> collection)
                {
                    ConcurrentBag<T> bag = new ConcurrentBag<T>(collection);
                    Parallel.ForEach(bag, (element)=>
                    {
                        EnrichModel(element, urlHelper);
                    }
                    );
                }
                 else if(okObjectResult.Value is PagedSearchVo<T> pagedSearch)
                {
                    
                    Parallel.ForEach(pagedSearch.List, (element)=>
                    {
                        EnrichModel(element, urlHelper);
                    }
                    );
                }
            }
        await Task.FromResult<object>(null);
        }
        bool IResponseEnricher.CanEnrich(ResultExecutingContext response)
        {
            if(response.Result is OkObjectResult okObjectResult)
            {
                return CanEnrich(okObjectResult.Value.GetType ());
            }
            return false;
        }

        public bool CanEnrich(ResultExecutingContext context)
        {
            throw new NotImplementedException();
        }
    }
}