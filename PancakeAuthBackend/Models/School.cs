using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace PancakeAuthBackend.Models {
    public class School {
        public int Id { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Name { get; set; } = null!;
        public ICollection<Student>? Students { get; set; }
        public ICollection<Subscription>? Subscriptions { get; set; }
        public ICollection<AvailedSubscription>? AvailedSubscriptions { get; set; }
        public ICollection<Batch>? Batches { get; set; }
        public Address Address { get; set; } = null!;
        public ICollection<Billing>? Payments { get; set; }
        public int AddressId { get; set; }
    }
}
