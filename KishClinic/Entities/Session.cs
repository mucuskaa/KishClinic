namespace KishClinic.Entities
{
    public class Session
    {
        public int ID { get; set; }
        public int ClientID { get; set; }
        public DateTime SessionDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan FinishTime { get; set; }
        public string Status { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public int SessionTypeID { get; set; }
        public SessionType? SessionType { get; set; }
    }
}
