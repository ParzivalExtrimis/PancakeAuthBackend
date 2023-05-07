using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace PancakeAuthBackend.Models {
    public class Chapter {
        public int Id { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Title { get; set; } = null!;

        [Column(TypeName = "varchar(200)")]
        public string Description { get; set; } = null!;
        public Subject Subject { get; set; } = null!;
        public int SubjectId { get; set; }
    }
}
