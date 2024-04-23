using NUnit.Framework;
using System;
using System.Linq;

namespace MyagkieLapki.Tests
{
    [TestFixture]
    public class ShelterTests
    {
        private Shelter _shelter;

        [SetUp]
        public void Setup()
        {
            // Ensure we are working with a clean instance for each test
            Shelter.ClearInstance();
            _shelter = Shelter.GetInstance();
        }

        [Test]
        public void GetInstance_ReturnsSingletonInstance()
        {
            var instance1 = Shelter.GetInstance();
            var instance2 = Shelter.GetInstance();
            Assert.AreSame(instance1, instance2);
        }

        [Test]
        public void Constructor_InitializesPropertiesCorrectly()
        {
            Assert.AreEqual(100, _shelter.AdoptionFee);
            Assert.AreEqual(50, _shelter.SurrenderFee);
            Assert.AreEqual(10000, _shelter.Balance);
            Assert.IsEmpty(_shelter.Applications);
            Assert.IsEmpty(_shelter.Animals);
            Assert.IsEmpty(_shelter.Employees);
            Assert.IsEmpty(_shelter.Cages);
            Assert.IsEmpty(_shelter.Clients);
        }

        [Test]
        public void Clear_ResetsProperties()
        {
            // Adding dummy data to test clear functionality
            _shelter.Animals.Add(new Animal(new AnimalPassport("Test", DateTime.Now, "Color", "Type", new List<string>()), new Cage(1, "Small")));
            _shelter.Clear();
            Assert.AreEqual(0, _shelter.Balance);
            Assert.IsEmpty(_shelter.Applications);
            Assert.IsEmpty(_shelter.Animals);
            Assert.IsEmpty(_shelter.Employees);
            Assert.IsEmpty(_shelter.Cages);
            Assert.IsEmpty(_shelter.Clients);
            Assert.AreEqual(100, _shelter.AdoptionFee);
            Assert.AreEqual(50, _shelter.SurrenderFee);
        }

        [Test]
        public void CollectFee_IncreasesBalance()
        {
            int initialBalance = _shelter.Balance;
            _shelter.CollectFee(200);
            Assert.AreEqual(initialBalance + 200, _shelter.Balance);
        }

        [Test]
        public void TryAdmitAnimal_SucceedsWhenCageAvailable()
        {
            _shelter.Cages.Add(new Cage(1, "Medium"));
            var animal = new Animal(new AnimalPassport("Buddy", DateTime.Today, "Black", "Short", new List<string>()), _shelter.Cages.First());
            var employee = new Employee("John Doe", DateTime.Today, 5);
            _shelter.Employees.Add(employee);

            bool result = _shelter.TryAdmitAnimal(animal);
            Assert.IsTrue(result);
            Assert.IsTrue(_shelter.Cages.First().IsBusy);
            Assert.AreSame(animal, _shelter.Cages.First().Animal);
            Assert.Contains(animal, _shelter.Animals);
            Assert.AreEqual(employee, animal.PinnedEmployee);
            Assert.Contains(animal, employee.PinnedAnimals);
        }

        [Test]
        public void ReleaseAnimal_ChangesAnimalAndEmployeeState()
        {
            var cage = new Cage(1, "Medium");
            var animal = new Animal(new AnimalPassport("Charlie", DateTime.Today, "White", "Fluffy", new List<string>()), cage);
            var employee = new Employee("Alice Wonderland", DateTime.Today, 10);
            _shelter.Employees.Add(employee);
            _shelter.Animals.Add(animal);
            employee.PinAnimal(animal);
            animal.PinnedEmployee = employee;

            _shelter.ReleaseAnimal(animal);
            Assert.IsFalse(employee.PinnedAnimals.Contains(animal));
        }

        [Test]
        public void AddCage_AddsCageCorrectly()
        {
            var cage = _shelter.AddCage(2, "Large");
            Assert.AreEqual(1, _shelter.Cages.Count);
            Assert.AreEqual("Large", cage.Size);
            Assert.AreEqual(2, cage.Id);
        }

        [Test]
        public void HireEmployee_AddsEmployee()
        {
            var employee = new Employee("Derek Shepherd", DateTime.Today.AddYears(-3), 3);
            _shelter.HireEmployee(employee);
            Assert.IsTrue(_shelter.Employees.Contains(employee));
        }

        [Test]
        public void FireEmployee_RemovesEmployee()
        {
            var employee = new Employee("Meredith Grey", DateTime.Today.AddYears(-5), 5);
            _shelter.HireEmployee(employee);
            _shelter.FireEmployee(employee);
            Assert.IsFalse(_shelter.Employees.Contains(employee));
        }

        [Test]
        public void RegisterApplication_AddsApplication()
        {
            var client = new Client("Cristina Yang", DateTime.Today);
            var animal = new Animal(new AnimalPassport("Lexie", DateTime.Today, "Gray", "Short", new List<string>()), new Cage(1, "Medium"));
            var application = new Application(animal, client, Enums.ApplicationType.Adoption, "APP-123");
            _shelter.RegisterApplication(application);
            Assert.Contains(application, _shelter.Applications);
        }

        [Test]
        public void RegisterClient_AddsClient()
        {
            var client = new Client("Alex Karev", DateTime.Today);
            _shelter.RegisterClient(client);
            Assert.IsTrue(_shelter.Clients.Contains(client));
        }

        [Test]
        public void PayEmployee_DecreasesBalanceAndPaysEmployee()
        {
            var employee = new Employee("Izzie Stevens", DateTime.Today.AddYears(-2), 2);
            _shelter.HireEmployee(employee);
            int salary = 500;
            _shelter.PayEmployee(employee, salary);
            Assert.AreEqual(10000 - salary, _shelter.Balance);
            // Assuming ReceivePayment logs payment to console in this implementation
        }
    }
}
