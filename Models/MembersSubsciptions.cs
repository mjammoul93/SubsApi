namespace SubsApi.Models
{
    public class MembersSubsciptions
    {
        public int Id { get; set; }
        public AppUser Member { get; set; } = new AppUser();
        public GymSubscriptionType Subscription { get; set; } = new GymSubscriptionType();
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set;}
        public DateTime CreatedDate { get; set; }
        public bool Active { get;set; }
    }
}
