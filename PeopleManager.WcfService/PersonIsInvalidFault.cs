using System.Runtime.Serialization;

namespace PeopleManager.WcfService
{
    [DataContract]
    public class PersonIsInvalidFault
    {
        private string _message;

        public PersonIsInvalidFault(string message)
        {
            _message = message;
        }

        [DataMember]
        public string Message
        {
            get => _message;
            set => _message = value;
        }
    }
}