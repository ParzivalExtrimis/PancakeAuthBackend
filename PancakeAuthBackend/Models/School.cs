namespace PancakeAuthBackend.Models {
    public class School {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<Student>? Students { get; set; }
        public ICollection<Subscription> Subscriptions { get; set; } = null!;
        public ICollection<Batch>? Batches { get; set; } = null!;
        public Address Address { get; set; } = null!;
        public ICollection<Payment>? Payments { get; set; }
        public int AddressId { get; set; }
    }
}
