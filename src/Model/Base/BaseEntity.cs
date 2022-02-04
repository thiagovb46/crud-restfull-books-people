using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RestWith.NET.Model.Base
{
    public class BaseEntity
    {
        [Key]
        public long Id{get;set;}
    }
}