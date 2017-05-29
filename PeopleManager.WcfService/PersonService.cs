using PeopleManager.Model;
using PeopleManager.Persistence;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Text;

namespace PeopleManager.WcfService
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _repository;

#if DEBUG

        public PersonService() : this(new PersonRepository())
        {
        }

#endif

        public PersonService(IPersonRepository repository)
        {
            if (repository == null)
                throw new ArgumentNullException(nameof(repository));

            _repository = repository;
        }

        public Person Add(Person person)
        {
            ThrowIfIsInvalid(person);

            _repository.Add(person);
            _repository.SaveChanges();

            return person;
        }

        public Person Get(Guid id)
        {
            return _repository.Get(id);
        }

        public IEnumerable<Person> GetAll()
        {
            return _repository.GetAll();
        }

        public void Remove(Guid id)
        {
            _repository.Remove(id);
            _repository.SaveChanges();
        }

        public void Update(Person person)
        {
            ThrowIfIsInvalid(person);

            _repository.Update(person);
            _repository.SaveChanges();
        }

        private void ThrowIfIsInvalid(Person person)
        {
            StringBuilder message = new StringBuilder();

            if (string.IsNullOrWhiteSpace(person.FirstName))
                message.AppendLine("First name is required.");

            if (string.IsNullOrWhiteSpace(person.LastName))
                message.AppendLine("Last name is required.");

            if (string.IsNullOrWhiteSpace(person.StreetName))
                message.AppendLine("Street Name is required.");

            if (string.IsNullOrWhiteSpace(person.HouseNumber))
                message.AppendLine("House number is required.");

            if (string.IsNullOrWhiteSpace(person.PostalCode))
                message.AppendLine("Postal code is required.");

            if (string.IsNullOrWhiteSpace(person.PhoneNumber))
                message.AppendLine("Phone number is required.");

            if (!person.DayOfBirth.HasValue)
                message.AppendLine("Date of birth is required.");

            if (message.Length != 0)
                throw new FaultException<PersonIsInvalidFault>(new PersonIsInvalidFault(message.ToString()));
        }
    }
}