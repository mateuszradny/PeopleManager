using PeopleManager.Model;
using System;
using System.Runtime.CompilerServices;

namespace PeopleManager.WinClient.ViewModels
{
    public enum PersonState
    {
        Added,
        Deleted,
        Modified,
        Unchanged
    }

    public class PersonViewModel : ViewModelBase
    {
        private readonly Person _person;
        private int? _age;

        public PersonViewModel(Person person)
        {
            State = person == null ? PersonState.Added : PersonState.Unchanged;
            _person = person ?? new Person();
            _age = _person.DayOfBirth.HasValue ? (int?)CalculateAge(_person.DayOfBirth.Value) : null;
        }

        public PersonViewModel() : this(null)
        { }

        #region Properties

        public int? Age
        {
            get => _age;
            private set
            {
                _age = value;
                OnPropertyChanged();
            }
        }

        public int? ApartmentNumber
        {
            get => _person.ApartmentNumber;
            set
            {
                _person.ApartmentNumber = value;
                OnPropertyChanged();
            }
        }

        public DateTime? DayOfBirth
        {
            get => _person.DayOfBirth;
            set
            {
                _person.DayOfBirth = value;
                OnPropertyChanged();

                Age = _person.DayOfBirth.HasValue ? (int?)CalculateAge(_person.DayOfBirth.Value) : null;
            }
        }

        public string FirstName
        {
            get => _person.FirstName;
            set
            {
                _person.FirstName = value;
                OnPropertyChanged();
            }
        }

        public string HouseNumber
        {
            get => _person.HouseNumber;
            set
            {
                _person.HouseNumber = value;
                OnPropertyChanged();
            }
        }

        public string LastName
        {
            get => _person.LastName;
            set
            {
                _person.LastName = value;
                OnPropertyChanged();
            }
        }

        public Person Person => _person;

        public string PhoneNumber
        {
            get => _person.PhoneNumber;
            set
            {
                _person.PhoneNumber = value;
                OnPropertyChanged();
            }
        }

        public string PostalCode
        {
            get => _person.PostalCode;
            set
            {
                _person.PostalCode = value;
                OnPropertyChanged();
            }
        }

        public PersonState State { get; set; }

        public string StreetName
        {
            get => _person.StreetName;
            set
            {
                _person.StreetName = value;
                OnPropertyChanged();
            }
        }

        #endregion Properties

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            base.OnPropertyChanged(propertyName);

            if (State == PersonState.Unchanged)
                State = PersonState.Modified;
        }

        private static int CalculateAge(DateTime dayOfBirth)
        {
            DateTime today = DateTime.Today;
            int age = today.Year - dayOfBirth.Year;

            if (dayOfBirth > today.AddYears(-age))
                age--;

            return age;
        }
    }
}