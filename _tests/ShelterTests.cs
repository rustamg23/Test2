// using NUnit.Framework;
// using MyagkieLapki;
// using System.Collections.Generic;
// using System.Linq;
//
// [TestFixture]
// public class ShelterTests
// {
//     private Shelter _shelter;
//
//     [SetUp]
//     public void Setup()
//     {
//         _shelter = Shelter.GetInstance();
//     }
//
//     [Test]
//     public void testSingleton()
//     {
//         Shelter shelter1 = Shelter.GetInstance();
//         Shelter shelter2 = Shelter.GetInstance();
//         Assert.AreEqual(shelter1, shelter2);
//     }
//
//     [Test]
//     public void testProperties()
//     {
//         
//         Shelter shelter3 = Shelter.GetInstance();
//         shelter3.Clear();
//         Assert.That(shelter3.AdoptionFee, Is.EqualTo(100));
//         Assert.That(shelter3.SurrenderFee ,Is.EqualTo(50));
//         Assert.That(shelter3.Balance ,Is.EqualTo(0));
//         Assert.That(shelter3.Applications, Is.EqualTo(new List<Application>()));
//         Assert.That(shelter3.Animals, Is.EqualTo(new List<Animal>()));
//         Assert.That(shelter3.Employees ,Is.EqualTo(new List<Employee>()));
//         Assert.That(shelter3.Cages ,Is.EqualTo(new List<Cage>()));
//         Assert.That(shelter3.Clients ,Is.EqualTo(new List<Client>()));
//     }
//
//     [Test]
//     public void RegisterClient_AddsClientToShelter()
//     {
//         var client = new Client("John Doe");
//         _shelter.RegisterClient(client);
//         Assert.IsTrue(_shelter.Clients.Contains(client));
//     }
//
//     [Test]
//     public void RegisterApplication_AddsApplicationToShelter()
//     {
//         var client = new Client("John Doe");
//         var animalPassport = new AnimalPassport("Sharik", DateTime.Parse("09.09.2020"), "red", "no front right leg",
//             new List<string> { "all good" });
//         var cage = _shelter.AddCage(1, "1m^2");
//         var animal = new Animal(animalPassport, cage);
//         var application = new Application(animal, client, ApplicationEnums.ApplicationType.Adoption, "1");
//         _shelter.RegisterApplication(application);
//         Assert.IsTrue(_shelter.Applications.Contains(application));
//     }
//
//     [Test]
//     public void AdmitAnimal_AnimalAddedToShelterAndCageAssigned()
//     {
//         var animalPassport = new AnimalPassport("Sharik", DateTime.Parse("09.09.2020"), "red", "no front right leg",
//             new List<string> { "all good" });
//         var cage = _shelter.AddCage(2, "1m^2");
//         var animal = new Animal(animalPassport, cage);
//         var employee = new Employee("John Doe", DateTime.Now, 5);
//         employee.PinAnimal(animal);
//         animal.PinnedEmployee = employee;
//         _shelter.TryAdmitAnimal(animal);
//         Assert.IsTrue(_shelter.Animals.Contains(animal));
//         Assert.IsNotNull(animal.Cage);
//         Assert.IsTrue(employee.PinnedAnimals.Contains(animal));
//         Assert.That(animal.PinnedEmployee, Is.EqualTo(employee));
//     }
//
//     [Test]
//     public void ReleaseAnimal_RemovesAnimalFromPinnedAnimals()
//     {
//         var employee = new Employee("John Doe", DateTime.Now, 5);
//         var cage = _shelter.AddCage(1, "1m^2");
//         var animal =
//             new Animal(new AnimalPassport("Sharik", DateTime.Today, "Brown", "Healthy", new List<string> { "Healthy" }),
//                 cage);
//         _shelter.TryAdmitAnimal(animal);
//         _shelter.ReleaseAnimal(animal);
//
//
//         Assert.IsFalse(employee.PinnedAnimals.Contains(animal));
//     }
//
//     [Test]
//     public void HireEmployee_AddsEmployeeToShelter()
//     {
//         var employee = new Employee("Jane Doe", DateTime.Now, 5);
//         _shelter.HireEmployee(employee);
//         Assert.IsTrue(_shelter.Employees.Contains(employee));
//     }
//
//     [Test]
//     public void FireEmployee_RemovesEmployeeFromShelter()
//     {
//         var employee = new Employee("Jane Doe", DateTime.Now, 5);
//         _shelter.HireEmployee(employee);
//         Assert.IsTrue(_shelter.Employees.Contains(employee));
//         _shelter.FireEmployee(employee);
//         Assert.IsFalse(_shelter.Employees.Contains(employee));
//     }
//
//     [Test]
//     public void CollectFee_IncreasesShelterBalance()
//     {
//         int initialBalance = _shelter.Balance;
//         int fee = 100;
//         _shelter.CollectFee(fee);
//         // Assert.AreEqual(initialBalance + fee, _shelter.Balance);
//         Assert.That(initialBalance + fee, Is.EqualTo(_shelter.Balance));
//     }
// }