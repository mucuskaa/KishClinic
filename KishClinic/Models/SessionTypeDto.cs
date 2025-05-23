namespace KishClinic.Models
{
    public class SessionTypeDto
    {
        public string Description { get; set; } = string.Empty;
        public decimal DefaultFee { get; set; }
        public int DefaultDuration { get; set; }
    }
}
