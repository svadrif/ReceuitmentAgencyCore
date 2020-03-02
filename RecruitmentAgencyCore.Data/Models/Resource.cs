using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitmentAgencyCore.Data.Models
{
    public class Resource
    {
        public int Id { get; set; }
        public string Key { get; set; }     // ключ
        public string Value { get; set; }   // значение

        public int? CultureId { get; set; }
        public Culture Culture { get; set; }
    }
}
