using PeopleManager.Model;
using PeopleManager.WcfService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;

namespace PeopleManager.WinClient.ViewModels
{
    public class PeopleViewModel : ViewModelBase
    {
        private readonly IPersonService _personService;
        private List<Guid> _existingIds;
        private bool _isDirty;

        public PeopleViewModel(IPersonService personService)
        {
            _personService = personService ?? throw new ArgumentNullException(nameof(personService));

            People.CollectionChanged += People_CollectionChanged;
            InitData();
        }

        public bool IsDirty
        {
            get => _isDirty;
            set
            {
                _isDirty = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<PersonViewModel> People { get; } = new ObservableCollection<PersonViewModel>();

        #region Commands

        public ICommand Cancel
        {
            get { return new RelayCommand(OnCancel, x => IsDirty); }
        }

        public ICommand Save
        {
            get { return new RelayCommand(OnSave, x => IsDirty); }
        }

        #endregion Commands

        private void InitData()
        {
            IEnumerable<Person> people = _personService.GetAll();
            _existingIds = people.Select(x => x.Id).ToList();

            People.Clear();
            foreach (Person person in people)
                People.Add(new PersonViewModel(person));

            IsDirty = false;
        }

        private void OnCancel(object obj)
        {
            this.InitData();
        }

        private void OnSave(object obj)
        {
            AddRecords();
            UpdateRecords();
            RemoveRecords();
        }

        private void People_CollectionChanged(object sender, NotifyCollectionChangedEventArgs eventArgs)
        {
            if (eventArgs.Action == NotifyCollectionChangedAction.Add)
                foreach (PersonViewModel person in eventArgs.NewItems)
                    person.PropertyChanged += (s, e) => IsDirty = true;

            IsDirty = true;
        }

        private void AddRecords()
        {
            IEnumerable<PersonViewModel> rows = People.Where(x => x.State == PersonState.Added);
            foreach (PersonViewModel row in rows)
            {
                Person record = row.Person;

                record.Id = _personService.Add(record).Id;
                _existingIds.Add(record.Id);

                row.State = PersonState.Unchanged;
            }
        }

        private void UpdateRecords()
        {
            IEnumerable<PersonViewModel> rows = People.Where(x => x.State == PersonState.Modified);
            foreach (PersonViewModel row in rows)
            {
                _personService.Update(row.Person);
                row.State = PersonState.Unchanged;
            }
        }

        private void RemoveRecords()
        {
            foreach (Guid id in _existingIds.Where(x => !People.Any(p => p.Person.Id == x)).ToList())
            {
                _personService.Remove(id);
                _existingIds.Remove(id);
            }
        }
    }
}