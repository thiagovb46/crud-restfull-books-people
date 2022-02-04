using System.Collections.Generic;
using System.Linq;
using RestWith.NET.Data.Converter.Contract;
using RestWith.NET.Data.VO;
using RestWith.NET.Model;

namespace RestWith.NET.Data.Converter.Implementations
{
    public class PersonConverter : IParser<PersonVo, Person>, IParser <Person, PersonVo>
    {
        public Person Parse(PersonVo origin)
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

        public List<Person> Parse(List<PersonVo> origin)
        {
            if(origin == null)
                return null;
            return origin.Select(item=> Parse(item)).ToList();
        }

        public PersonVo Parse(Person origin)
        {
            if( origin == null)
                return null;
            return new PersonVo
            {
                Id = origin.Id,
                FirstName= origin.FirstName,
                LastName = origin.LastName,
                Adress = origin.Adress,
                Gender = origin.Gender,
                Enable = origin.Enable
            };
        }
        public List<PersonVo> Parse(List<Person> origin)
        {
             if(origin == null)
                return null;
            return origin.Select(item=> Parse(item)).ToList();
        }
    }
}