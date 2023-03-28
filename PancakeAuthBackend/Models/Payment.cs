namespace PancakeAuthBackend.Models {
    public class Payment {

        public int Id { get; set; }
        public string status { get; set; } = null!;
        public DateTime date { get; set; }
        public int Amount { get; set; }
        public string? Mode { get; set; }
        public string? Details { get; set; }
        [JsonIgnore]
        public School School { get; set; } = null!;
        [JsonIgnore]
        public int? SchoolId { get; set; }
    }
}
