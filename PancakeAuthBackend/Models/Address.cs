using System.ComponentModel.DataAnnotations.Schema;

namespace PancakeAuthBackend.Models {
    public class Address {
        public int Id { get; set; }
        public School? School { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string StreetName { get; set; } = null!;

        [Column(TypeName = "varchar(50)")]
        public string City { get; set; } = null!;

        [Column(TypeName = "varchar(50)")]
        public string Region { get; set; } = null!;

        [Column(TypeName = "varchar(50)")]
        public string State { get; set; } = null!;

        [Column(TypeName = "varchar(30)")]
        public string PostalCode { get; set; } = null!;

        [Column(TypeName = "varchar(50)")]
        public string Country { get; set; } = null!;
    }
}
