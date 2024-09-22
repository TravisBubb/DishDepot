using BSS.DishDepot.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BSS.DishDepot.WebApp.Data
{
    public class DishDepotIdentityDbContext(DbContextOptions<DishDepotIdentityDbContext> options) 
        : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>(options)
    {
    }
}
