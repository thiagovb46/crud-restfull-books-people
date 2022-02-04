using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestWith.NET.Business;
using RestWith.NET.Data.VO;

namespace RestWith.NET.Controllers
{
    [Route("[controller]")]
    public class UserSingupController : Controller
    {
        private readonly ILogger<UserSingupController> _logger;
        private readonly IUsersBusiness _business;

        public UserSingupController(ILogger<UserSingupController> logger, IUsersBusiness business) 
        {
            _logger = logger;
            _business = business;
        }

        [HttpPost]
        public IActionResult Create([FromBody] SingupUserVo user)
        {
           return  Created("Create sucess", _business.Create(user));
        }    
        }
}