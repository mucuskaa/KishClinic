namespace KishClinic.Entities
{
    public class SessionType
    {
        public int ID { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal DefaultFee { get; set; }
        public int DefaultDuration { get; set; }
    }
}
