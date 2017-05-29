using PeopleManager.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;

namespace PeopleManager.Persistence
{
    public class XmlRepository<T> : IRepository<T>
        where T : EntityBase
    {
        private readonly string _filePath;
        private readonly Lazy<List<T>> _entities;

        public XmlRepository(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("Invalid file path.");

            _filePath = filePath;
            _entities = new Lazy<List<T>>(() => InitData());
        }

        public T Add(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            if (entity.Id == Guid.Empty)
                entity.Id = Guid.NewGuid();

            if (_entities.Value.Count(x => x.Id == entity.Id) > 0)
                throw new InvalidOperationException($"The entity with id {entity.Id} already exists.");

            _entities.Value.Add(entity);
            return entity;
        }

        public T Get(Guid id)
        {
            return _entities.Value.Single(x => x.Id == id);
        }

        public IEnumerable<T> GetAll()
        {
            return _entities.Value;
        }

        public void Remove(Guid id)
        {
            T entity = Get(id);
            _entities.Value.Remove(entity);
        }

        public void Update(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            T original = Get(entity.Id);
            foreach (PropertyInfo property in typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                object newValue = property.GetValue(entity);
                property.SetValue(original, newValue);
            }
        }

        public void SaveChanges()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
            using (XmlWriter writer = XmlWriter.Create(_filePath))
                serializer.Serialize(writer, _entities.Value);
        }

        private List<T> InitData()
        {
            if (!File.Exists(_filePath))
                return new List<T>();

            XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
            using (XmlReader reader = XmlReader.Create(_filePath))
                return serializer.Deserialize(reader) as List<T>;
        }
    }
}