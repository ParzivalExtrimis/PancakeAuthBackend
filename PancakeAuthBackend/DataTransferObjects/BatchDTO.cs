namespace PancakeAuthBackend.DataTransferObjects {
    public class BatchDTO {
        public string Name { get; set; } = null!;
        public string Grade { get; set; } = null!;
        public List<string> Subjects { get; set; } = null!;
    }
}
