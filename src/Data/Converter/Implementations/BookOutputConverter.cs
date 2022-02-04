using System.Collections.Generic;
using System.Linq;
using RestWith.NET.Data.Converter.Contract;
using RestWith.NET.Data.VO;
using RestWith.NET.Model;

namespace RestWith.NET.Data.Converter.Implementations
{
    public class BookOutputConverter : IParser <BookVoOutput, Book>, IParser <Book, BookVoOutput>
    {
        public BookVoOutput Parse(Book origin)
        {
                if(origin==null)
                    return null;
                return new BookVoOutput 
                {
                    Id = origin.Id,
                    Title = origin.Title,
                    Author = origin.Author,
                    Price = origin.Price,
                    LaunchDate = origin.LaunchDate
                };
        }
        public List<BookVoOutput> Parse(List<Book> origin)
        {
            if(origin == null)
                return null;
            return origin.Select(item=> Parse(item)).ToList();
        }

        public Book Parse(BookVoOutput origin)
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
        public List<Book> Parse(List<BookVoOutput> origin)
        {
             if(origin == null)
                return null;
            return origin.Select(item=> Parse(item)).ToList();
        }
    }
}