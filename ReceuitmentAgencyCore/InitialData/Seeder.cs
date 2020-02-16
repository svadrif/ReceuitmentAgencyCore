using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ReceuitmentAgencyCore.Security;
using RecruitmentAgencyCore.Data;
using RecruitmentAgencyCore.Data.Models;
using RecruitmentAgencyCore.Data.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceuitmentAgencyCore.InitialData
{
    public class Seeder
    {
        private readonly AppDbContext _ctx;
        private readonly RecruitmentAgencyUserManager _userManager;

        private readonly IGenericRepository<Role> _roleRepository;
        private readonly IGenericRepository<Permission> _permissionRepository;
        private readonly IGenericRepository<Gender> _genderRepository;
        private readonly IGenericRepository<Country> _countryRepository;
        private readonly IGenericRepository<Experience> _experienceRepository;
        private readonly IGenericRepository<FamilyStatus> _familyStatusRepository;
        private readonly IGenericRepository<Branch> _branchRepository;
        private readonly IGenericRepository<Citizenship> _citizenshipRepository;
        private readonly IGenericRepository<Currency> _currencyRepository;
        private readonly IGenericRepository<EducationType> _educationTypeRepository;
        private readonly IGenericRepository<ForeignLanguage> _foreignLanguageRepository;
        private readonly IGenericRepository<LanguageLevel> _languageLevelRepository;
        private readonly IGenericRepository<Region> _regionRepository;
        private readonly IGenericRepository<Schedule> _scheduleRepository;
        private readonly IGenericRepository<SocialStatus> _socialStatusRepository;
        private readonly IGenericRepository<TypeOfEmployment> _typeOfEmploymentRepository;
        private readonly IGenericRepository<University> _universityRepository;
        private readonly IGenericRepository<District> _districtRepository;
        private readonly IGenericRepository<Menu> _menuRepository;
       
        private readonly string _path;

        public Seeder(AppDbContext ctx, IHostEnvironment hosting, RecruitmentAgencyUserManager userManager,
                     IGenericRepository<Role> roleRepository, IGenericRepository<Permission> permissionRepository,
                     IGenericRepository<Gender> genderRepository, IGenericRepository<Country> countryRepository,
                     IGenericRepository<Menu> menuRepository, IGenericRepository<Experience> experienceRepository,
                     IGenericRepository<FamilyStatus> familyStatusRepository, IGenericRepository<Branch> branchRepository,
                     IGenericRepository<Citizenship> citizenshipRepository, IGenericRepository<Currency> currencyRepository,
                     IGenericRepository<EducationType> educationTypeRepository, IGenericRepository<ForeignLanguage> foreignLanguageRepository,
                     IGenericRepository<LanguageLevel> languageLevelRepository, IGenericRepository<Region> regionRepository,
                     IGenericRepository<Schedule> scheduleRepository, IGenericRepository<SocialStatus> socialStatusRepository,
                     IGenericRepository<TypeOfEmployment> typeOfEmploymentRepository, IGenericRepository<University> universityRepository,
                     IGenericRepository<District> districtRepository)
        {
            _ctx = ctx;
            _path = hosting.ContentRootPath;
            _userManager = userManager;
            _roleRepository = roleRepository;
            _permissionRepository = permissionRepository;
            _genderRepository = genderRepository;
            _countryRepository = countryRepository;
            _menuRepository = menuRepository;
            _experienceRepository = experienceRepository;
            _familyStatusRepository = familyStatusRepository;
            _branchRepository = branchRepository;
            _citizenshipRepository = citizenshipRepository;
            _currencyRepository = currencyRepository;
            _educationTypeRepository = educationTypeRepository;
            _foreignLanguageRepository = foreignLanguageRepository;
            _languageLevelRepository = languageLevelRepository;
            _regionRepository = regionRepository;
            _scheduleRepository = scheduleRepository;
            _socialStatusRepository = socialStatusRepository;
            _typeOfEmploymentRepository = typeOfEmploymentRepository;
            _universityRepository = universityRepository;
            _districtRepository = districtRepository;
        }

        public async Task Seed()
        {
            _ctx.Database.EnsureCreated();

            #region Users
            if (!_ctx.Users.Any())
            {
                var user = await _userManager.FindByEmailAsync("developer6098@gmail.com");
                if (user == null)
                {
                    user = new User()
                    {
                        UserName = "Firdavs",
                        Email = "developer6098@gmail.com",
                        PhoneNumber = "933986098"
                    };
                    var res = await _userManager.CreateAsync(user, "6098DeveloperC#");
                    if (res != IdentityResult.Success)
                    {
                        throw new InvalidOperationException("Failed to create default user");
                    }
                }
            }
            #endregion

            #region Roles
            if (!_ctx.Roles.Any())
            {
                Role[] roles = new Role[]
                {
                    new Role{ Name = "Admin", CreatedBy = 1, CreatedDate = DateTime.Now },
                    new Role{ Name = "Employer", CreatedBy = 1, CreatedDate = DateTime.Now },
                    new Role{ Name = "JobSeeker", CreatedBy = 1, CreatedDate = DateTime.Now }           
                };

                _roleRepository.AddRange(roles);
            }
            #endregion

            #region Permissions
            if (!_ctx.Permissions.Any())
            {
                Permission[] permissions = new Permission[]
                {
                    new Permission{ Name = "Read", CreatedBy = 1, CreatedDate = DateTime.Now },
                    new Permission{ Name = "Write", CreatedBy = 1, CreatedDate = DateTime.Now },
                    new Permission{ Name = "Read | Write", CreatedBy = 1, CreatedDate = DateTime.Now },
                    new Permission{ Name = "No access", CreatedBy = 1, CreatedDate = DateTime.Now }
                };

                _permissionRepository.AddRange(permissions);
            }
            #endregion

            #region Genders
            if (!_ctx.Genders.Any())
            {
                Gender[] genders = new Gender[]
                {       
                    new Gender{ NameUz = "Erkak", NameRu = "Мужской", NameEn = "Male" },
                    new Gender{ NameUz = "Ayol", NameRu = "Женский", NameEn = "Female"}                 
                };

                _genderRepository.AddRange(genders);
            }
            #endregion

            #region Countries
            if (!_ctx.Countries.Any())
            {
                Country[] countries = new Country[]
                {
                    new Country{ NameUz = "O'zbekiston", NameRu = "Узбекистан", NameEn = "Uzbekistan"}
                };

                _countryRepository.AddRange(countries);
            }
            #endregion

            #region Experiences
            if (!_ctx.Experiences.Any())
            {
                List<Experience> experiences = GetList<Experience>(_path + "/wwwroot/jsonData/experience.json");
                experiences.AsParallel().ForAll(x => x.CreatedDate = DateTime.Now);
                _experienceRepository.AddRange(experiences);
                
            }
            #endregion

            #region FamilyStatuses
            if (!_ctx.FamilyStatuses.Any())
            {
                List<FamilyStatus> familyStatuses = GetList<FamilyStatus>(_path + "/wwwroot/jsonData/familyStatus.json");
                familyStatuses.AsParallel().ForAll(x => x.CreatedDate = DateTime.Now);
                _familyStatusRepository.AddRange(familyStatuses);
            }
            #endregion

            #region Branches
            if (!_ctx.Branches.Any())
            {
                List<Branch> branches = GetList<Branch>(_path + "/wwwroot/jsonData/branch.json");
                branches.AsParallel().ForAll(x => x.CreatedDate = DateTime.Now);
                _branchRepository.AddRange(branches);
            }
            #endregion

            #region Citizenships
            if (!_ctx.Citizenships.Any())
            {
                IList<Citizenship> citizenships = GetList<Citizenship>(_path + "/wwwroot/jsonData/citizenship.json");
                citizenships.AsParallel().ForAll(x => x.CreatedDate = DateTime.Now);
                _citizenshipRepository.AddRange(citizenships);
            }
            #endregion

            #region Currencies
            if (!_ctx.Currencies.Any())
            {
                List<Currency> currencies = GetList<Currency>(_path + "/wwwroot/jsonData/currency.json");
                _currencyRepository.AddRange(currencies);
            }
            #endregion

            #region EducationTypes
            if (_ctx.EducationTypes.Any())
            {
                List<EducationType> educationTypes = GetList<EducationType>(_path + "/wwwroot/jsonData/educationType.json");
                educationTypes.AsParallel().ForAll(x => x.CreatedDate = DateTime.Now);
                _educationTypeRepository.AddRange(educationTypes);
            }
            #endregion

            #region ForeignLanguages
            if (_ctx.ForeignLanguages.Any())
            {
                List<ForeignLanguage> foreignLanguages = GetList<ForeignLanguage>(_path + "/wwwroot/jsonData/foreignLanguage.json");
                foreignLanguages.AsParallel().ForAll(x => x.CreatedDate = DateTime.Now);
                _foreignLanguageRepository.AddRange(foreignLanguages);
            }
            #endregion

            #region LanguageLevels
            if (!_ctx.LanguageLevels.Any())
            {
                List<LanguageLevel> languageLevels = GetList<LanguageLevel>(_path + "/wwwroot/jsonData/languageLevel.json");
                languageLevels.AsParallel().ForAll(x => x.CreatedDate = DateTime.Now);
                _languageLevelRepository.AddRange(languageLevels);
            }
            #endregion

            #region Regions
            if (!_ctx.Regions.Any())
            {
                List<Region> regions = GetList<Region>(_path + "/wwwroot/jsonData/region.json");
                regions.AsParallel().ForAll(x => x.CreatedDate = DateTime.Now);
                _regionRepository.AddRange(regions);
            }
            #endregion

            #region Schedules
            if (!_ctx.Schedules.Any())
            {
                List<Schedule> schedules = GetList<Schedule>(_path + "/wwwroot/jsonData/schedule.json");
                schedules.AsParallel().ForAll(x => x.CreatedDate = DateTime.Now);
                _scheduleRepository.AddRange(schedules);
            }
            #endregion

            #region SocialStatuses
            if (!_ctx.SocialStatuses.Any())
            {
                List<SocialStatus> socialStatuses = GetList<SocialStatus>(_path + "/wwwroot/jsonData/socialStatus.json");
                socialStatuses.AsParallel().ForAll(x => x.CreatedDate = DateTime.Now);
                _socialStatusRepository.AddRange(socialStatuses);
            }
            #endregion

            #region TypeOfEmployments
            if (!_ctx.TypeOfEmployments.Any())
            {
                List<TypeOfEmployment> typeOfEmployments = GetList<TypeOfEmployment>(_path + "/wwwroot/jsonData/typeOfEmployment.json");
                typeOfEmployments.AsParallel().ForAll(x => x.CreatedDate = DateTime.Now);
                _typeOfEmploymentRepository.AddRange(typeOfEmployments);
            }
            #endregion

            #region Universities
            if (!_ctx.Universities.Any())
            {
                List<University> universities = GetList<University>(_path + "/wwwroot/jsonData/university.json");
                universities.AsParallel().ForAll(x => x.CreatedDate = DateTime.Now);
                _universityRepository.AddRange(universities);
            }
            #endregion

            #region Districts
            if (!_ctx.Districts.Any())
            {
                List<District> districts = GetList<District>(_path + "/wwwroot/jsonData/district.json");
                districts.AsParallel().ForAll(x => x.CreatedDate = DateTime.Now);
                _districtRepository.AddRange(districts);
            }
            #endregion

            await _ctx.SaveChangesAsync();
        }

        private List<T> GetList<T>(string path) where T : class
        {
            string str = File.ReadAllText(path, Encoding.UTF8);
            JObject o = JObject.Parse(str);
            JArray a = (JArray)o["JsonData"];
            List<T> list = a.ToObject<List<T>>();
            return list;
        }
    }
}
