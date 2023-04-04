﻿namespace PancakeAuthBackend.Models {
    public class Address {
        public int Id { get; set; }
        public string SchoolName { get; set; } = null!;
        public string StreetName { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Region { get; set; } = null!;
        public string State { get; set; } = null!;
        public string PostalCode { get; set; } = null!;
        public string Country { get; set; } = null!;
    }
}
