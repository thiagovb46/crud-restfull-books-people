using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestWith.NET.Data.VO;
using RestWith.NET.Model;
using RestWith.NET.Repository;

namespace RestWith.NET.Business.Implementations
{
    public class UsersBusinessImplementation : IUsersBusiness
    {
        private IUserRepository _repository;
        public UsersBusinessImplementation (IUserRepository repository)
        {
                _repository = repository;
                
        }

        public SingupUserVo Create(SingupUserVo user)
        {
            _repository.Create(new User {
                
                UserName = user.UserName,
                FullName = user.FullName,
                Password= user.Password
            });
            return user;
        }
    }
}