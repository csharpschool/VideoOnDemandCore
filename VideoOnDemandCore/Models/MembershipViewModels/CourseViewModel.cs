using System.Collections.Generic;
using VideoOnDemandCore.Models.DTOModels;

namespace VideoOnDemandCore.Models.MembershipViewModels
{
    public class CourseViewModel
    {
        public CourseDTO Course { get; set; }
        public InstructorDTO Instructor { get; set; }
        public IEnumerable<ModuleDTO> Modules { get; set; }
    }
}
