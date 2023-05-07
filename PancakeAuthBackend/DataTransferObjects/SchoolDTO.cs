namespace PancakeAuthBackend.DataTransferObjects {
    public class SchoolDTO {
        public string Name { get; set; } = null!;
        public string StreetName { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Region { get; set; } = null!;
        public string State { get; set; } = null!;
        public string PostalCode { get; set; } = null!;
        public string Country { get; set; } = null!;
    }

    public class RegisterSchoolDTO : SchoolDTO {
        public string ManagerFirstName { get; set; } = null!;   
        public string ManagerLastName { get; set; } = null!;   
        public string ManagerPicture { get; set; } = null!;   
        public string ManagerEmail { get; set; } = null!;   
        public string ManagerPhone { get; set; } = null!;   
        public string ManagerPassword { get; set; } = null!;   
    }
}
