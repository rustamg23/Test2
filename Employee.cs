namespace MyagkieLapki
{
    public class Employee
    {
        public string FullName { get; set; }
        public List<Animal> PinnedAnimals { get; private set; }
        public DateTime HireDate { get; set; }
        public int YearsOfExperience { get; set; }

        public Employee(string fullName, DateTime hireDate, int yearsOfExperience)
        {
            FullName = fullName;
            HireDate = hireDate;
            YearsOfExperience = yearsOfExperience;
            PinnedAnimals = new List<Animal>();
        }

        public void PinAnimal(Animal animal)
        {
            PinnedAnimals.Add(animal);
        }
        
        public void ProcessAdoption(Application application)
        {
            if (application.Type == Enums.ApplicationType.Adoption && application.Status == Enums.ApplicationStatus.Registration)
            {
                if ((DateTime.Now - application.Client.ClientFromDate).TotalDays >= 1)
                {
                    if (PinnedAnimals.Contains(application.Animal))
                    {
                        application.Status = Enums.ApplicationStatus.Approved;
                        application.ArchiveApplication();
                        var _shelter = Shelter.GetInstance();
                        _shelter.ReleaseAnimal(application.Animal);
                        
                        Console.WriteLine($"Adoption approved for {application.Animal.Passport.Name} by {FullName}.");
                    }
                }
                else
                {
                    application.Status = Enums.ApplicationStatus.Rejected;
                    application.CancelApplication();
                    Console.WriteLine($"Adoption rejected for {application.Animal.Passport.Name} as the client registered today.");
                }
            }
        }
        
        public void ReceivePayment(int amount)
        {
            // Получение оплаты от клиента
            Console.WriteLine($"{FullName} received payment of {amount}");
        }

    }
}