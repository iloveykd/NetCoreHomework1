using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//using homework1.Models;

namespace homework1 {
    [Route ("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase {
        private readonly ContosoUniversityContext db;
        public CoursesController (ContosoUniversityContext _db) {
            this.db = _db;
        }

        // GET api/courses
        [HttpGet ("students")]
        public ActionResult<IEnumerable<VwCourseStudents>> GetVwCourseStudents () {

            return db.VwCourseStudents.FromSqlRaw("SELECT * FROM dbo.vwCourseStudents").ToList();

        }

        // GET api/courses
        [HttpGet ("studentcount")]
        public ActionResult<IEnumerable<VwCourseStudentCount>> GetVwCourseStudentCount () {

            return db.VwCourseStudentCount.FromSqlRaw("SELECT * FROM dbo.VwCourseStudentCount").ToList();

        }

        // GET api/courses
        [HttpGet ("departmentcount")]
        public ActionResult<IEnumerable<VwDepartmentCourseCount>> GetVwDepartmentCourseCount() {

            return db.VwDepartmentCourseCount.FromSqlRaw("SELECT * FROM dbo.VwDepartmentCourseCount").ToList();

        }

        // GET api/courses/5
        [HttpGet ("{id}")]
        public ActionResult<VwCourseStudents> GetVwCourseStudentsById (int id) {
            return null;
        }

        // POST api/courses
        [HttpPost]
        public void PostVwCourseStudents (VwCourseStudents value) { }

        // PUT api/courses/5
        [HttpPut ("{id}")]
        public void PutVwCourseStudents (int id, VwCourseStudents value) { }

        // DELETE api/courses/5
        [HttpDelete ("{id}")]
        public void DeleteVwCourseStudentsById (int id) { }
    }
}