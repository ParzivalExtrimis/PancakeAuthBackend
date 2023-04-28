using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace PancakeAuthBackend.Models {
    public class Billing {
        public int Id { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Status { get; set; } = null!;
        public DateTime DueDate { get; set; }
        public int Amount { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string? Details { get; set; }
        public School School { get; set; } = null!;
        public int SchoolId { get; set; }
    }
}
