using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitmentAgencyCore.Data.Models
{
    public class Logging
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }
        public string IpAddress { get; set; }
        public DateTime? ActionTime { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string MethodType { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }
    }
}