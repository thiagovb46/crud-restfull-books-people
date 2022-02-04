using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestWith.NET.Data.VO;
using RestWith.NET.Model;

namespace RestWith.NET.Business
{
    public interface IUsersBusiness
    {
        public SingupUserVo Create(SingupUserVo user);
    }
}