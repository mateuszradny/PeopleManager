using Microsoft.VisualStudio.TestTools.UnitTesting;
using PeopleManager.Model;
using PeopleManager.Persistence;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PeopleManager.Tests.Persistence
{
    public class FakeEntity : EntityBase
    {
        public string Name { get; set; }
    }

    [TestClass]
    public class XmlRepositoryTests
    {
        [TestMethod]
        public void Add_ShouldAddsEntity_WhenIdIsUnique()
        {
            string fileName = Guid.NewGuid().ToString("N") + ".xml";

            Guid entityId = Guid.NewGuid();
            FakeEntity entity = new FakeEntity() { Id = entityId, Name = entityId.ToString() };
            XmlRepository<FakeEntity> repository = new XmlRepository<FakeEntity>(fileName);

            repository.Add(entity);
            repository.SaveChanges();

            repository = new XmlRepository<FakeEntity>(fileName);
            entity = repository.Get(entityId);

            Assert.AreEqual(entityId.ToString(), entity.Name);
            File.Delete(fileName);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Add_ShouldThrowsException_WhenIdIsNotUnique()
        {
            string fileName = Guid.NewGuid().ToString("N") + ".xml";

            Guid entityId = Guid.NewGuid();
            FakeEntity entity = new FakeEntity() { Id = entityId, Name = entityId.ToString() };
            XmlRepository<FakeEntity> repository = new XmlRepository<FakeEntity>(fileName);

            repository.Add(entity);
            repository.Add(entity);
        }

        [TestMethod]
        public void GetAll_ShouldReturnsEmptyCollection_IfNoData()
        {
            string fileName = Guid.NewGuid().ToString("N") + ".xml";
            XmlRepository<FakeEntity> repository = new XmlRepository<FakeEntity>(fileName);

            IEnumerable<FakeEntity> entities = repository.GetAll();

            Assert.AreEqual(0, entities.Count());
            File.Delete(fileName);
        }

        [TestMethod]
        public void GetAll_ShouldReturnsAllAddedEntities()
        {
            string fileName = Guid.NewGuid().ToString("N") + ".xml";

            int entityCount = 3;
            IEnumerable<Guid> entityIds = Enumerable.Range(0, entityCount).Select(x => Guid.NewGuid()).ToList();
            IEnumerable<FakeEntity> entities = entityIds.Select(x => new FakeEntity() { Id = x, Name = x.ToString() });
            XmlRepository<FakeEntity> repository = new XmlRepository<FakeEntity>(fileName);

            foreach (FakeEntity entity in entities)
                repository.Add(entity);

            repository.SaveChanges();

            repository = new XmlRepository<FakeEntity>(fileName);
            entities = repository.GetAll();

            Assert.AreEqual(entityCount, entities.Count());
            foreach (Guid entityId in entityIds)
                Assert.IsTrue(entities.SingleOrDefault(x => x.Id == entityId && x.Name == entityId.ToString()) != null);

            File.Delete(fileName);
        }

        [TestMethod]
        public void Remove_ShouldRemovesEntity_IfIdExists()
        {
            string fileName = Guid.NewGuid().ToString("N") + ".xml";

            Guid entityId = Guid.NewGuid();
            FakeEntity entity = new FakeEntity() { Id = entityId, Name = entityId.ToString() };
            XmlRepository<FakeEntity> repository = new XmlRepository<FakeEntity>(fileName);

            repository.Add(entity);
            repository.SaveChanges();
            repository.Remove(entityId);
            repository.SaveChanges();

            repository = new XmlRepository<FakeEntity>(fileName);

            Assert.AreEqual(0, repository.GetAll().Count());
            File.Delete(fileName);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Remove_ShouldThrowsException_IfIdNotExists()
        {
            string fileName = Guid.NewGuid().ToString("N") + ".xml";

            Guid entityId = Guid.NewGuid();
            XmlRepository<FakeEntity> repository = new XmlRepository<FakeEntity>(fileName);

            repository.Remove(entityId);
        }

        [TestMethod]
        public void Update_ShouldSavesChanges_IfEntityExists()
        {
            string fileName = Guid.NewGuid().ToString("N") + ".xml";

            Guid entityId = Guid.NewGuid();
            FakeEntity entity = new FakeEntity() { Id = entityId, Name = entityId.ToString() };
            XmlRepository<FakeEntity> repository = new XmlRepository<FakeEntity>(fileName);

            repository.Add(entity);
            repository.SaveChanges();

            entity = new FakeEntity() { Id = entityId, Name = "NewValue" };

            repository = new XmlRepository<FakeEntity>(fileName);
            repository.Update(entity);
            repository.SaveChanges();

            repository = new XmlRepository<FakeEntity>(fileName);
            entity = repository.Get(entityId);

            Assert.AreEqual("NewValue", entity.Name);
            File.Delete(fileName);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Update_ShouldThrowsException_IfEntityNotExists()
        {
            string fileName = Guid.NewGuid().ToString("N") + ".xml";

            Guid entityId = Guid.NewGuid();
            FakeEntity entity = new FakeEntity() { Id = entityId, Name = entityId.ToString() };
            XmlRepository<FakeEntity> repository = new XmlRepository<FakeEntity>(fileName);

            repository.Update(entity);
        }
    }
}