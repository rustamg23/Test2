using NUnit.Framework;
using System;

namespace MyagkieLapki.Tests
{
    [TestFixture]
    public class AnimalTests
    {
        private AnimalPassport _passport;
        private Cage _cage;
        private Animal _animal;

        [SetUp]
        public void Setup()
        {
            _passport = new AnimalPassport("Test Animal", DateTime.Today, "Black", "Short Hair", new List<string>());
            _cage = new Cage(1, "Medium");
            _animal = new Animal(_passport, _cage);
        }

        [Test]
        public void Constructor_InitializesPropertiesCorrectly()
        {
            Assert.AreEqual(_passport, _animal.Passport);
            Assert.AreEqual(_cage, _animal.Cage);
            Assert.AreEqual(0, _animal.Happiness);
            Assert.IsTrue(DateTime.Now.Subtract(_animal.AdmitDate).TotalSeconds < 1);
            Assert.AreEqual(Enums.AnimalStatus.NewAnimal, _animal.Status);
        }

        [Test]
        public void AcceptAnimal_ChangesStatusToAccepted()
        {
            _animal.AcceptAnimal();
            Assert.AreEqual(Enums.AnimalStatus.Accepted, _animal.Status);
        }

        [Test]
        public void AdoptAnimal_ChangesStatusToAdopted_WhenReadyForAdoption()
        {
            _animal.Status = Enums.AnimalStatus.ReadyForAdoption;
            bool result = _animal.AdoptAnimal();
            Assert.IsTrue(result);
            Assert.AreEqual(Enums.AnimalStatus.Adopted, _animal.Status);
        }

        [Test]
        public void AdoptAnimal_Fails_WhenNotReadyForAdoption()
        {
            _animal.Status = Enums.AnimalStatus.Healing;
            bool result = _animal.AdoptAnimal();
            Assert.IsFalse(result);
            Assert.AreEqual(Enums.AnimalStatus.Healing, _animal.Status);
        }

        [Test]
        public void AdministerMedication_IncreasesHappiness()
        {
            var status = _animal.Status;
            _animal.Status = Enums.AnimalStatus.Healing;
            _animal.AdministerMedication("Painkiller", DateTime.Now, new Employee("John Doe", DateTime.Now, 5));
            Assert.AreEqual(1, _animal.Happiness);
            _animal.Status = status;
        }

        [Test]
        public void InteractWithEmployee_IncreasesHappiness_Play()
        {
            var employee = new Employee("Jane Doe", DateTime.Now, 5);
            _animal.InteractWithEmployee(employee, "play");
            Assert.AreEqual(2, _animal.Happiness);
        }

        [Test]
        public void MarkAsDeceased_UpdatesPassportRecords()
        {
            _animal.MarkAsDeceased(DateTime.Now);
            Assert.IsTrue(_animal.Passport.MedicalRecords.Contains("Death: Animal deceased on " + DateTime.Now.ToString("yyyy-MM-dd")));
            Assert.IsFalse(_cage.IsBusy);
        }
    }
}