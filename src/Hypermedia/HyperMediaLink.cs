using System.Text;

namespace RestWith.NET.Hypermedia
{
    public class HyperMediaLink
    {
        public string Rel{get;set;}
        private string href{get;set;}
        public string Href 
        {
            get
            {
                object _lock = new object();
                lock(_lock)
                {
                        StringBuilder sb = new StringBuilder(href);
                        return sb.Replace("%2f", "/").ToString();
                }

            }
            set
            {
                href = value;
            }
        } // = %2f o dotnet converte // para %2f
        public string Type{get;set;} 
        public string Action{get;set;}
    }
}