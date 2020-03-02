using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecruitmentAgencyCore.Helpers
{
    public static class Utility
    {
        public static IList<SelectListItem> GetDriverLicenses()
        {
            return new List<SelectListItem>
            {
                 new SelectListItem { Text = "A", Value = "A" },
                 new SelectListItem { Text = "B", Value = "B" },
                 new SelectListItem { Text = "C", Value = "C" },
                 new SelectListItem { Text = "D", Value = "D" },
                 new SelectListItem { Text = "E", Value = "E" }
            };
        }
    }
}
