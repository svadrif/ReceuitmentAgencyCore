using Microsoft.EntityFrameworkCore;
using RecruitmentAgencyCore.Data.Mappings;
using RecruitmentAgencyCore.Data.Models;

namespace RecruitmentAgencyCore.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
           : base(options)
        {
        }

        public DbSet<Branch> Branches { get; set; }
        public DbSet<Citizenship> Citizenships { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Culture> Cultures { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<EducationType> EducationTypes { get; set; }
        public DbSet<Employer> Employers { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<FamilyStatus> FamilyStatuses { get; set; }
        public DbSet<ForeignLanguage> ForeignLanguages { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<JobSeeker> JobSeekers { get; set; }
        public DbSet<JobSeekerForeignLanguage> JobSeekerForeignLanguages { get; set; }
        public DbSet<LanguageLevel> LanguageLevels { get; set; }
        public DbSet<Logging> Loggings { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuRolePermission> MenuRolePermissions { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<PreviousWork> PreviousWorks { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Response> Responses { get; set; }
        public DbSet<Resume> Resumes { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<SocialStatus> SocialStatuses { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<TypeOfEmployment> TypeOfEmployments { get; set; }
        public DbSet<University> Universities { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Vacancy> Vacancies { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new BranchMap());
            builder.ApplyConfiguration(new CitizenshipMap());
            builder.ApplyConfiguration(new CountryMap());
            builder.ApplyConfiguration(new CultureMap());
            builder.ApplyConfiguration(new CurrencyMap());
            builder.ApplyConfiguration(new DistrictMap());
            builder.ApplyConfiguration(new EducationMap());
            builder.ApplyConfiguration(new EducationTypeMap());
            builder.ApplyConfiguration(new EmployerMap());
            builder.ApplyConfiguration(new ExperienceMap());
            builder.ApplyConfiguration(new FamilyStatusMap());
            builder.ApplyConfiguration(new ForeignLanguageMap());
            builder.ApplyConfiguration(new GenderMap());
            builder.ApplyConfiguration(new JobSeekerForeignLanguageMap()); 
            builder.ApplyConfiguration(new JobSeekerMap()); 
            builder.ApplyConfiguration(new LanguageLevelMap());
            builder.ApplyConfiguration(new LoggingMap());
            builder.ApplyConfiguration(new MenuMap()); 
            builder.ApplyConfiguration(new MenuRolePermissionMap());
            builder.ApplyConfiguration(new PermissionMap());
            builder.ApplyConfiguration(new PreviousWorkMap());
            builder.ApplyConfiguration(new RegionMap());
            builder.ApplyConfiguration(new ResourceMap());
            builder.ApplyConfiguration(new ResponseMap());
            builder.ApplyConfiguration(new ResumeMap());
            builder.ApplyConfiguration(new RoleMap());
            builder.ApplyConfiguration(new ScheduleMap());
            builder.ApplyConfiguration(new SocialStatusMap());
            builder.ApplyConfiguration(new SubscriptionMap());
            builder.ApplyConfiguration(new TypeOfEmploymentMap());
            builder.ApplyConfiguration(new UniversityMap());
            builder.ApplyConfiguration(new UserMap());
            builder.ApplyConfiguration(new VacancyMap());
        }
    }
}
