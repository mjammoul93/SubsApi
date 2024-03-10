namespace SubsApi.Models
{
    public class GymSubscriptionType
    {
        public int Id { get; set; } 
        public string SubscriptionType { get; set; }
        public int ValidityInDays { get; set; }
        public float Price { get; set; }
    }
}
