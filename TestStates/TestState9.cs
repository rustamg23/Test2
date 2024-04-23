using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace MyagkieLapki.Tests
{
    [TestFixture]
    public class TestState9
    {
        [Test]
        public void ApplicationProcess_ApprovedThenRejected()
        {
            // Setup
            var shelter = Shelter.GetInstance();
            var client = new Client("Sarah Connor", DateTime.Now); // Client registers today
            shelter.RegisterClient(client);
            var cage = shelter.AddCage(3, "Medium");
            var passport = new AnimalPassport("Buster", DateTime.Now.AddYears(-1), "Tan", "Short", new List<string>());
            var animal = new Animal(passport, cage);
            animal.AcceptAnimal();
            animal.Status = Enums.AnimalStatus.ReadyForAdoption;

            // Act
            var application = client.SendApplication(shelter, animal, Enums.ApplicationType.Adoption, "APP005");
            client.PayFeeForAction(shelter, application, 100); // Pay registration fee
            application.UpdateApplicationStatus(Enums.ApplicationStatus.Approved); // Initially approve the application
            application.UpdateApplicationStatus(Enums.ApplicationStatus
                .Rejected); // Then reject due to client registration timing

            // Asserts
            Assert.AreEqual(Enums.ApplicationStatus.Rejected, application.Status,
                "Application should be rejected due to recent client registration");

            // Clean up
            Shelter.ClearInstance();
        }
    }
}