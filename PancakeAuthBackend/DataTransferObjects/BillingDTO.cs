namespace PancakeAuthBackend.DataTransferObjects {
    public class BillingDTO {
        public string status { get; set; } = null!;
        public DateTime dueDate { get; set; }
        public int Amount { get; set; }
        public string? Details { get; set; }
        public string School { get; set; } = null!;
    }
}
