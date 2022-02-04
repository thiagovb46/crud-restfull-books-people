using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using RestWith.NET.Context;
using RestWith.NET.Data.VO;
using RestWith.NET.Model;

namespace RestWith.NET.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;
            
            
        }
        public User Create (User user)
        {
            var pass = ComputeHash(user.Password);
            user.Password = pass.ToString();
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;

        }

        public User ValidateCredentials(UserVo user)
        {
            var pass = ComputeHash(user.Password);
            return _context.Users.
            FirstOrDefault(
                u=>u.UserName==user.UserName
                && u.Password==pass.ToString());
        }
        public User RefreshUserInfo(User user) 
        {
            if(!_context.Users.Any(u=> u.Id==user.Id))
                return null;
            var result  = _context.Users.SingleOrDefault(p => p.Id.Equals(user.Id));
            try 
            {
                _context.Entry(result).CurrentValues.SetValues(user);
                _context.SaveChanges();
            }
            catch(Exception e)
            {
                throw;  
            }
            return result;
        }
        private object ComputeHash(string password)
        {
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(password);
            byte[] hash = md5.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }

        public User ValidateCredentials(string username)
        {
            return _context.Users.SingleOrDefault(u=> (u.UserName == username));
        }

        public bool RevokeToken(string username)
        {
            var user = _context.Users.SingleOrDefault(u=> u.UserName==username);
            if(user is null)
                return false;
            user.RefreshToken = null;
            _context.SaveChanges();
            return true;
        }


    }
}