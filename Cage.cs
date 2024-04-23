namespace MyagkieLapki
{
    public class Cage
    {
        public Animal? Animal { get; set; }
        public string Size { get; set; }
        public int Id { get; private set; }
        public bool IsBusy { get; set; }
        public bool IsDirty { get; set; } = false;
        
        public Cage(int id, string size)
        {
            Id = id;
            Size = size;
            Animal = null;
            IsBusy = false;
        }
        
        public void CleanCage()
        {
            // Очистка клетки
            IsBusy = false;
            IsDirty = false;
            Console.WriteLine("Cage cleaned and ready for new animal");
        }

        // Replaces CheckOccupancyStatus
        public bool IsReadyForNewAnimal()
        {
            // Check if the cage is clean and not occupied
            return !IsBusy && !IsDirty;
        }

        public void PrepareForNewAnimal()
        {
            if (IsReadyForNewAnimal())
            {
                Console.WriteLine("Cage is ready for a new animal.");
            }
            else
            {
                CleanCage();
            }
        }
    }
}