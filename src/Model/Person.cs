using RestWith.NET.Model.Base;

namespace RestWith.NET.Model
{
    public class Person: BaseEntity
    {  
         public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Adress { get; set; }
        public string Gender { get; set; }
        public bool Enable { get; set; }
    }
}
