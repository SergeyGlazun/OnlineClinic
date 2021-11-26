using Microsoft.AspNetCore.Identity;


namespace OnlineClinic.Models.AuthorizationAuthentication.Model
{
    public class User: IdentityUser
    {
        public string Lastname { get; set; }
        public string AddressCity { get; set; }
    }
}
