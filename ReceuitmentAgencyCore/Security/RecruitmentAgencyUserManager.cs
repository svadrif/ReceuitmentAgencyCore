using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RecruitmentAgencyCore.Data.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitmentAgencyCore.Security
{
    public class RecruitmentAgencyUserManager : UserManager<User>
    {
        readonly RecruitmentAgencyUserStore _userStore;
        public RecruitmentAgencyUserManager(RecruitmentAgencyUserStore userStore,
             IOptions<IdentityOptions> optionsAccessor,
             IPasswordHasher<User> passwordHasher,
             IEnumerable<IUserValidator<User>> userValidators,
             IEnumerable<IPasswordValidator<User>> passwordValidators,
             ILookupNormalizer normalizer,
             IdentityErrorDescriber errorDescriber,
             IServiceProvider service,
             ILogger<UserManager<User>> logger
            ) : base(userStore, optionsAccessor, passwordHasher, userValidators, passwordValidators, normalizer, errorDescriber, service, logger)
        {
            _userStore = userStore;
        }
        public override Task<User> FindByEmailAsync(string email)
        {
            return _userStore.FindByEmailAsync(email);
        }
        public override Task<IdentityResult> CreateAsync(User user)
        {
            return _userStore.CreateAsync(user, new CancellationToken());
        }
    }
}
