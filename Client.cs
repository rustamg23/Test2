using System;
using System.Collections.Generic;

namespace MyagkieLapki
{
    public class Client
    {
        public string FullName { get; private set; }
        public List<Application> Applications { get; private set; }
        public DateTime ClientFromDate { get; private set; }

        public Client(string fullName, DateTime date)
        {
            FullName = fullName;
            Applications = new List<Application>();
            ClientFromDate = date;
        }

        public Application SendApplication(Shelter shelter, Animal animal, Enums.ApplicationType type, string app_id)
        {
            var application = new Application(animal, this, type, app_id);
            Applications.Add(application);
            shelter.RegisterApplication(application);
            Console.WriteLine($"Application sent by {FullName} for {type} of {animal.Passport.Name} registered successfully.");
            return application;
        }

        public void PayFeeForAction(Shelter shelter, Application application, int fee)
        {
            if (application.Status == Enums.ApplicationStatus.AwaitRegistrationPayment && Applications.Contains(application))
            {
                shelter.CollectFee(fee);
                application.Status = Enums.ApplicationStatus.Registration;
                Console.WriteLine($"{FullName} has paid a fee of {fee} for application ID: {application.ID}");
            }
            else
            {
                Console.WriteLine("Payment cannot be processed or application status is incorrect.");
            }
        }

        public void Register(Shelter shelter)
        {
            shelter.RegisterClient(this);
            Console.WriteLine($"{FullName} has been registered as a client since {ClientFromDate.ToShortDateString()}");
        }
    }
}