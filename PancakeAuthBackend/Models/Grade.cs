using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace PancakeAuthBackend.Models {
    public class Grade {
        public int Id { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Name { get; set; } = null!;
        [ForeignKey("ClassManagerGradeId")]
        public List<User>? ClassManager { get; set; }
        public ICollection<Student>? Students { get; set; }
    }
}
