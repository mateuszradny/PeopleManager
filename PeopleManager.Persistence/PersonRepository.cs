using PeopleManager.Model;
using System;
using System.IO;

namespace PeopleManager.Persistence
{
    public class PersonRepository : XmlRepository<Person>, IPersonRepository
    {
        public PersonRepository()
            : base(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "people.xml"))
        {
        }
    }
}