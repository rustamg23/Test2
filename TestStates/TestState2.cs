using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace MyagkieLapki.Tests
{
    [TestFixture]
    public class TestState2
    {

        [Test]
        public void AnimalLifecycle_NewToReturned()
        {
            // Setup
            var shelter = Shelter.GetInstance();
            var cage = shelter.AddCage(1, "Medium");
            var passport = new AnimalPassport("Milo", DateTime.Now.AddYears(-3), "Black", "Short-haired",
                new List<string>());
            var animal = new Animal(passport, cage);

            // Act
            animal.AcceptAnimal(); // Animal accepted into the shelter
            animal.Status = Enums.AnimalStatus.Healing; // Assume the animal is now healing
            animal.Status = Enums.AnimalStatus.ReadyForAdoption; // Animal is now ready for adoption
            animal.MarkAsDeceased(DateTime.Now); // Assuming returning to owner as deceased for simplicity

            // Asserts
            Assert.AreEqual(Enums.AnimalStatus.Dead, animal.Status, "Animal status should be 'Dead'");
            Assert.IsFalse(cage.IsBusy, "Cage should be not busy after returning to owner");

            // Clean up
            Shelter.ClearInstance();
        }
    }
}