using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//using homework1.Models;

namespace homework1.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase {
        private readonly ContosoUniversityContext db;
        public EnrollmentController (ContosoUniversityContext _db) {
            this.db = _db;
        }

        // GET api/enrollment
        [HttpGet ("get")]
        public ActionResult<IEnumerable<Enrollment>> GetEnrollments () {
            return db.Enrollment.Include(enrollment => enrollment.Student).ToList();
        }

        // POST api/enrollment/add
        [HttpPost("add")]
        public ResultModel AddEnrollment (Enrollment _Enrollment) {
            var result = new ResultModel();
            
            db.Enrollment.Add(new Enrollment{CourseId = _Enrollment.CourseId,StudentId = _Enrollment.StudentId,
            Grade = _Enrollment.Grade});

            db.SaveChanges();

            result.Data = _Enrollment.CourseId;
            result.IsSuccess = true;

            return result;
        }

        // PUT api/enrollment/update/5
        [HttpPut("update/{id}")]
        public ResultModel UpdateEnrollment(int id, Enrollment _Enrollment)
        {
            var result = new ResultModel();

            if (id != _Enrollment.CourseId)
            {
                result.Message = BadRequest().ToString();
                result.IsSuccess = false;
                return result;
            }

            db.Entry(_Enrollment).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoItemExists(id))
                {
                    result.Message = NotFound().ToString();
                    result.IsSuccess = false;
                    return result;
                }
                else
                {
                    throw;
                }
            }

            result.IsSuccess = true;
            return result;

        }

        // DELETE api/enrollment/delete/5
        [HttpDelete("delete/{id}")]
        public ResultModel DeleteEnrollmentById(int id)
        {
            var result = new ResultModel();

            var EnrollmentItem = db.Enrollment.Find(id);
            if (EnrollmentItem == null)
            {
                result.Message = NotFound().ToString();
                result.IsSuccess = false;
                return result;
            }

            db.Enrollment.Remove(EnrollmentItem);
            db.SaveChanges();

            return result;

        }


        private bool TodoItemExists(long id) =>
         db.Enrollment.Any(e => e.EnrollmentId == id);

         
    }
}