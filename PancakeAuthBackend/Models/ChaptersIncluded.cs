namespace PancakeAuthBackend.Models {
    public class ChaptersIncluded {
        public int ChapterId { get; set; }
        public int SubscriptionId { get; set; }
        public Chapter Chapter { get; set; } = null!;
        public Subscription Subscription { get; set; } = null!;
    }
}
