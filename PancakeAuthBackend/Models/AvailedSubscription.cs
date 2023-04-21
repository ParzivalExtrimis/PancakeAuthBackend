namespace PancakeAuthBackend.Models {
    public class AvailedSubscription {
        public int SchoolId { get; set; }
        public int SubscriptionId { get; set; }
        public School School { get; set; } = null!;
        public Subscription Subscription { get; set; } = null!;
    }
}
