using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestWith.NET.Data.VO;
using RestWith.NET.Hypermedia.Constants;

namespace RestWith.NET.Hypermedia.Enricher
{
    public class PersonEnricher : ContentReponseEnricher<PersonVo>
    {
        private readonly object _lock = new object();
        protected override Task EnrichModel(PersonVo content, IUrlHelper urlHelper)
        {
           var path = "api/person/v1";
           string link = getLink(content.Id, urlHelper, path);
           content.Links.Add(new HyperMediaLink()
           {
               Action = HttpActionVerb.GET,
               Href = link,
               Rel =  RelationType.self,
               Type = ReponseTypeFormat.DefaultGet
           });

            content.Links.Add(new HyperMediaLink()
           {
               Action = HttpActionVerb.POST,
               Href = link,
               Rel =  RelationType.self,
               Type = ReponseTypeFormat.DefaultGet
           });

             content.Links.Add(new HyperMediaLink()
           {
               Action = HttpActionVerb.PUT,
               Href = link,
               Rel =  RelationType.self,
               Type = ReponseTypeFormat.DefaultGet
           });
              content.Links.Add(new HyperMediaLink()
           {
               Action = HttpActionVerb.PATCH,
               Href = link,
               Rel =  RelationType.self,
               Type = ReponseTypeFormat.DefaultPatch
           });
              content.Links.Add(new HyperMediaLink()
           {
               Action = HttpActionVerb.DELETE,
               Href = link,
               Rel =  RelationType.self,
               Type = "int"
           });

           return null;
        }

        private string getLink(long id, IUrlHelper urlHelper, string path)
        {
            lock(_lock)
            {
                var url = new {controller = path, id = id};
                return new StringBuilder (urlHelper.Link("DefaultApi", url)).Replace("%2f", "/").ToString();
            }
        }
    }
}