using System;
using System.Collections.Generic;

namespace RecruitmentAgencyCore.Data.Models
{
    public class Vacancy
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; } // картинка вакансии, не обязательно
        public string Description { get; set; }
        public string Requirements { get; set; } // Требования
        public string Responsibility { get; set; } // Обязанности
        public string Conditions { get; set; } // Условия работы
        public string Address { get; set; }
        public int SalaryFrom { get; set; }
        public int SalaryTo { get; set; }
        public int CurrencyId { get; set; }
        public Currency Currency { get; set; }

        public int? EmployerId { get; set; }
        public virtual Employer Employer { get; set; }
        public int? BranchId { get; set; }
        public virtual Branch Branch { get; set; }
        public int? ExperienceId { get; set; }
        public virtual Experience Experience { get; set; }
        public int? ScheduleId { get; set; } // график работы
        public virtual Schedule Schedule { get; set; }
        public int? EducationTypeId { get; set; }
        public virtual EducationType EducationType { get; set; }    

        public virtual ICollection<Response> Responses { get; set; }

        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

    }
}