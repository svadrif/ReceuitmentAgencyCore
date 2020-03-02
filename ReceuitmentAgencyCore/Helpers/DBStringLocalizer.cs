using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using RecruitmentAgencyCore.Data.Models;
using RecruitmentAgencyCore.Data.Repository;

namespace RecruitmentAgencyCore.Helpers
{
    public class DBStringLocalizer : IStringLocalizer
    {
        private readonly IGenericRepository<Resource> _resourceRepo;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DBStringLocalizer(IGenericRepository<Resource> resourceRepo, IHttpContextAccessor httpContextAccessor)
        {
            _resourceRepo = resourceRepo;
            _httpContextAccessor = httpContextAccessor;
        }

        public LocalizedString this[string name]
        {
            get
            {
                string value = GetString(name);
                return new LocalizedString(name, value ?? name, resourceNotFound: value == null);
            }
        }

        public LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                string format = GetString(name);
                string value = string.Format(format ?? name, arguments);
                return new LocalizedString(name, value, resourceNotFound: format == null);
            }
        }

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            return _resourceRepo
                .GetAllIncluding(r => r.Culture)
                .Where(r => r.Culture.Name == (_httpContextAccessor.HttpContext.Session.GetString("culture") ?? "uz"))
                .Select(r => new LocalizedString(r.Key, r.Value));
        }

        public IStringLocalizer WithCulture(CultureInfo culture)
        {
            CultureInfo.DefaultThreadCurrentCulture = culture;
            return new DBStringLocalizer(_resourceRepo, _httpContextAccessor);
        }

        private string GetString(string name)
        {
            return _resourceRepo
                .GetAllIncluding(r => r.Culture)
                .Where(r => r.Culture.Name == (_httpContextAccessor.HttpContext.Session.GetString("culture") ?? "uz"))
                .FirstOrDefault(r => r.Key == name)?.Value;
        }
    }
}
