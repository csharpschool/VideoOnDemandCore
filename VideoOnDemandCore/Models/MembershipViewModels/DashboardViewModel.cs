using System.Collections.Generic;
using VideoOnDemandCore.Models.DTOModels;

namespace VideoOnDemandCore.Models.MembershipViewModels
{
    public class DashboardViewModel
    {
        public List<List<CourseDTO>> Courses { get; set; }
    }
}
