using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Linq;
using RecruitmentAgencyCore.Data;
using RecruitmentAgencyCore.Data.Models;
using RecruitmentAgencyCore.Data.Repository;
using RecruitmentAgencyCore.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RecruitmentAgencyCore.InitialData
{
    public class Seeder
    {
        private readonly AppDbContext _ctx;
        private readonly RecruitmentAgencyUserManager _userManager;
        private readonly RecruitmentAgencyUserStore _userStore;

        private readonly IGenericRepository<User> _userRepository;
        private readonly IGenericRepository<Role> _roleRepository;
        private readonly IGenericRepository<Permission> _permissionRepository;
        private readonly IGenericRepository<Gender> _genderRepository;
        private readonly IGenericRepository<Country> _countryRepository;
        private readonly IGenericRepository<Culture> _cultureRepo;
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
        private readonly IGenericRepository<MenuRolePermission> _menuRolePermissionRepository;

        private readonly string _path;

        public Seeder(AppDbContext ctx, RecruitmentAgencyUserStore userStore, IHostEnvironment hosting, RecruitmentAgencyUserManager userManager,
                     IGenericRepository<User> userRepository, IGenericRepository<Role> roleRepository, IGenericRepository<Permission> permissionRepository,
                     IGenericRepository<Gender> genderRepository, IGenericRepository<Country> countryRepository, IGenericRepository<Culture> cultureRepo,
                     IGenericRepository<Menu> menuRepository, IGenericRepository<Experience> experienceRepository,
                     IGenericRepository<FamilyStatus> familyStatusRepository, IGenericRepository<Branch> branchRepository,
                     IGenericRepository<Citizenship> citizenshipRepository, IGenericRepository<Currency> currencyRepository,
                     IGenericRepository<EducationType> educationTypeRepository, IGenericRepository<ForeignLanguage> foreignLanguageRepository,
                     IGenericRepository<LanguageLevel> languageLevelRepository, IGenericRepository<Region> regionRepository,
                     IGenericRepository<Schedule> scheduleRepository, IGenericRepository<SocialStatus> socialStatusRepository,
                     IGenericRepository<TypeOfEmployment> typeOfEmploymentRepository, IGenericRepository<University> universityRepository,
                     IGenericRepository<District> districtRepository, IGenericRepository<MenuRolePermission> menuRolePermissionRepository)
        {
            _ctx = ctx;
            _path = hosting.ContentRootPath;
            _userManager = userManager;
            _userStore = userStore;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _permissionRepository = permissionRepository;
            _genderRepository = genderRepository;
            _countryRepository = countryRepository;
            _cultureRepo = cultureRepo;
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
            _menuRolePermissionRepository = menuRolePermissionRepository;
        }

        public void Seed()
        {
            _ctx.Database.EnsureCreated();

            #region Roles
            if (!_ctx.Roles.Any())
            {
                Role[] roles = new Role[3]
                {
                    new Role{ Name = "Admin", CreatedBy = 1, CreatedDate = DateTime.Now },
                    new Role{ Name = "Employer", CreatedBy = 1, CreatedDate = DateTime.Now },
                    new Role{ Name = "JobSeeker", CreatedBy = 1, CreatedDate = DateTime.Now }
                };
                roles.Reverse();
                _roleRepository.AddRange(roles);
            }
            #endregion

            #region Users
            if (!_ctx.Users.Any())
            {
                User user = new User
                {
                    UserName = "developer6098@gmail.com",
                    NormalizedUserName = "developer6098@gmail.com",
                    Email = "developer6098@gmail.com",
                    NormalizedEmail = "developer6098@gmail.com",
                    PhoneNumber = "(93) 398-60-98",
                    PhoneNumberConfirmed = true,
                    OpenPassword = "6098DeveloperC#",
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    IsActive = true,
                    RoleId = _roleRepository.Find(x => x.Name.ToLower() == "admin").Id,
                    CreatedDate = DateTime.Now.Date
                };

                PasswordHasher<User> password = new PasswordHasher<User>();
                string hashed = password.HashPassword(user, "6098DeveloperC#");
                user.PasswordHash = hashed;

                _userRepository.Add(user);
            }
            #endregion

            #region Permissions
            if (!_ctx.Permissions.Any())
            {
                Permission[] permissions = new Permission[4]
                {
                    new Permission{ Name = "Read", CreatedBy = 1, CreatedDate = DateTime.Now },
                    new Permission{ Name = "Write", CreatedBy = 1, CreatedDate = DateTime.Now },
                    new Permission{ Name = "ReadWrite", CreatedBy = 1, CreatedDate = DateTime.Now },
                    new Permission{ Name = "NoAccess", CreatedBy = 1, CreatedDate = DateTime.Now }
                };
                foreach (var item in permissions)
                {
                    _permissionRepository.Add(item);
                }
                //permissions.Reverse();
                //_permissionRepository.AddRange(permissions);

            }
            #endregion

            #region Menus
            if (!_ctx.Menus.Any())
            {
                List<Menu> menus = GetList<Menu>(_path + "/wwwroot/jsonData/menu.json");
                menus.ForEach(x => x.CreatedDate = DateTime.Now);
                foreach (var item in menus)
                {
                    _menuRepository.Add(item);
                }
                //menus.Reverse();
                //_menuRepository.AddRange(menus);
            }
            #endregion

            #region MenuRolePermissions
            if (!_ctx.MenuRolePermissions.Any())
            {
                List<MenuRolePermission> menuRolePermissions = GetList<MenuRolePermission>(_path + "/wwwroot/jsonData/menuRolePermission.json");
                menuRolePermissions.ForEach(x => x.CreatedDate = DateTime.Now);
                foreach (var item in menuRolePermissions)
                {
                    _menuRolePermissionRepository.Add(item);
                }
                //menuRolePermissions.Reverse();
                //_menuRolePermissionRepository.AddRange(menuRolePermissions);
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
                foreach (var item in genders)
                {
                    _genderRepository.Add(item);
                }
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
                experiences.ForEach(x => x.CreatedDate = DateTime.Now);
                experiences.Reverse();
                _experienceRepository.AddRange(experiences);

            }
            #endregion

            #region FamilyStatuses
            if (!_ctx.FamilyStatuses.Any())
            {
                List<FamilyStatus> familyStatuses = GetList<FamilyStatus>(_path + "/wwwroot/jsonData/familyStatus.json");
                familyStatuses.ForEach(x => x.CreatedDate = DateTime.Now);
                familyStatuses.Reverse();
                _familyStatusRepository.AddRange(familyStatuses);
            }
            #endregion

            #region Branches
            if (!_ctx.Branches.Any())
            {
                List<Branch> branches = GetList<Branch>(_path + "/wwwroot/jsonData/branch.json");
                branches.ForEach(x => x.CreatedDate = DateTime.Now);
                foreach (var item in branches)
                {
                    _branchRepository.Add(item);
                }
                //branches.Reverse();
                //_branchRepository.AddRange(branches);
            }
            #endregion

            #region Citizenships
            if (!_ctx.Citizenships.Any())
            {
                List<Citizenship> citizenships = GetList<Citizenship>(_path + "/wwwroot/jsonData/citizenship.json");
                citizenships.ForEach(x => x.CreatedDate = DateTime.Now);
                citizenships.Reverse();
                _citizenshipRepository.AddRange(citizenships);
            }
            #endregion

            #region Currencies
            if (!_ctx.Currencies.Any())
            {
                List<Currency> currencies = GetList<Currency>(_path + "/wwwroot/jsonData/currency.json");
                currencies.Reverse();
                _currencyRepository.AddRange(currencies);
            }
            #endregion

            #region EducationTypes
            if (!_ctx.EducationTypes.Any())
            {
                List<EducationType> educationTypes = GetList<EducationType>(_path + "/wwwroot/jsonData/educationType.json");
                educationTypes.ForEach(x => x.CreatedDate = DateTime.Now);
                educationTypes.Reverse();
                _educationTypeRepository.AddRange(educationTypes);
            }
            #endregion

            #region ForeignLanguages
            if (!_ctx.ForeignLanguages.Any())
            {
                List<ForeignLanguage> foreignLanguages = GetList<ForeignLanguage>(_path + "/wwwroot/jsonData/foreignLanguage.json");
                foreignLanguages.ForEach(x => x.CreatedDate = DateTime.Now);
                foreignLanguages.Reverse();
                _foreignLanguageRepository.AddRange(foreignLanguages);
            }
            #endregion

            #region LanguageLevels
            if (!_ctx.LanguageLevels.Any())
            {
                List<LanguageLevel> languageLevels = GetList<LanguageLevel>(_path + "/wwwroot/jsonData/languageLevel.json");
                languageLevels.ForEach(x => x.CreatedDate = DateTime.Now);
                languageLevels.Reverse();
                _languageLevelRepository.AddRange(languageLevels);
            }
            #endregion

            #region Regions
            if (!_ctx.Regions.Any())
            {
                List<Region> regions = GetList<Region>(_path + "/wwwroot/jsonData/region.json");
                regions.ForEach(x => x.CreatedDate = DateTime.Now);
                regions.Reverse();
                _regionRepository.AddRange(regions);
            }
            #endregion

            #region Schedules
            if (!_ctx.Schedules.Any())
            {
                List<Schedule> schedules = GetList<Schedule>(_path + "/wwwroot/jsonData/schedule.json");
                schedules.ForEach(x => x.CreatedDate = DateTime.Now);
                schedules.Reverse();
                _scheduleRepository.AddRange(schedules);
            }
            #endregion

            #region SocialStatuses
            if (!_ctx.SocialStatuses.Any())
            {
                List<SocialStatus> socialStatuses = GetList<SocialStatus>(_path + "/wwwroot/jsonData/socialStatus.json");
                socialStatuses.ForEach(x => x.CreatedDate = DateTime.Now);
                socialStatuses.Reverse();
                _socialStatusRepository.AddRange(socialStatuses);
            }
            #endregion

            #region TypeOfEmployments
            if (!_ctx.TypeOfEmployments.Any())
            {
                List<TypeOfEmployment> typeOfEmployments = GetList<TypeOfEmployment>(_path + "/wwwroot/jsonData/typeOfEmployment.json");
                typeOfEmployments.ForEach(x => x.CreatedDate = DateTime.Now);
                typeOfEmployments.Reverse();
                _typeOfEmploymentRepository.AddRange(typeOfEmployments);
            }
            #endregion

            #region Universities
            if (!_ctx.Universities.Any())
            {
                List<University> universities = GetList<University>(_path + "/wwwroot/jsonData/university.json");
                universities.ForEach(x => x.CreatedDate = DateTime.Now);
                universities.Reverse();
                _universityRepository.AddRange(universities);
            }
            #endregion

            #region Districts
            if (!_ctx.Districts.Any())
            {
                List<District> districts = GetList<District>(_path + "/wwwroot/jsonData/district.json");
                districts.ForEach(x => x.CreatedDate = DateTime.Now);
                districts.Reverse();
                _districtRepository.AddRange(districts);
            }
            #endregion

            #region Cultures
            if (!_ctx.Cultures.Any())
            {
                Culture[] cultures = new Culture[]
                {
                     new Culture
                    {
                        Name = "en",
                        Resources = new List<Resource>()
                        {
                            new Resource { Key = "ForJobseekers", Value = "For Jobseekers" },
                            new Resource { Key = "ForEmployers", Value = "For Employers" },
                            new Resource { Key = "Services", Value =  "Services" },
                            new Resource { Key = "Blog", Value = "Blog" },
                            new Resource { Key = "SignUp", Value = "Sign up" },
                            new Resource { Key = "Login", Value =  "Login" },
                            new Resource { Key = "StartNow", Value = "Start building your own career now!" },
                            new Resource { Key = "KetWord", Value = "Key words" },
                            new Resource { Key = "AllRegions", Value =  "All regions" },
                            new Resource { Key = "WeOffer", Value = "We offer&nbsp;<a href='job-listing.html'>2,989 job vacancies</a> right now!" }
                        }
                    },
                    new Culture
                    {
                        Name = "ru",
                        Resources = new List<Resource>()
                        {
                            new Resource { Key = "ForJobseekers", Value = "Для соискателей" },
                            new Resource { Key = "ForEmployers", Value = "Для работодателей" },
                            new Resource { Key = "Services", Value =  "Сервисы" },
                            new Resource { Key = "Blog", Value = "Блог" },
                            new Resource { Key = "SignUp", Value = "Регистрация" },
                            new Resource { Key = "Login", Value =  "Вход" },
                            new Resource { Key = "StartNow", Value = "Начните строить свою карьеру сейчас!" },
                            new Resource { Key = "KetWord", Value = "Ключевые слова" },
                            new Resource { Key = "AllRegions", Value =  "Все регионы" },
                            new Resource { Key = "WeOffer", Value = "Мы предлагаем вам&nbsp;<a href='job-listing.html'>2,989 вакансий</a> прямо сейчас!" }
                        }
                    },
                    new Culture
                    {
                        Name = "uz",
                        Resources = new List<Resource>()
                        {
                            new Resource { Key = "ForJobseekers", Value = "Ish izlovchilarga" },
                            new Resource { Key = "ForEmployers", Value = "Ish beruvchilarga" },
                            new Resource { Key = "Services", Value =  "Xizmatlar" },
                            new Resource { Key = "Blog", Value = "Blog" },
                            new Resource { Key = "SignUp", Value = "Registratsiya" },
                            new Resource { Key = "Login", Value =  "Kirish" },
                            new Resource { Key = "StartNow", Value = "Shaxsiy karyerangizni qurishni hoziroq boshlang!" },
                            new Resource { Key = "KetWord", Value = "Kalit so'z" },
                            new Resource { Key = "AllRegions", Value =  "Barcha viloyatlar" },
                            new Resource { Key = "WeOffer", Value = "Biz sizga&nbsp;<a href='job-listing.html'>2,989 ish o'rinlarini</a> taklif qilamiz!" }
                        }
                    }
                };
                _cultureRepo.AddRange(cultures);
            }
            #endregion
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
