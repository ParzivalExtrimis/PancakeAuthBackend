namespace PancakeAuthBackend.DataTransferObjects {
    public class SubscriptionDTO {
        public string Type { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public List<string> IncludedChapters { get; set; } = null!;
    }
}
