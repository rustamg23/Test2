using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace MyagkieLapki.Tests
{
    [TestFixture]
    public class TestState5
    {
        [Test]
        public void ApplicationProcess_FullApprovalFlow()
        {
            // Setup
            var shelter = Shelter.GetInstance();
            var client = new Client("John Doe", DateTime.Now.AddYears(-1));
            shelter.RegisterClient(client);
            var cage = shelter.AddCage(1, "Large");
            var passport = new AnimalPassport("Bella", DateTime.Now.AddYears(-2), "White", "Puffy", new List<string>());
            var animal = new Animal(passport, cage);
            animal.AcceptAnimal();
            animal.Status = Enums.AnimalStatus.ReadyForAdoption;

            // Act
            var application = client.SendApplication(shelter, animal, Enums.ApplicationType.Adoption, "APP001");
            client.PayFeeForAction(shelter, application, 100); // Assuming payment is done correctly
            application.UpdateApplicationStatus(Enums.ApplicationStatus.Approved); // Approving the application

            // Asserts
            Assert.AreEqual(Enums.ApplicationStatus.Approved, application.Status, "Application should be approved");
            Assert.IsTrue(animal.AdoptAnimal(), "Animal should be adopted successfully");

            // Clean up
            Shelter.ClearInstance();
        }

    }
}