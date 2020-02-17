using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RecruitmentAgencyCore.Data;
using RecruitmentAgencyCore.Data.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RecruitmentAgencyCore.Security
{
    public class RecruitmentAgencySignInManager : SignInManager<User>
    {
        private readonly RecruitmentAgencyUserManager _userManager;
        private readonly AppDbContext _db;
        private readonly IHttpContextAccessor _contextAccessor;

        public RecruitmentAgencySignInManager(
           RecruitmentAgencyUserManager userManager,
           IHttpContextAccessor contextAccessor,
           IUserClaimsPrincipalFactory<User> claimsFactory,
           IOptions<IdentityOptions> optionsAccessor,
           ILogger<SignInManager<User>> logger,
           AppDbContext dbContext,
           IAuthenticationSchemeProvider schemeProvider,
           IUserConfirmation<User> userConfirmation
           )
           : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemeProvider, userConfirmation)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _contextAccessor = contextAccessor ?? throw new ArgumentNullException(nameof(contextAccessor));
            _db = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public override async Task<SignInResult> PasswordSignInAsync(string userName, string password, bool rememberMe, bool shouldLockout)
        {
            var user = _userManager.FindByEmailAsync(userName).Result;
            user.LastLoginTime = DateTime.Now;
            
            return await base.PasswordSignInAsync(userName, password, rememberMe, shouldLockout);
        }
        public override Task SignInWithClaimsAsync(User user, AuthenticationProperties authenticationProperties, IEnumerable<Claim> additionalClaims)
        {
            return base.SignInWithClaimsAsync(user, authenticationProperties, additionalClaims);
        }
    }
}
