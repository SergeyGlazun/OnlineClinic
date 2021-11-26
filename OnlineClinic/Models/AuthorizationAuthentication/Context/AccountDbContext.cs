using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineClinic.Models.AuthorizationAuthentication.Model;


namespace OnlineClinic.Models.AuthorizationAuthentication.Context
{
    public class AccountDbContext : IdentityDbContext<User>
    {
        public AccountDbContext(DbContextOptions<AccountDbContext> options)
         : base(options)
        {
            //Database.EnsureCreated();
        }
    }
}
