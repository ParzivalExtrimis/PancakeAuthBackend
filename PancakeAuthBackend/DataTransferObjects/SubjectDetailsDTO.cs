namespace PancakeAuthBackend.DataTransferObjects {
    public class SubjectDetailsDTO {
        public string Name { get; set; } = null!;
        public ICollection<ChapterDTO> Chapters { get; set; } = null!;
    }
}
