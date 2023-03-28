namespace PancakeAuthBackend.Models {
    public class Subscription {
        public int Id { get; set; }
        public string Type { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int SchoolId { get; set; }
        public ICollection<Chapter> IncludedChapters { get; set; } = null!;
    }
}
