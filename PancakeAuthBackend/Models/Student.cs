using System.Text.Json.Serialization;

namespace PancakeAuthBackend.Models {
    public class Student {
        public int Id { get; set; }
        public string StudentUID { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string CityOfOrigin { get; set; } = null!;
        public string StateOfOrigin { get; set; } = null!;
        public string CountryOfOrigin { get; set; } = null!;
        public string Nationality { get; set; } = null!;
        public Grade Grade { get; set; } = null!;
        public Batch Batch { get; set; } = null!;
        public School School { get; set; } = null!;

        public int GradeId { get; set; }
        public int BatchId { get; set; }
        public int SchoolId { get; set; }
    }
}
