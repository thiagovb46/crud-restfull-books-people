using RestWith.NET.Data.VO;
using RestWith.NET.Model;

namespace RestWith.NET.Repository
{
    public interface IUserRepository
    {
        User ValidateCredentials (UserVo user);
        User ValidateCredentials(string username);
        User RefreshUserInfo (User user);
        bool RevokeToken(string username);
        User Create(User user);

    }
}