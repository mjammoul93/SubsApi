using System.ComponentModel.DataAnnotations.Schema;

namespace SubsApi.DTOs
{
    [NotMapped]
    public class SubscriptionDetailsDTO
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string SubscriptionType { get; set; }
        public float Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int RemainingDays { get; set; }
    }
}
