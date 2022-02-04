using System.Collections.Generic;
using RestWith.NET.Data.Converter.Implementations;
using RestWith.NET.Data.VO;
using RestWith.NET.Model;
using RestWith.NET.Repository;

namespace RestWith.NET.Business.Implementations
{
    public class BookBusinessImplementation : IBookBusiness
    {   
        private readonly IRepository<Book> _bookRepository;
        private readonly BookConverter _bookConverter;
        private readonly BookOutputConverter _bookOutputConverter;

        public BookBusinessImplementation(IRepository<Book> bookRepository )
        {
            _bookRepository = bookRepository;
            _bookConverter = new BookConverter();
            _bookOutputConverter = new BookOutputConverter();
        }
        public BookVoOutput Create(BookVo book)
        {
            var created = _bookRepository.Create(_bookConverter.Parse(book));
            return _bookOutputConverter.Parse(created);
        }
        public BookVoOutput FindById(long id)
        {
            return _bookOutputConverter.Parse(_bookRepository.FindById(id));
        }
         public List<BookVoOutput> FindAll()
        {
           return _bookOutputConverter.Parse( _bookRepository.FindAll());
        }
        public BookVoOutput Update(BookVo book, long id)
        {
          return _bookOutputConverter.
          Parse( _bookRepository.
          Update(_bookConverter.Parse(book), id));
        }
         public void Delete(long id)
        {
            _bookRepository.Delete(id);
        }
    }
}