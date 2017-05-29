using PeopleManager.Model;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace PeopleManager.WcfService.Proxy
{
    public class PersonServiceProxy : ClientBase<IPersonService>, IPersonService
    {
        public Person Add(Person person)
        {
            return base.Channel.Add(person);
        }

        public Person Get(Guid id)
        {
            return base.Channel.Get(id);
        }

        public IEnumerable<Person> GetAll()
        {
            return base.Channel.GetAll();
        }

        public void Remove(Guid id)
        {
            base.Channel.Remove(id);
        }

        public void Update(Person person)
        {
            base.Channel.Update(person);
        }
    }
}