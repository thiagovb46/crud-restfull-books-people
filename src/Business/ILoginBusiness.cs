using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestWith.NET.Data.VO;

namespace RestWith.NET.Business
{
    public interface ILoginBusiness
    {
        public TokenVo ValidateCredentials(UserVo user);
        public TokenVo ValidateCredentials(TokenVo token);
        bool RevokeToken (string userName);
    }
}