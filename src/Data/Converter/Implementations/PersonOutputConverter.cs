using System.Collections.Generic;
using System.Linq;
using RestWith.NET.Data.Converter.Contract;
using RestWith.NET.Data.VO;
using RestWith.NET.Model;

namespace RestWith.NET.Data.Converter.Implementations
{
    public class PersonOutputConverter : IParser<PersonVoOutput, Person>, IParser <Person, PersonVoOutput>
    {
        public Person Parse(PersonVoOutput origin)
        {
            if(origin==null)
                return null;
            return new Person 
            {
                Id = origin.Id,
                FirstName = origin.FirstName,
                LastName = origin.LastName,
                Adress = origin.Adress,
                Gender = origin.Gender,
                Enable = origin.Enable
            };
        }

        public List<Person> Parse(List<PersonVoOutput> origin)
        {
            if(origin == null)
                return null;
            return origin.Select(item=> Parse(item)).ToList();
        }

        public PersonVoOutput Parse(Person origin)
        {
            if( origin == null)
                return null;
            return new PersonVoOutput
            {
                Id = origin.Id,
                FirstName= origin.FirstName,
                LastName = origin.LastName,
                Adress = origin.Adress,
                Gender = origin.Gender,
                Enable = origin.Enable
            };
        }
        public List<PersonVoOutput> Parse(List<Person> origin)
        {
             if(origin == null)
                return null;
            return origin.Select(item=> Parse(item)).ToList();
        }
    }
}