using System.Collections.Generic;
using RestWith.NET.Data.VO;
namespace RestWith.NET.Business
{
    public interface IBookBusiness
    {
        BookVoOutput Create(BookVo book);
        BookVoOutput FindById(long id);
        BookVoOutput Update(BookVo book, long id);
        void Delete(long id);
        List<BookVoOutput> FindAll();
    }
}