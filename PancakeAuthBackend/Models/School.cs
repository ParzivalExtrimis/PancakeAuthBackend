using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace PancakeAuthBackend.Models {
    public class School {
        public int Id { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Name { get; set; } = null!;
        public User SchoolManager { get; set; } = null!;
        [ForeignKey("CoSMSchoolId")]
        public List<User>? CoSchoolManagers { get; set; } 
        [ForeignKey("ClassManagerSchoolId")]
        public List<User>? DepartmentManagers { get; set; } 
        public ICollection<Student>? Students { get; set; }
        public Address Address { get; set; } = null!;
        public int AddressId { get; set; }
    }
}
