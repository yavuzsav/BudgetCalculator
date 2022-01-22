using Microsoft.AspNetCore.Identity;

namespace BudgetCalculator.Entities.Concrete
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}