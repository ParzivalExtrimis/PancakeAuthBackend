namespace PancakeAuthBackend.DataTransferObjects {
    public class SchoolDTO {
        public string Name { get; set; } = null!;
        public ICollection<string> Subscriptions { get; set; } = null!;
        public string StreetName { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Region { get; set; } = null!;
        public string State { get; set; } = null!;
        public string PostalCode { get; set; } = null!;
        public string Country { get; set; } = null!;
    }
}
