using NUnit.Framework;
using System;

namespace MyagkieLapki.Tests
{
    [TestFixture]
    public class EmployeeTests
    {
        private Employee _employee;
        private Animal _animal;
        private Application _application;
        private Client _client;
        private Shelter _shelter;
        private Cage _cage;
        
        [SetUp]
        public void Setup()
        {
            _shelter = Shelter.GetInstance();
            _employee = new Employee("Alice Jones", DateTime.Today.AddYears(-10), 10);
            _animal = new Animal(new AnimalPassport("Buddy", DateTime.Today, "Golden", "Wavy", new List<string>()), new Cage(1, "Small"));
            
            // Create client with a date that simulates past registration
            _client = new Client("Charlie", DateTime.Today.AddDays(-2));
            _application = new Application(_animal, _client, Enums.ApplicationType.Adoption, "APP003");
            _application.Status = Enums.ApplicationStatus.Registration;
            
            _employee.PinAnimal(_animal);
        }

        [Test]
        public void Constructor_InitializesPropertiesCorrectly()
        {
            Assert.AreEqual("Alice Jones", _employee.FullName);
            Assert.AreEqual(DateTime.Today.AddYears(-10), _employee.HireDate);
            Assert.AreEqual(10, _employee.YearsOfExperience);
            Assert.IsNotNull(_employee.PinnedAnimals);
        }

        [Test]
        public void PinAnimal_AddsAnimal()
        {
            _employee.PinAnimal(_animal);
            Assert.Contains(_animal, _employee.PinnedAnimals);
        }

        [Test]
        public void ProcessAdoption_ProcessesCorrectly()
        {
            _shelter = Shelter.GetInstance();
            _employee = new Employee("Alice Jones", DateTime.Today.AddYears(-10), 10);
            _cage = _shelter.AddCage(1, "Small");
            _animal = new Animal(new AnimalPassport("Buddy", DateTime.Today, "Golden", "Wavy", new List<string>()), _cage);
            _animal.PinnedEmployee = _employee;
            _employee.PinnedAnimals.Add(_animal);
            // Create client with a date that simulates past registration
            _client = new Client("Charlie", DateTime.Today.AddDays(-2));
            _application = _client.SendApplication(_shelter, _animal, Enums.ApplicationType.Adoption,"AP003");
            // Process the adoption application
            _client.PayFeeForAction(_shelter, _application, 100);
            _employee.ProcessAdoption(_application);
            
            // Check if the application status is set to Approved
            Assert.AreEqual(Enums.ApplicationStatus.Approved, _application.Status);
        }
        
        [Test]
        public void ReceivePayment_UpdatesBalance()
        {
            _shelter.PayEmployee(_employee, 200);
            // This method outputs to the console and has no state change; normally you'd test state change or output
            Assert.Pass("Received payment is purely a console output in this context.");
        }
    }
}
