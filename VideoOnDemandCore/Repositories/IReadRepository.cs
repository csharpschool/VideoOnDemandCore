using System.Collections.Generic;
using VideoOnDemandCore.Entities;

namespace VideoOnDemandCore.Repositories
{
    public interface IReadRepository
    {
        IEnumerable<Course> GetCourses(string userId);
        Course GetCourse(string userId, int courseId);
    }
}
