namespace MyagkieLapki
{
    public class Animal
    {
        public AnimalPassport Passport { get; set; }
        public Enums.AnimalStatus Status { get; set; }
        public DateTime AdmitDate { get; set; }
        public Employee PinnedEmployee { get; set; }
        public Cage Cage { get; set; }
        public int AnimalId { get; set; }
        public int Happiness { get; set; } = 0;
        
        public Animal(AnimalPassport passport, Cage cage)
        {
            Passport = passport;
            AdmitDate = DateTime.Now;
            Cage = cage;
            Status = Enums.AnimalStatus.NewAnimal;
        }

        public void AcceptAnimal()
        {
            Status = Enums.AnimalStatus.Accepted; // Животное принято в приют
        }
        
        public bool AdoptAnimal()
        {
            if (Status == Enums.AnimalStatus.ReadyForAdoption)
            {
                Status = Enums.AnimalStatus.Adopted;
                Cage.PrepareForNewAnimal();
                return true;
            }
            else
            {
                Console.WriteLine("Animal is not ready for adoption");
                return false;
            }
        }
        public void AdministerMedication(string medication, DateTime date, Employee admin)
        {
            if (Status == Enums.AnimalStatus.Healing)
            {
                Passport.AddMedicalRecord($"Medication: {medication} administered by {admin.FullName} on {date.ToString("yyyy-MM-dd")}");
                Happiness += 1;
            }
            else
            {
                Console.WriteLine("Animal status is not healing right now");
            }
        }

        public void InteractWithEmployee(Employee employee, string interactionType)
        {
            // Логика взаимодействия, влияющая на состояние животного
            if (interactionType == "play") Happiness += 2;
            else if (interactionType == "groom") Happiness += 1;
            Console.WriteLine($"Interaction: {interactionType} with {employee.FullName}");
        }

        public void MarkAsDeceased(DateTime dateOfDeath)
        {
            // Отметить животное как умершее и вызвать нужные методы
            Passport.AddMedicalRecord($"Death: Animal deceased on {dateOfDeath.ToString("yyyy-MM-dd")}");
            Status = Enums.AnimalStatus.Dead;
            Cage.PrepareForNewAnimal();
        }
    }
}