// using NUnit.Framework;
// using MyagkieLapki;
// using System.Collections.Generic;
// using System.Linq;
//
// [TestFixture]
// public class EmployeeAndClientTests
// {
//     private Shelter _shelter;
//
//     [SetUp]
//     public void Setup()
//     {
//         _shelter = Shelter.GetInstance();
//     }
//     [Test]
//     public void Constructor_InitializesEmployeeProperties_Correctly()
//     {
//         var hireDate = DateTime.Now;
//         var employee = new Employee("John Doe",  hireDate, 5);
//
//         Assert.AreEqual("John Doe", employee.FullName);
//         Assert.AreEqual(hireDate, employee.HireDate);
//         Assert.AreEqual(5, employee.YearsOfExperience);
//         Assert.IsNotNull(employee.PinnedAnimals);
//         Assert.IsEmpty(employee.PinnedAnimals);
//     }
//
//     [Test]
//     public void UpdatePassport_UpdatesPassportWithNewDetails()
//     {
//         var passport = new AnimalPassport("Buddy", DateTime.Today, "Brown", "Friendly", new List<string> { "all good" });
//         var cage = _shelter.AddCage(1, "1m^2");
//         var animal = new Animal(passport, cage);
//         var newMedicalRecords = "Vaccinated";
//         var employee = new Employee("John Doe", DateTime.Now, 10);
//         _shelter.HireEmployee(employee);
//         employee.PinAnimal(animal);
//         animal.PinnedEmployee = employee;
//
//         // employee.UpdateAnimalPassport(animal, "Buddy New", DateTime.Today.AddYears(-1), "Black", "Very Friendly",
//         //     newMedicalRecords);
//         Assert.AreEqual("Buddy New", animal.Passport.Name);
//         Assert.AreEqual(DateTime.Today.AddYears(-1), animal.Passport.BirthDate);
//         Assert.AreEqual("Black", animal.Passport.Color);
//         Assert.AreEqual("Very Friendly", animal.Passport.Appearance);
//         Assert.AreEqual(newMedicalRecords, animal.Passport.MedicalRecords[1]);
//     }
//
//     [Test]
//     public void ProcessAdoption_blablabla()
//     {
//         var passport = new AnimalPassport("Buddy", DateTime.Today, "Brown", "Friendly", new List<string> { "all good" });
//         var cage = _shelter.AddCage(2, "1m^2");
//         var animal = new Animal(passport, cage);
//         var employee = new Employee("John Doe", DateTime.Now, 10);
//         var client = new Client("Valeriy Zmushenko");
//         _shelter.HireEmployee(employee);
//         employee.PinAnimal(animal);
//         animal.PinnedEmployee = employee;
//         client.Register(_shelter);
//         Application application = client.SendApplication(_shelter, animal, ApplicationEnums.ApplicationType.Adoption, "A001");
//         client.PayFeeForAction(_shelter, application, 100);
//         employee.ProcessAdoption(application);
//         
//         
//     }
//     
//
//
// }
