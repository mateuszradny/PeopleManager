using PeopleManager.Model;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace PeopleManager.WcfService
{
    [ServiceContract]
    public interface IPersonService
    {
        [OperationContract]
        [FaultContract(typeof(PersonIsInvalidFault))]
        Person Add(Person person);

        [OperationContract]
        Person Get(Guid id);

        [OperationContract]
        IEnumerable<Person> GetAll();

        [OperationContract]
        void Remove(Guid id);

        [OperationContract]
        [FaultContract(typeof(PersonIsInvalidFault))]
        void Update(Person person);
    }
}