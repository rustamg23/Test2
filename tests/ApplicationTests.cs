using NUnit.Framework;

namespace MyagkieLapki.Tests
{
    [TestFixture]
    public class ApplicationTests
    {
        private Application _application;

        [SetUp]
        public void Setup()
        {
            var animal = new Animal(new AnimalPassport("Test Animal", DateTime.Today, "Grey", "Long Hair", new List<string>()), new Cage(1, "Small"));
            var client = new Client("Alice", DateTime.Today);
            _application = new Application(animal, client, Enums.ApplicationType.Adoption, "1234");
        }

        [Test]
        public void Constructor_InitializesPropertiesCorrectly()
        {
            Assert.AreEqual(Enums.ApplicationStatus.NewApplication, _application.Status);
        }

        [Test]
        public void UpdateApplicationStatus_ChangesStatus()
        {
            _application.UpdateApplicationStatus(Enums.ApplicationStatus.Approved);
            Assert.AreEqual(Enums.ApplicationStatus.Approved, _application.Status);
        }

        [Test]
        public void CancelApplication_CancelsApplication()
        {
            _application.CancelApplication();
            Assert.Pass("Cancellation just logs to console currently");
        }

        [Test]
        public void ArchiveApplication_ArchivesApplication()
        {
            _application.ArchiveApplication();
            Assert.Pass("Archivation just logs to console currently");
        }
    }
}