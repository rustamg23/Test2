using NUnit.Framework;
using System;

namespace MyagkieLapki.Tests
{
    [TestFixture]
    public class ClientTests
    {
        private Client _client;
        private Shelter _shelter;
        private Animal _animal;
        private Application _application;

        [SetUp]
        public void Setup()
        {
            _client = new Client("Bob Smith", DateTime.Today);
            _shelter = Shelter.GetInstance();
            _animal = new Animal(new AnimalPassport("Spot", DateTime.Today, "Spotted", "Smooth", new List<string>()), new Cage(1, "Large"));
            _application = new Application(_animal, _client, Enums.ApplicationType.Adoption, "APP001");
        }

        [Test]
        public void Constructor_InitializesPropertiesCorrectly()
        {
            Assert.AreEqual("Bob Smith", _client.FullName);
            Assert.IsNotNull(_client.Applications);
            Assert.IsTrue(DateTime.Today.Subtract(_client.ClientFromDate).TotalSeconds < 1);
        }

        [Test]
        public void SendApplication_CreatesAndRegistersApplication()
        {
            // Assuming the application needs a valid client and animal
            var application = _client.SendApplication(_shelter, _animal, Enums.ApplicationType.Adoption, "APP002");
            Assert.IsTrue(_shelter.Applications.Contains(application));
            Assert.AreEqual("APP002", application.ID);
            Assert.AreEqual(_client, application.Client);
            Assert.AreEqual(Enums.ApplicationType.Adoption, application.Type);
        }

        [Test]
        public void PayFeeForAction_ProcessesPayment()
        {
            _client.Applications.Add(_application);
            _application.Status = Enums.ApplicationStatus.AwaitRegistrationPayment;
            var shelterBalance = _shelter.Balance;
            _client.PayFeeForAction(_shelter, _application, 100);
            Assert.AreEqual(Enums.ApplicationStatus.Registration, _application.Status);
            Assert.AreEqual(shelterBalance + 100, _shelter.Balance);
        }

        [Test]
        public void Register_RegistersClientInShelterAndSetsClientFromDate()
        {
            _shelter.Clients.Clear();  // Ensure the clients list is clear
            _client.Register(_shelter);
            Assert.IsTrue(_shelter.Clients.Contains(_client));
            // Assert that the date is set correctly upon registration
            Assert.That(_client.ClientFromDate, Is.EqualTo(DateTime.Today).Within(TimeSpan.FromSeconds(1)));
        }
        
        [Test]
        public void SendSurrenderApplication_CreatesAndRegistersSurrenderApplication()
        {
            var surrenderApplication = _client.SendApplication(_shelter, _animal, Enums.ApplicationType.Surrender, "APP003");
            Assert.IsTrue(_shelter.Applications.Contains(surrenderApplication));
            Assert.AreEqual("APP003", surrenderApplication.ID);
            Assert.AreEqual(Enums.ApplicationType.Surrender, surrenderApplication.Type);
            Assert.AreEqual(_client, surrenderApplication.Client);
        }

        [Test]
        public void PayFeeForSurrenderAction_ProcessesPayment()
        {
            var surrenderApplication = new Application(_animal, _client, Enums.ApplicationType.Surrender, "APP003");
            _client.Applications.Add(surrenderApplication);
            surrenderApplication.Status = Enums.ApplicationStatus.AwaitRegistrationPayment;
            var shelterBalanceBefore = _shelter.Balance;
            _client.PayFeeForAction(_shelter, surrenderApplication, _shelter.SurrenderFee);
            Assert.AreEqual(Enums.ApplicationStatus.Registration, surrenderApplication.Status);
            Assert.AreEqual(shelterBalanceBefore + _shelter.SurrenderFee, _shelter.Balance);
        }

    }
}
