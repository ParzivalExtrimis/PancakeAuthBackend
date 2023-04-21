using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace PancakeAuthBackend.Models {
    public class Batch {
        public int Id { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Name { get; set; } = null!;
        public ICollection<Student> Students { get; set; } = null!;
    }
}
