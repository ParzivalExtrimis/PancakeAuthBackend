using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PancakeAuthBackend.Models {
    public class Student {
        public int Id { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string StudentUID { get; set; } = null!;

        [Column(TypeName = "varchar(100)")]
        public string Name { get; set; } = null!;

        [Column(TypeName = "varchar(50)")]
        public string Email { get; set; } = null!;

        [Column(TypeName = "varchar(50)")]
        public string PhoneNumber { get; set; } = null!;
        public User User { get; set; } = null!;

        [Column(TypeName = "varchar(100)")]
        public string CityOfOrigin { get; set; } = null!;

        [Column(TypeName = "varchar(100)")]
        public string StateOfOrigin { get; set; } = null!;

        [Column(TypeName = "varchar(50)")]
        public string CountryOfOrigin { get; set; } = null!;

        [Column(TypeName = "varchar(50)")]
        public string Nationality { get; set; } = null!;
        public Department Department { get; set; } = null!;
        public int DepartmentId { get; set; }
        public School School { get; set; } = null!;
        public int SchoolId { get; set; }
        public ICollection<Billing>? Billings { get; set; }
    }
}
