using NUnit.Framework;

namespace MyagkieLapki.Tests
{
    [TestFixture]
    public class CageTests
    {
        private Cage _cage;

        [SetUp]
        public void Setup()
        {
            _cage = new Cage(1, "Large");
        }

        [Test]
        public void Constructor_InitializesPropertiesCorrectly()
        {
            Assert.AreEqual("Large", _cage.Size);
            Assert.AreEqual(1, _cage.Id);
            Assert.IsFalse(_cage.IsBusy);
            Assert.IsFalse(_cage.IsDirty);
        }

        [Test]
        public void CleanCage_SetsPropertiesCorrectly()
        {
            _cage.IsBusy = true;
            _cage.IsDirty = true;
            _cage.CleanCage();
            Assert.IsFalse(_cage.IsBusy);
            Assert.IsFalse(_cage.IsDirty);
        }

        [Test]
        public void PrepareForNewAnimal_CleansIfNecessary()
        {
            _cage.IsDirty = true;
            _cage.PrepareForNewAnimal();
            Assert.IsFalse(_cage.IsDirty);
        }
        
        [Test]
        public void IsReadyForNewAnimal_ReturnsTrue_WhenCageIsCleanAndNotOccupied()
        {
            _cage.IsBusy = false;
            _cage.IsDirty = false;
            Assert.IsTrue(_cage.IsReadyForNewAnimal(), "Cage should be ready for a new animal when it is not busy and clean.");
        }

        [Test]
        public void IsReadyForNewAnimal_ReturnsFalse_WhenCageIsOccupied()
        {
            _cage.IsBusy = true;
            _cage.IsDirty = false;
            Assert.IsFalse(_cage.IsReadyForNewAnimal(), "Cage should not be ready for a new animal when it is occupied.");
        }

        [Test]
        public void IsReadyForNewAnimal_ReturnsFalse_WhenCageIsDirty()
        {
            _cage.IsBusy = false;
            _cage.IsDirty = true;
            Assert.IsFalse(_cage.IsReadyForNewAnimal(), "Cage should not be ready for a new animal when it is dirty.");
        }
    }
}