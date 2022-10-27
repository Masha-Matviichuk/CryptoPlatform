using Microsoft.AspNetCore.Identity;

namespace DAL.Entities
{
        public class User : IdentityUser
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public bool IsDeleted { get; set; }
        }
}
