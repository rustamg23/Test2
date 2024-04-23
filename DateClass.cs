namespace MyagkieLapki
{
    public class DateClass
    {
        private static DateClass instance;
        private static readonly object syncRoot = new object();
        public DateTime Date { get; private set; }

        private DateClass()
        {
            Date = DateTime.Now;
        }

        public static DateClass GetInstance()
        {
            if (instance == null)
            {
                lock (syncRoot)
                {
                    if (instance == null)
                    {
                        instance = new DateClass();
                    }
                }
            }
            return instance;
        }
    }
}