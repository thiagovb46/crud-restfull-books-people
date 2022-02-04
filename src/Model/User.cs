using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RestWith.NET.Model
{
    public class User
    {
        [Key]
        public long Id{ get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string RefreshToken { get; set; }
        public DateTime Refresh_Token_Expiry_Time { get; set;}
    }
}