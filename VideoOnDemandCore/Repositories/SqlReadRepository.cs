using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoOnDemandCore.Data;
using VideoOnDemandCore.Entities;

namespace VideoOnDemandCore.Repositories
{
    public class SqlReadRepository : IReadRepository
    {
        private ApplicationDbContext _db;
        public SqlReadRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Course> GetCourses(string userId)
        {
            var courses = _db.UserCourses.Where(uc =>
               uc.UserId.Equals(userId))
                .Join(_db.Courses, uc => uc.CourseId, c => c.Id,
                     (uc, c) => new { Course = c })
                .Select(s => s.Course);

            foreach (var course in courses)
            {
                course.Instructor = _db.Instructors.SingleOrDefault(
                    s => s.Id.Equals(course.InstructorId));
                course.Modules = _db.Modules.Where(
                    m => m.CourseId.Equals(course.Id)).ToList();
            }

            return courses;
        }

        public Course GetCourse(string userId, int courseId)
        {
            throw new NotImplementedException();
        }

        public Video GetVideo(string userId, int videoId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Video> GetVideos(string userId, int moduleId = 0)
        {
            throw new NotImplementedException();
        }
    }
}
