namespace PancakeAuthBackend.Models {
    public class Batch {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public Grade Grade { get; set; } = null!;
        public School School { get; set; } = null!;
        public ICollection<Subject> Subjects { get; set; } = null!;
        public int GradeId { get; set; }
        public int SchoolId { get; set; }
    }
}
