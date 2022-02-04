using RestWith.NET.Data.VO;
using src.Hypermedia;
using System.Collections.Generic;


namespace RestWith.NET.Business
{
    public interface IPersonBusiness
    {
        PersonVoOutput Create(PersonVo person);
        PersonVoOutput FindById(long id);
        List<PersonVoOutput> FindByName(string firstName, string lastName);
        public PersonVoOutput Update(PersonVo person, long id);
        PersonVo Disable (long id);
        void Delete(long id);
        List<PersonVoOutput> FindAll();
        PagedSearchVo <PersonVoOutput> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page);
    }
}
