namespace MyagkieLapki;

using System;
using System.Collections.Generic;
using System.Threading;

public class Shelter
{
    private static Shelter instance;
    private static readonly object syncRoot = new object();

    public int AdoptionFee { get; set; }
    public int SurrenderFee { get; set; }
    public Dictionary<string, decimal> EmployeeSalary { get; set; }
    public int Balance { get; private set; }
    public List<Application> Applications { get; private set; }
    public List<Animal> Animals { get; private set; }
    public List<Employee> Employees { get; private set; }
    public List<Cage> Cages { get; private set; }
    public List<Client> Clients { get; private set; }

    private Shelter()
    {
        AdoptionFee = 100;
        SurrenderFee = 50;

        Balance = 10000;
        Applications = new List<Application>();
        Animals = new List<Animal>();
        Employees = new List<Employee>();
        Cages = new List<Cage>();
        Clients = new List<Client>();
    }

    public void Clear()
    {
        // Сбросить или повторно инициализировать все коллекции и значения
        Balance = 0;
        Applications.Clear();
        Animals.Clear();
        Employees.Clear();
        Cages.Clear();
        Clients.Clear();
        // Опционально восстановить начальные значения настроек, если нужно
        AdoptionFee = 100;
        SurrenderFee = 50;
    }
    
    public static Shelter GetInstance()
    {
        if (instance == null)
        {
            lock (syncRoot)
            {
                if (instance == null)
                {
                    instance = new Shelter();
                }
            }
        }

        return instance;
    }
    
    public static void ClearInstance()
    {
        lock (syncRoot)
        {
            instance = null;
        }
    }



    public void CollectFee(int amount)
    {
        Balance += amount;
        Console.WriteLine($"Balance topped up by {amount}");
    }

    public bool TryAdmitAnimal(Animal animal)
    {
        Cage availableCage = Cages.FirstOrDefault(c => !c.IsBusy);
        if (availableCage != null)
        {
            availableCage.Animal = animal;
            availableCage.IsBusy = true;
            Animals.Add(animal);
            Employee employee = Employees.FirstOrDefault();
            if (employee != null)
            {
                animal.PinnedEmployee = employee;
                employee.PinnedAnimals.Add(animal);
                animal.Status = Enums.AnimalStatus.ReadyForAdoption;
                Console.WriteLine($"Animal {animal.Passport.Name} admitted in cage {availableCage.Id} and {employee.FullName} pinned to them.");
                return true;
            }
            
        }
        Console.WriteLine("No available cages to admit the animal.");
        return false;
    }
    
    public void ReleaseAnimal(Animal animal)
    {
        var employee = animal.PinnedEmployee;
        if (employee.PinnedAnimals.Contains(animal))
        {
            employee.PinnedAnimals.Remove(animal);
            animal.Status = Enums.AnimalStatus.Adopted;
            Console.WriteLine($"Animal {animal.Passport.Name} has been released from {employee.FullName}.");
        }
        else
        {
            Console.WriteLine("This animal is not assigned to this employee.");
        }
    }

    public Cage AddCage(int id, string size)
    {
        var newCage = new Cage(id, size);
        Cages.Add(newCage);
        return newCage;
    }

    public void HireEmployee(Employee employee)
    {
        Employees.Add(employee);
        Console.WriteLine($"Employee hired: {employee.FullName}");
    }

    public void FireEmployee(Employee employee)
    {
        Employees.Remove(employee);
        Console.WriteLine($"Employee fired: {employee.FullName}");
    }

    public void RegisterApplication(Application application)
    {
        Applications.Add(application);
        application.UpdateApplicationStatus(Enums.ApplicationStatus.AwaitRegistrationPayment);
        Console.WriteLine($"Application registered: {application.Client.FullName}");
    }

    public void RegisterClient(Client client)
    {
        Clients.Add(client);
        Console.WriteLine($"Client registered: {client.FullName}");
    }
    
    public void PayEmployee(Employee employee, int salary)
    {
        if (Balance >= salary)
        {
            Balance -= salary;
            employee.ReceivePayment(salary);
            Console.WriteLine($"Paid {salary} to {employee.FullName}. Remaining balance: {Balance}.");
        }
        else
        {
            Console.WriteLine("Insufficient funds to pay salary.");
        }
    }
}