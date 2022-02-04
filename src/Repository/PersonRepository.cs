using System;
using System.Collections.Generic;
using System.Linq;
using RestWith.NET.Context;
using RestWith.NET.Model;
using RestWith.NET.Repository.Generic;

namespace src.Repository
{
    public class PersonRepository: GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository(DataContext context): base(context) {}
        

        public Person UpdatePerson(long id, Person person)
        {
            var toBeUpdated = _context.People.SingleOrDefault(p=>p.Id==id);
            if(toBeUpdated==null)
                return null;
            toBeUpdated.Adress =person.Adress;
            toBeUpdated.Enable= person.Enable;
            toBeUpdated.FirstName = person.FirstName;
            toBeUpdated.Gender = person.Gender;
            toBeUpdated.LastName = person.LastName;
            _context.SaveChanges();
            return toBeUpdated;

        }

        public Person Disable(long id)
        {
            if(! _context.People.Any(p=>p.Id.Equals(id)))
                return null;
            var user = _context.People.SingleOrDefault(p=>p.Id.Equals(id));
            if(user != null) 
            {
                user.Enable = false;
                try 
                {
                    _context.Entry(user).CurrentValues.SetValues(user);
                    _context.SaveChanges();
                }
                catch (Exception e)
                {
                    throw;
                }
            }
            return user ;
        }

        public List<Person> FindByName(string firstName, string lastName)
        {
            if(! string.IsNullOrWhiteSpace(firstName) && !string.IsNullOrWhiteSpace(lastName))
            {
                return _context.People.
                 Where(p=> p.FirstName.Contains(firstName) 
                 && p.LastName.Contains(lastName)).ToList();

            }
            if (string.IsNullOrWhiteSpace(firstName) && !string.IsNullOrWhiteSpace(lastName))
            {
                return _context.People.
                Where(p=>p.LastName.Contains(lastName)).ToList();
            }
            if (!string.IsNullOrWhiteSpace(firstName) && string.IsNullOrWhiteSpace(lastName))
            {
                  return _context.People.Where(p=> p.FirstName.Contains(firstName)).ToList();
            }
            return null;
            }
    }
}