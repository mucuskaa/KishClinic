namespace KishClinic.Models
{
    public class SessionDto
    {
        public int ClientID { get; set; }
        public DateTime SessionDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan FinishTime { get; set; }
        public string Status { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public int SessionTypeID { get; set; }
    }
}
