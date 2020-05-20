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

        #region Repositories
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
        #endregion

        private readonly string _path;

        #region Constructor
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
        #endregion
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
                            new Resource { Key = "KeyWord", Value = "Key words" },
                            new Resource { Key = "AllRegions", Value =  "All regions" },
                            new Resource { Key = "WeOffer", Value = "We offer&nbsp;<a href='job-listing.html'>2,989 job vacancies</a> right now!" },
                            new Resource { Key = "Password", Value = "Password"},
                            new Resource { Key = "RememberMe", Value = "Remember Me?"},
                            new Resource { Key = "Welcome", Value = "Welcome to <span class='text-primary'>Jobs</span>Factory"},
                            new Resource { Key = "Place", Value = "A place where leading employers are already looking for your talent and experience."},
                            new Resource { Key = "MoreThan", Value = "More than 3.8 million visitors every day"},
                            new Resource { Key = "Leading", Value = "Leading recruiting website in the US, Europe and Asia"},
                            new Resource { Key = "24/7", Value = "24/7 Dedicated and free Support"},
                            new Resource { Key = "Only", Value = "Only relevant and verified vacancies"},
                            new Resource { Key = "RecentJobs", Value = "Recent Jobs"},
                            new Resource { Key = "Pricing", Value = "Pricing"},
                            new Resource { Key = "Startup", Value = "Startup</p><span class='badge'>7 Days Free</span>" },
                            new Resource { Key = "Register", Value = "Register"},
                            new Resource { Key = "EnterEmail", Value = "Enter email"},
                            new Resource { Key = "EnterPhone", Value = "Enter phonenumber"},
                            new Resource { Key = "EnterPassword", Value = "Enter password"},
                            new Resource { Key = "ConfirmPassword", Value = "Confirm password"},
                            new Resource { Key = "Employer", Value = "Employer"},
                            new Resource { Key = "JobSeeker", Value = "JobSeeker"},
                            new Resource { Key = "Back", Value = "Back"},
                            new Resource { Key = "Already", Value = "Already have an account? Login"},
                            new Resource { Key = "ForgotPassword", Value = " Forgot Password?"},
                            new Resource { Key = "GeneralInformation", Value = "General Information"},
                            new Resource { Key = "CompanyName", Value = "Company name*"},
                            new Resource { Key = "Description", Value = "Description"},
                            new Resource { Key = "StaffCount", Value = "Staffs count"},
                            new Resource { Key = "UploadCompanyLogo", Value = "Upload company logo:"},
                            new Resource { Key = "UploadImage", Value = "Upload image"},
                            new Resource { Key = "SelectCountry", Value = "Select country"},
                            new Resource { Key = "SelectRegion", Value = "Select region"},
                            new Resource { Key = "SelectDistrict", Value = "Select district"},
                            new Resource { Key = "ContactDetails", Value = "Contact Details"},
                            new Resource { Key = "Address", Value = "Address"},
                            new Resource { Key = "CompanyWebsite", Value = "Company website"},
                            new Resource { Key = "Phone", Value = "Phone number"},
                            
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
                            new Resource { Key = "KeyWord", Value = "Ключевые слова" },
                            new Resource { Key = "AllRegions", Value =  "Все регионы" },
                            new Resource { Key = "WeOffer", Value = "Мы предлагаем вам&nbsp;<a href='job-listing.html'>2,989 вакансий</a> прямо сейчас!" },
                            new Resource { Key = "Password", Value = "Пароль"},
                            new Resource { Key = "RememberMe", Value = "Запомнить?"},
                            new Resource { Key = "Welcome", Value = "Добро пожаловать в <span class='text-primary'>Jobs</span>Factory"},
                            new Resource { Key = "Place", Value = "Именно здесь самые топовые компании ищут самых талантливых и опытных специалистов."},
                            new Resource { Key = "MoreThan", Value = "Более чем 3.8 миллион посетитилей в день"},
                            new Resource { Key = "Leading", Value = "Лидерующее рекрутинговое агентство в США, Европе и Азии"},
                            new Resource { Key = "24/7", Value = "24/7 круглосуточная и бесплатная поддержка"},
                            new Resource { Key = "Only", Value = "Только проверенные и надёжные вакансии"},
                            new Resource { Key = "RecentJobs", Value = "Последние вакансии"},
                            new Resource { Key = "Pricing", Value = "Тарифы"},
                            new Resource { Key = "Startup", Value = "Стартап</p><span class='badge'>7 дней бесплатно</span>" },
                            new Resource { Key = "Register", Value = "Регистрация"},
                            new Resource { Key = "EnterEmail", Value = "Введите эмейл"},
                            new Resource { Key = "EnterPhone", Value = "Введите номер телефона"},
                            new Resource { Key = "EnterPassword", Value = "Введите пароль"},
                            new Resource { Key = "ConfirmPassword", Value = "Подтвердите пароль"},
                            new Resource { Key = "Employer", Value = "Работодатель"},
                            new Resource { Key = "JobSeeker", Value = "Соискатель"},
                            new Resource { Key = "Back", Value = "Назад"},
                            new Resource { Key = "Already", Value = "Уже есть аккаунт? Вход"},
                            new Resource { Key = "ForgotPassword", Value = "Забыли пароль?"},
                            new Resource { Key = "GeneralInformation", Value = "Основные данные"},
                            new Resource { Key = "CompanyName", Value = "Название компании*"},
                            new Resource { Key = "Description", Value = "Описание"},
                            new Resource { Key = "StaffCount", Value = "Количество сотрудников"},
                            new Resource { Key = "UploadCompanyLogo", Value = "Загрузите логотип компании:"},
                            new Resource { Key = "UploadImage", Value = "Загрузите картинку"},
                            new Resource { Key = "SelectCountry", Value = "Выберите страну"},
                            new Resource { Key = "SelectRegion", Value = "Выберите регион"},
                            new Resource { Key = "SelectDistrict", Value = "Выберите район"},
                            new Resource { Key = "ContactDetails", Value = "Контактные данные"},
                            new Resource { Key = "Address", Value = "Адрес"},
                            new Resource { Key = "CompanyWebsite", Value = "Вебсайт компании"},
                            new Resource { Key = "Phone", Value = "Номер телефона"}
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
                            new Resource { Key = "KeyWord", Value = "Kalit so'z" },
                            new Resource { Key = "AllRegions", Value =  "Barcha viloyatlar" },
                            new Resource { Key = "WeOffer", Value = "Biz sizga&nbsp;<a href='job-listing.html'>2,989 ish o'rinlarini</a> taklif qilamiz!" },
                            new Resource { Key = "Password", Value = "Parol"},
                            new Resource { Key = "RememberMe", Value = "Eslab qolish?"},
                            new Resource { Key = "Welcome", Value = "<span class='text-primary'>Jobs</span>Factoryga xush kelibsiz"},
                            new Resource { Key = "Place", Value = "Aynan shu yerda dunyoning top kompaniyalari tajribali va qobiliyatli mutaxassislarni izlashadi."},
                            new Resource { Key = "MoreThan", Value = "Kuniya 3.8 million ko'p tashrif buyuruvchilar"},
                            new Resource { Key = "Leading", Value = "AQSh, Evropa va Osiyada birinchi kadrlar agentligi"},
                            new Resource { Key = "24/7", Value = "24/7 kun-tun va tekin aloqa"},
                            new Resource { Key = "Only", Value = "Faqat tekshirilgan va ishonchli ish o'rinlari"},
                            new Resource { Key = "RecentJobs", Value = "So'nggi ish o'rinlari"},
                            new Resource { Key = "Pricing", Value = "Tariflar"},
                            new Resource { Key = "Startup", Value = "Startup</p><span class='badge'>7 kun bepul</span>" },
                            new Resource { Key = "Register", Value = "Registratsiya"},
                            new Resource { Key = "EnterEmail", Value = "E-pochtani kiriting"},
                            new Resource { Key = "EnterPhone", Value = "Telefon raqamingizni kiriitng"},
                            new Resource { Key = "EnterPassword", Value = "Parolni kiriting"},
                            new Resource { Key = "ConfirmPassword", Value = "Parolni qayta kiriitng"},
                            new Resource { Key = "Employer", Value = "Ish beruvchi"},
                            new Resource { Key = "JobSeeker", Value = "Ish izlovchi"},
                            new Resource { Key = "Back", Value = "Ortga"},
                            new Resource { Key = "Already", Value = "Akkaunt mavjudmi? Kirish"},
                            new Resource { Key = "ForgotPassword", Value = " Parolni unutdingizmi?"},
                            new Resource { Key = "GeneralInformation", Value = "Umumiy ma'lumotlar"},
                            new Resource { Key = "CompanyName", Value = "Kompaniya nomi*"},
                            new Resource { Key = "Description", Value = "Qisqacha tavsiv"},
                            new Resource { Key = "StaffCount", Value = "Xodimlar soni"},
                            new Resource { Key = "UploadCompanyLogo", Value = "Kompaniya logotipini yuklang:"},
                            new Resource { Key = "UploadImage", Value = "Rasmni yuklang"},
                            new Resource { Key = "SelectCountry", Value = "Davlatni tanlang"},
                            new Resource { Key = "SelectRegion", Value = "Viloyatni tanlang"},
                            new Resource { Key = "SelectDistrict", Value = "Tumanni tanlang"},
                            new Resource { Key = "ContactDetails", Value = "Kontakt ma'lumotlari"},
                            new Resource { Key = "Address", Value = "Manzil"},
                            new Resource { Key = "CompanyWebsite", Value = "Kompaniya vebsayti"},
                            new Resource { Key = "Phone", Value = "Telefon raqami"}

                        }
                    }
                };
                _cultureRepo.AddRange(cultures);
            }
            #endregion
        }

        #region GetList helper
        private List<T> GetList<T>(string path) where T : class
        {
            string str = File.ReadAllText(path, Encoding.UTF8);
            JObject o = JObject.Parse(str);
            JArray a = (JArray)o["JsonData"];
            List<T> list = a.ToObject<List<T>>();
            return list;
        }
        #endregion
    }
}
