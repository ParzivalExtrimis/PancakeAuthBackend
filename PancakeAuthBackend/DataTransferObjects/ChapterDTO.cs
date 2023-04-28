namespace PancakeAuthBackend.DataTransferObjects {
    public class ChapterDTO {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string? Subject { get; set; }
    }
}
