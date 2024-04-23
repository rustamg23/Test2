using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace MyagkieLapki.Tests
{
    [TestFixture]
    public class TestState4
    {
        [Test]
        public void AnimalLifecycle_NewToAdoptionDirect()
        {
            // Setup
            var shelter = Shelter.GetInstance();
            var cage = shelter.AddCage(1, "Small");
            var passport = new AnimalPassport("Lucy", DateTime.Now.AddYears(-1), "Black", "Smooth", new List<string>());
            var animal = new Animal(passport, cage);

            // Act
            animal.AcceptAnimal();
            animal.Status = Enums.AnimalStatus.ReadyForAdoption;
            var adopted = animal.AdoptAnimal(); // Attempt to adopt the animal directly

            // Asserts
            Assert.IsTrue(adopted, "Animal should be adopted directly without healing");
            Assert.AreEqual(Enums.AnimalStatus.Adopted, animal.Status, "Animal status should be 'Adopted'");
            Assert.IsFalse(cage.IsBusy, "Cage should be not busy after adoption");

            // Clean up
            Shelter.ClearInstance();
        }
    }
}