using System.Collections.Generic;
using System.Linq;
using RestWith.NET.Data.Converter.Contract;
using RestWith.NET.Data.VO;
using RestWith.NET.Model;

namespace RestWith.NET.Data.Converter.Implementations
{
    public class BookConverter : IParser <BookVo, Book>, IParser <Book, BookVo>
    {
        public BookVo Parse(Book origin)
        {
                if(origin==null)
                    return null;
                return new BookVo 
                {
                    Id = origin.Id,
                    Title = origin.Title,
                    Author = origin.Author,
                    Price = origin.Price,
                    LaunchDate = origin.LaunchDate
                };
        }
        public List<BookVo> Parse(List<Book> origin)
        {
            if(origin == null)
                return null;
            return origin.Select(item=> Parse(item)).ToList();
        }

        public Book Parse(BookVo origin)
        {
            if(origin==null)
                return null;
            return new Book 
            {
                
                Title = origin.Title,
                Author = origin.Author,
                Price = origin.Price,
                LaunchDate = origin.LaunchDate
            };
        }
        public List<Book> Parse(List<BookVo> origin)
        {
             if(origin == null)
                return null;
            return origin.Select(item=> Parse(item)).ToList();
        }
    }
}