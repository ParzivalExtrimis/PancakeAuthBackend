using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace PancakeAuthBackend.Models {
    public class Subscription {
        public int Id { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Type { get; set; } = null!;

        [Column(TypeName = "varchar(100)")]
        public string Name { get; set; } = null!;

        [Column(TypeName = "varchar(200)")]
        public string Description { get; set; } = null!;
        public ICollection<AvailedSubscription> AvailedSchools { get; set; } = null!;
        public ICollection<School> Schools { get; set; } = null!; 
        public ICollection<ChaptersIncluded> IncludedChapters { get; set; } = null!;
        public ICollection<Chapter> Chapters { get; set; } = null!;
    }
}
