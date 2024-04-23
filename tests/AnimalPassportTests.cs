using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace MyagkieLapki.Tests
{
    [TestFixture]
    public class AnimalPassportTests
    {
        private AnimalPassport _passport;

        [SetUp]
        public void Setup()
        {
            _passport = new AnimalPassport("Fido", DateTime.Today, "Brown", "Furry", new List<string>());
        }

        [Test]
        public void Constructor_InitializesPropertiesCorrectly()
        {
            Assert.AreEqual("Fido", _passport.Name);
            Assert.AreEqual(DateTime.Today, _passport.BirthDate);
            Assert.AreEqual("Brown", _passport.Color);
            Assert.AreEqual("Furry", _passport.Appearance);
            Assert.IsNotNull(_passport.MedicalRecords);
        }

        [Test]
        public void AddMedicalRecord_AddsRecord()
        {
            _passport.AddMedicalRecord("Vaccinated for rabies");
            Assert.Contains("Vaccinated for rabies", _passport.MedicalRecords);
        }

        [Test]
        public void UpdatePassportDetails_UpdatesDetails()
        {
            _passport.UpdatePassportDetails("Rex", DateTime.Today.AddDays(-365), "Black", "Short-Hair");
            Assert.AreEqual("Rex", _passport.Name);
            Assert.AreEqual(DateTime.Today.AddDays(-365), _passport.BirthDate);
            Assert.AreEqual("Black", _passport.Color);
            Assert.AreEqual("Short-Hair", _passport.Appearance);
        }
    }
}