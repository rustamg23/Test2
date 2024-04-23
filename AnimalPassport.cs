namespace MyagkieLapki
{
    public class AnimalPassport
    {
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string Color { get; set; }
        public string Appearance { get; set; }
        public List<string> MedicalRecords { get; private set; }  // Использование List вместо массива

        public AnimalPassport(string name, DateTime birthDate, string color, string appearance, List<string> medicalRecords)
        {
            Name = name;
            BirthDate = birthDate;
            Color = color;
            Appearance = appearance;
            MedicalRecords = medicalRecords; // Инициализация пустым списком, если передан null
        }

        public void AddMedicalRecord(string record)
        {
            MedicalRecords.Add(record);
        }
        
        public void UpdatePassportDetails(string name, DateTime birthDate, string color, string appearance)
        {
            Name = name;
            BirthDate = birthDate;
            Color = color;
            Appearance = appearance;
        }

        public void TransferOwnershipToClient(Client client)
        {
            // Передача паспорта клиенту
            Console.WriteLine($"Passport transferred to {client.FullName}");
            // Здесь может быть логика удаления паспорта из системы
        }
    }
}