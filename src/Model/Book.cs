using System;
using System.ComponentModel.DataAnnotations.Schema;
using RestWith.NET.Model.Base;

namespace RestWith.NET.Model
{
    public class Book: BaseEntity
    {
        
        public string Title { get; set; }
        public string Author { get; set; }

        public decimal Price { get; set; }

        public DateTime LaunchDate { get; set; }
        }
}