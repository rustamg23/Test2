using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace MyagkieLapki.Tests
{
    [TestFixture]
    public class TestState3
    {
        [Test]
        public void AnimalLifecycle_NewToDeceased()
        {
            // Setup
            var shelter = Shelter.GetInstance();
            var cage = shelter.AddCage(1, "Large");
            var passport = new AnimalPassport("Oliver", DateTime.Now.AddYears(-4), "Grey", "Tabby", new List<string>());
            var animal = new Animal(passport, cage);

            // Act
            animal.AcceptAnimal();
            animal.Status = Enums.AnimalStatus.Healing;
            animal.Status = Enums.AnimalStatus.ReadyForAdoption;
            animal.MarkAsDeceased(DateTime.Now); // Animal passes away

            // Asserts
            Assert.AreEqual(Enums.AnimalStatus.Dead, animal.Status, "Animal status should be 'Dead'");
            Assert.IsFalse(cage.IsBusy, "Cage should not be busy after animal is deceased");

            // Clean up
            Shelter.ClearInstance();
        }
    }
}