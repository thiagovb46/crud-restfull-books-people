using System.Collections.Generic;
using RestWith.NET.Data.VO;
using RestWith.NET.Data.Converter.Implementations;
using src.Repository;
using src.Hypermedia;
using RestWith.NET.Model;
using System;

namespace RestWith.NET.Business.Implementations
{
    public class PersonBusinessImplementation : IPersonBusiness
    { 
        private readonly IPersonRepository  _personRepository;
        private readonly PersonConverter _converter;
        private readonly PersonOutputConverter _outputConverter;
        public PersonBusinessImplementation(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
            _converter = new PersonConverter();
            _outputConverter = new PersonOutputConverter();
        }
        public PersonVoOutput Create(PersonVo person)
        {
                var personEntity = _personRepository.Create(_converter.Parse(person));
                return _outputConverter.Parse(personEntity);
            
        }
         public PersonVoOutput FindById(long id)
        {
            var finded = _personRepository.FindById(id);
            return _outputConverter.Parse(finded);
        }
        public List<PersonVoOutput> FindAll()
        {
            return _outputConverter.Parse(_personRepository.FindAll());
        }

        public PagedSearchVo<PersonVoOutput> FindWithPagedSearch(string name, string sortDirection,int pageSize, int page)
        {
            
            var sort = (!string.IsNullOrWhiteSpace(sortDirection)&& !sortDirection.Equals("desc")) ? "asc" : "desc";
            var size = pageSize;
            var offset = page;

            string query ="select * from People p where 1=1 ";
            if(!string.IsNullOrWhiteSpace(name))
            {
                query+= $"and p.FirstName like '%{name}%' ";
            } 

            string countQuery = "select count(*) from People p where 1=1 ";
            if(!string.IsNullOrWhiteSpace(name))
            {
                countQuery+= $"and p.FirstName like '%{name}%' ";
            }
            
            query = query + $"order by p.FirstName OFFSET {offset} rows fetch next {size} rows only";
            
            var persons = _personRepository.FindWithPagedSearch(query);
            int TotalResults = _personRepository.getCount(countQuery);
            return new PagedSearchVo<PersonVoOutput>
            {
                CurrentPage = offset,
                List = _outputConverter.Parse(persons),
                PageSize = size,
                SortDirections = sort,
                TotalResults = TotalResults
            };
        }
        public PersonVoOutput Update(PersonVo person, long id)
        {
            return _outputConverter.
            Parse( _personRepository.
            UpdatePerson(id, _converter.Parse(person)));
        }
        public void Delete(long id)
        {
            _personRepository.Delete(id);
        }

        public PersonVo Disable(long id)
        {
            var personEntity = _personRepository.Disable(id);
            return _converter.Parse(personEntity);
        }

        public List<PersonVoOutput> FindByName(string firstName, string lastName)
        {
            return _outputConverter.
            Parse(_personRepository.
            FindByName(firstName,lastName));
        }
    }
}
