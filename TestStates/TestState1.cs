using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace MyagkieLapki.Tests
{
    [TestFixture]
    public class TestState1
    {
        [Test]
        public void AnimalLifecycle_NewToAdopted()
        {
            // Setup
            var shelter = Shelter.GetInstance();
            var cage = shelter.AddCage(1, "Medium");
            var passport =
                new AnimalPassport("Buddy", DateTime.Now.AddYears(-2), "Brown", "Fluffy", new List<string>());
            var animal = new Animal(passport, cage);

            // Act
            animal.Status = Enums.AnimalStatus.Healing; // Assume the animal is now healing
            animal.Status = Enums.AnimalStatus.ReadyForAdoption; // Animal is now ready for adoption
            var adopted = animal.AdoptAnimal(); // Attempt to adopt the animal

            // Asserts
            Assert.IsTrue(adopted, "Animal should be adopted");
            Assert.AreEqual(Enums.AnimalStatus.Adopted, animal.Status, "Animal status should be 'Adopted'");
            Assert.IsFalse(cage.IsBusy, "Cage should be not busy after adoption");
            Assert.IsTrue(cage.IsReadyForNewAnimal(), "Cage should be ready for a new animal");

            // Clean up
            Shelter.ClearInstance();
        }
    }
}