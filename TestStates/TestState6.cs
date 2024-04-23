using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace MyagkieLapki.Tests
{
    [TestFixture]
    public class TestState6
    {
        [Test]
        public void ApplicationProcess_Rejection()
        {
            // Setup
            var shelter = Shelter.GetInstance();
            var client = new Client("Jane Smith", DateTime.Now);
            shelter.RegisterClient(client);
            var cage = shelter.AddCage(1, "Medium");
            var passport = new AnimalPassport("Charlie", DateTime.Now.AddYears(-2), "Ginger", "Fluffy",
                new List<string>());
            var animal = new Animal(passport, cage);
            animal.AcceptAnimal();

            // Act
            var application = client.SendApplication(shelter, animal, Enums.ApplicationType.Adoption, "APP002");
            client.PayFeeForAction(shelter, application, 100);
            application.UpdateApplicationStatus(Enums.ApplicationStatus.Rejected); // Reject the application

            // Asserts
            Assert.AreEqual(Enums.ApplicationStatus.Rejected, application.Status, "Application should be rejected");

            // Clean up
            Shelter.ClearInstance();
        }
    }
}