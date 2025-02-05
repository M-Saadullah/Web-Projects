using Microsoft.AspNetCore.Identity;

namespace The_Outfit.Models
{
    public class MyAppUser:IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string country {  get; set; }
    }
}
