using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace MyagkieLapki.Tests
{
    [TestFixture]
    public class TestState10
    {
        [Test]
        public void ApplicationProcess_StraightforwardAdoption()
        {
            // Setup
            var shelter = Shelter.GetInstance();
            var client = new Client("Tom Hanks", DateTime.Now.AddYears(-2)); // Client well-established
            shelter.RegisterClient(client);
            var cage = shelter.AddCage(4, "Small");
            var passport = new AnimalPassport("Maggie", DateTime.Now.AddYears(-3), "Grey", "Long-haired",
                new List<string>());
            var animal = new Animal(passport, cage);
            animal.AcceptAnimal();
            animal.Status = Enums.AnimalStatus.ReadyForAdoption;

            // Act
            var application = client.SendApplication(shelter, animal, Enums.ApplicationType.Adoption, "APP006");
            client.PayFeeForAction(shelter, application, 100); // Pay registration fee
            application.UpdateApplicationStatus(Enums.ApplicationStatus.Approved); // Approve application
            var adopted = animal.AdoptAnimal(); // Animal is adopted

            // Asserts
            Assert.IsTrue(adopted, "Animal should be adopted successfully");
            Assert.AreEqual(Enums.ApplicationStatus.Approved, application.Status,
                "Application status should be 'Approved'");

            // Clean up
            Shelter.ClearInstance();
        }
    }
}