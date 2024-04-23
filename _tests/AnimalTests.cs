// using NUnit.Framework;
// using MyagkieLapki;
// using System.Collections.Generic;
// using System.Linq;
//
//
// [TestFixture]
// public class AnimalTests
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
//     public void Constructor_InitializesPassportAndAdmitDate()
//     {
//         var passport = new AnimalPassport("Buddy", DateTime.Today, "Brown", "Friendly",
//             new List<string> { "all good" });
//         var cage = _shelter.AddCage(1, "1m^2");
//         var animal = new Animal(passport, cage);
//
//         Assert.AreEqual(passport, animal.Passport);
//         Assert.AreEqual(DateTime.Today, animal.AdmitDate.Date);
//     }
//
//     [Test]
//     public void TestAdministerMedication_IncreasesHappiness()
//     {
//         var passport = new AnimalPassport("Buddy", DateTime.Today, "Brown", "Friendly",
//             new List<string> { "all good" });
//         var cage = _shelter.AddCage(1, "1m^2");
//         var animal = new Animal(passport, cage);
//         var employee = new Employee("John Doe", DateTime.Today, 0);
//         employee.PinAnimal(animal);
//         animal.PinnedEmployee = employee;
//         int initialHappiness = animal.Happiness;
//         animal.AdministerMedication("Antibiotic", DateTime.Today, employee);
//         Assert.IsTrue(animal.Happiness > initialHappiness, "Happiness should increase after medication.");
//     }
//
//     [Test]
//     public void TestInteractWithEmployee_IncreasesHappiness()
//     {
//         var passport = new AnimalPassport("Buddy", DateTime.Today, "Brown", "Friendly",
//             new List<string> { "all good" });
//         var cage = _shelter.AddCage(1, "1m^2");
//         var animal = new Animal(passport, cage);
//         var employee = new Employee("John Doe", DateTime.Today, 0);
//         int initialHappiness = animal.Happiness;
//         animal.InteractWithEmployee(employee, "play");
//         Assert.AreEqual(initialHappiness + 2, animal.Happiness, "Happiness should increase by 2 when playing.");
//     }
//
//     [Test]
//     public void TestCleanUpAfterDeath_CleansCage()
//     {
//         var passport = new AnimalPassport("Buddy", DateTime.Today, "Brown", "Friendly", new List<string> { "all good" });
//         var cage = _shelter.AddCage(1, "1m^2");
//         var animal = new Animal(passport, cage);
//         animal.Cage.IsDirty = true;  // Simulate a dirty cage
//         animal.MarkAsDeceased(DateTime.Today);
//         Assert.IsFalse(animal.Cage.IsBusy, "Cage should not be busy after death.");
//         Assert.IsFalse(animal.Cage.IsDirty, "Cage should be clean after death.");
//     }
// }