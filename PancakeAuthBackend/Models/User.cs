using Microsoft.AspNetCore.Identity;

namespace PancakeAuthBackend.Models {
    public class User : IdentityUser {

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? ProfilePicture { get; set; }

    }
}
