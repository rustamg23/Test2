using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace MyagkieLapki.Tests
{
    [TestFixture]
    public class TestState7
    {
        [Test]
        public void ApplicationProcess_Cancellation()
        {
            // Setup
            var shelter = Shelter.GetInstance();
            var client = new Client("Ella Fitzgerald", DateTime.Now.AddYears(-1));
            shelter.RegisterClient(client);
            var cage = shelter.AddCage(1, "Medium");
            var passport = new AnimalPassport("Max", DateTime.Now.AddYears(-3), "Brindle", "Mixed", new List<string>());
            var animal = new Animal(passport, cage);
            animal.AcceptAnimal();

            // Act
            var application = client.SendApplication(shelter, animal, Enums.ApplicationType.Adoption, "APP003");
            client.PayFeeForAction(shelter, application, 100);
            application.CancelApplication(); // Cancel the application

            // Asserts
            Assert.AreEqual(Enums.ApplicationStatus.Registration, application.Status,
                "Application should be reset to NewApplication after cancellation");

            // Clean up
            Shelter.ClearInstance();
        }
    }
}