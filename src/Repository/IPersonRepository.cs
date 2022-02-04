using System.Collections.Generic;
using RestWith.NET.Model;
using RestWith.NET.Repository;

namespace src.Repository
{
    public interface IPersonRepository : IRepository<Person>
    {
        Person Disable(long id);
        Person UpdatePerson(long id, Person person);
        List<Person> FindByName (string firstName, string secondName);
    }
}