namespace PancakeAuthBackend.Models {
    public class Subject {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<Chapter> Chapters { get; set; } = null!;
        public ICollection<Batch> Batches { get; set; } = null!;  
    }
}
