using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace IdentityServer.Identity
{
    public class AdditionalUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser>
    {
        public AdditionalUserClaimsPrincipalFactory(
            UserManager<ApplicationUser> userManager, 
            IOptions<IdentityOptions> optionsAccessor
        ) : base(userManager, optionsAccessor)
        {
        }
        
        protected async override Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            var roles = await UserManager.GetRolesAsync(user);
            
            identity.AddClaims(roles.Select(role => new Claim(JwtClaimTypes.Role, role)));
            
            return identity;
        }
    }
}