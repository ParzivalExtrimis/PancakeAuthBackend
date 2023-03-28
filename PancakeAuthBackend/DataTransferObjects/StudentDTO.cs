using System.Text.Json.Serialization;

namespace PancakeAuthBackend.DataTransferObjects {
    public class StudentDTO {
        public string StudentUID { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string CityOfOrigin { get; set; } = null!;
        public string StateOfOrigin { get; set; } = null!;
        public string CountryOfOrigin { get; set; } = null!;
        public string Nationality { get; set; } = null!;
        public string Grade { get; set; } = null!;
        public string Batch { get; set; } = null!;
        public string School { get; set; } = null!;
    }
}
