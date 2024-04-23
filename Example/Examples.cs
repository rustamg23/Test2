using NUnit.Framework;
using System;

namespace MyagkieLapki.Tests
{
    [TestFixture]
    public class Examples
    {
        private Shelter shelter;
        private Client client;
        private Animal animal;
        private Cage cage;
        private Employee employee;

        [SetUp]
        public void Setup()
        {
            try
            {
                shelter = Shelter.GetInstance();
                client = new Client("John Doe", DateTime.Today.AddDays(-10));
                cage = new Cage(1, "Large");
                animal = new Animal(
                    new AnimalPassport("Rex", DateTime.Now.AddYears(-3), "Black", "Labrador", new List<string>()),
                    cage);

                employee = new Employee("Alice Johnson", DateTime.Now, 5);
                shelter.HireEmployee(employee);
                shelter.AddCage(cage.Id, cage.Size);
            }
            catch (Exception ex)
            {
                Assert.Fail($"Test failed with exception: {ex.Message}");
            }
        }

        [Test]
        public void TestSurrenderAndAdoptionProcess()
        {
            try
            {
                var shelter = Shelter.GetInstance();
                var client = new Client("John Doe", DateTime.Today.AddDays(-2));
                var surrenderCage = new Cage(1, "Large");
                var adoptionCage = new Cage(2, "Medium");
                var surrenderedAnimal =
                    new Animal(
                        new AnimalPassport("Rex", DateTime.Now.AddYears(-3), "Black", "Labrador", new List<string>()),
                        surrenderCage);
                var adoptableAnimal =
                    new Animal(
                        new AnimalPassport("Bella", DateTime.Now.AddYears(-2), "White", "Poodle", new List<string>()),
                        adoptionCage);
                var employee = new Employee("Alice Johnson", DateTime.Now, 5);

                shelter.HireEmployee(employee);
                shelter.AddCage(surrenderCage.Id, surrenderCage.Size);
                shelter.AddCage(adoptionCage.Id, adoptionCage.Size);
                this.employee.PinnedAnimals.Add(adoptableAnimal);
                adoptableAnimal.PinnedEmployee = this.employee;
                client.Register(shelter);
                Assert.Contains(client, shelter.Clients);

                // Client surrenders a pet
                var surrenderApplication = client.SendApplication(shelter, surrenderedAnimal,
                    Enums.ApplicationType.Surrender, "APP001");
                client.PayFeeForAction(shelter, surrenderApplication, 50); // Assuming surrender fee
                Assert.AreEqual(Enums.ApplicationStatus.Registration, surrenderApplication.Status);
                surrenderApplication.Status = Enums.ApplicationStatus.Approved;
                Assert.AreEqual(Enums.ApplicationStatus.Approved, surrenderApplication.Status);

                // Client adopts another pet
                var adoptionApplication =
                    client.SendApplication(shelter, adoptableAnimal, Enums.ApplicationType.Adoption, "APP002");
                client.PayFeeForAction(shelter, adoptionApplication, 100); // Assuming adoption fee
                Assert.AreEqual(Enums.ApplicationStatus.Registration, adoptionApplication.Status);
                employee.ProcessAdoption(adoptionApplication);
                shelter.TryAdmitAnimal(adoptableAnimal);
                Assert.IsTrue(adoptableAnimal.AdoptAnimal());
                Assert.AreEqual(Enums.AnimalStatus.Adopted, adoptableAnimal.Status);
                Assert.IsTrue(adoptionCage.IsReadyForNewAnimal());

                Assert.IsTrue(surrenderCage.IsReadyForNewAnimal());
            }
            catch (Exception ex)
            {
                Assert.Fail($"Test failed with exception: {ex.Message}");
            }
            finally
            {
                Shelter.ClearInstance();
            }
        }


