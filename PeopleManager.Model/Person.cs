using System;
using System.Runtime.Serialization;

namespace PeopleManager.Model
{
    [DataContract]
    public class Person : EntityBase
    {
        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string StreetName { get; set; }

        [DataMember]
        public string HouseNumber { get; set; }

        [DataMember]
        public int? ApartmentNumber { get; set; }

        [DataMember]
        public string PostalCode { get; set; }

        [DataMember]
        public string PhoneNumber { get; set; }

        [DataMember]
        public DateTime? DayOfBirth { get; set; }
    }
}