using System;
using System.Runtime.Serialization;

namespace PeopleManager.Model
{
    [DataContract]
    public abstract class EntityBase
    {
        [DataMember]
        public Guid Id { get; set; }
    }
}