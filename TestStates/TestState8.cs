using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace MyagkieLapki.Tests
{
    [TestFixture]
    public class TestState8
    {
        [Test]
        public void ApplicationProcess_SuspensionAndResumption()
        {
            // Setup
            var shelter = Shelter.GetInstance();
            var client = new Client("Michael Johnson", DateTime.Now.AddYears(-1));
            shelter.RegisterClient(client);
            var cage = shelter.AddCage(2, "Large");
            var passport = new AnimalPassport("Daisy", DateTime.Now.AddYears(-2), "Spotted", "Dalmatian",
                new List<string>());
            var animal = new Animal(passport, cage);
            animal.AcceptAnimal();
            animal.Status = Enums.AnimalStatus.ReadyForAdoption;

            // Act
            var application = client.SendApplication(shelter, animal, Enums.ApplicationType.Adoption, "APP004");
            client.PayFeeForAction(shelter, application, 100); // Pay registration fee
            application.UpdateApplicationStatus(Enums.ApplicationStatus
                .AwaitRegistrationPayment); // Simulate suspension
            application.UpdateApplicationStatus(Enums.ApplicationStatus.Registration); // Resume application
            application.UpdateApplicationStatus(Enums.ApplicationStatus.Approved); // Approve application
            var adopted = animal.AdoptAnimal(); // Complete adoption

            // Asserts
            Assert.IsTrue(adopted, "Adoption should be successfully completed after application resumed and approved");
            Assert.AreEqual(Enums.ApplicationStatus.Approved, application.Status, "Application should be approved");

            // Clean up
            Shelter.ClearInstance();
        }
    }
}