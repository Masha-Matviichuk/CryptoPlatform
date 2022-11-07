using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace DAL.Entities
{
        public class User : IdentityUser
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public IEnumerable<string> UserLikes { get; set; }
            public IEnumerable<string> UserDislikes { get; set; }
            public bool IsDeleted { get; set; }

            public User()
            {
                UserLikes = new List<string>();
                UserDislikes = new List<string>();
            }
            
        }
}
