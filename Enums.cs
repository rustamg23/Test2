namespace MyagkieLapki
{
    public class Enums
    {
        public enum ApplicationStatus
        {
            NewApplication,        // Новая заявка
            AwaitRegistrationPayment, // Ожидание оплаты регистрации
            Registration,          // Регистрация заявки
            Approved,              // Одобрено
            Rejected               // Отклонено
        }

        public enum ApplicationType
        {
            Adoption,  // Усыновление животного из приюта
            Surrender  // Сдача животного клиентом в приют
        }

        public enum AnimalStatus
        {
            NewAnimal,
            Accepted,
            Healing,
            ReadyForAdoption,
            Adopted,
            Dead
        }


    }
}