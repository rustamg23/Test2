namespace MyagkieLapki
{
    public class Application
    {
        public Animal Animal { get; set; }
        public Client Client { get; set; }
        public Enums.ApplicationStatus Status { get; set; }
        public Enums.ApplicationType Type { get; set; }
        public string ID { get; set; }

        public Application(Animal animal, Client client, Enums.ApplicationType type, string id)
        {
            Animal = animal;
            Client = client;
            Type = type;
            ID = id;
            Status = Enums.ApplicationStatus.NewApplication; // По умолчанию новая заявка
        }
        
        public void UpdateApplicationStatus(Enums.ApplicationStatus newStatus)
        {
            // Обновление статуса заявки
            Status = newStatus;
            Console.WriteLine($"Application status updated to {newStatus}");
        }

        public void CancelApplication()
        {
            // Отмена заявки
            Console.WriteLine("Application canceled");
        }

        public void ArchiveApplication()
        {
            // Архивирование заявки
            Console.WriteLine("Application archived");
        }
    }
}