        [Test]
        public void TestMultipleAnimalAdmissionsAndUpdates()
        {
            var shelter = Shelter.GetInstance();
            var animals = new List<Animal>
            {
                new Animal(
                    new AnimalPassport("Rex", DateTime.Now.AddYears(-3), "Black", "Labrador", new List<string>()),
                    new Cage(1, "Large")),
                new Animal(
                    new AnimalPassport("Bella", DateTime.Now.AddYears(-2), "White", "Poodle", new List<string>()),
                    new Cage(2, "Medium")),
                new Animal(new AnimalPassport("Max", DateTime.Now.AddYears(-1), "Brown", "Boxer", new List<string>()),
                    new Cage(3, "Small"))
            };
            var employees = new List<Employee>
            {
                new Employee("Alice Johnson", DateTime.Now, 5),
                new Employee("Bob Smith", DateTime.Now.AddYears(-1), 3),
                new Employee("Carol White", DateTime.Now.AddYears(-2), 6)
            };


            shelter.AddCage(0, "Large");
            shelter.AddCage(1, "Large");
            shelter.AddCage(2, "Large");
            employees.ForEach(e => shelter.HireEmployee(e));
            animals.ForEach(a =>
            {
                Assert.IsTrue(shelter.TryAdmitAnimal(a));
                a.AcceptAnimal();
                Assert.AreEqual(Enums.AnimalStatus.Accepted, a.Status);
            });

            // Each employee updates a different aspect of each animal
            for (int i = 0; i < employees.Count; i++)
            {
                string newName = $"New {animals[i].Passport.Name}";
                animals[i].Passport.UpdatePassportDetails(newName, animals[i].Passport.BirthDate,
                    animals[i].Passport.Color, animals[i].Passport.Appearance);
                Assert.AreEqual(newName, animals[i].Passport.Name);
                animals[i].Passport.AddMedicalRecord($"Checkup by {employees[i].FullName}");
            }

            Shelter.ClearInstance();
        }


        [Test]
        public void TestInteractionsBeforeAnimalDeath()
        {
            var shelter = Shelter.GetInstance();
            var animals = new List<Animal>
            {
                new Animal(
                    new AnimalPassport("Rex", DateTime.Now.AddYears(-3), "Black", "Labrador", new List<string>()),
                    new Cage(1, "Large")),
                new Animal(
                    new AnimalPassport("Bella", DateTime.Now.AddYears(-2), "White", "Poodle", new List<string>()),
                    new Cage(2, "Medium")),
                new Animal(new AnimalPassport("Max", DateTime.Now.AddYears(-1), "Brown", "Boxer", new List<string>()),
                    new Cage(3, "Small"))
            };
            var employee = new Employee("Alice Johnson", DateTime.Now, 5);

            shelter.HireEmployee(employee);
            animals.ForEach(a => { a.AcceptAnimal(); });

            // Play with the first animal
            animals[0].InteractWithEmployee(employee, "play");
            Assert.AreEqual(2, animals[0].Happiness);
            animals[0].MarkAsDeceased(DateTime.Now);
            Assert.AreEqual(Enums.AnimalStatus.Dead, animals[0].Status);

            // Administer medication to the second animal
            animals[1].Status = Enums.AnimalStatus.Healing;
            animals[1].AdministerMedication("Antibiotics", DateTime.Now, employee);
            Assert.AreEqual(1, animals[1].Happiness);
            animals[1].MarkAsDeceased(DateTime.Now);
            Assert.AreEqual(Enums.AnimalStatus.Dead, animals[1].Status);

            // The third animal dies without any prior interaction
            animals[2].MarkAsDeceased(DateTime.Now);
            Assert.AreEqual(Enums.AnimalStatus.Dead, animals[2].Status);

            animals.ForEach(a =>
            {
                a.Cage.CleanCage();
                Assert.IsTrue(a.Cage.IsReadyForNewAnimal());
            });

            Shelter.ClearInstance();
        }


        [TearDown]
        public void CleanUp()
        {
            Shelter.ClearInstance();
        }
    }
}