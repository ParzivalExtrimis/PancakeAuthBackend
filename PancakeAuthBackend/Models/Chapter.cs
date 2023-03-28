namespace PancakeAuthBackend.Models {
    public class Chapter {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public Subscription Subscription { get; set; } = null!;
        public Subject Subject { get; set; } = null!;
        public int SubscriptionId { get; set; }
    }
}
