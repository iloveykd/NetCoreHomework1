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
    public class CourseInstructorController : ControllerBase {
        private readonly ContosoUniversityContext db;
        public CourseInstructorController (ContosoUniversityContext _db) {
            this.db = _db;
        }

        // GET api/courseinstructor/get
        [HttpGet("get")]
        public ActionResult<IEnumerable<CourseInstructor>> GetCourseInstruct()
        {
            var test = db.CourseInstructor.ToList();
            return test;
        }
        

        // POST api/courseinstruct/add
        [HttpPost("add")]
        public ResultModel AddCourse (CourseInstructor _CourseInstructor) {
            var result = new ResultModel();
            
            db.CourseInstructor.Add(new CourseInstructor{CourseId = _CourseInstructor.CourseId,InstructorId = _CourseInstructor.InstructorId});

            db.SaveChanges();

            result.Data = _CourseInstructor.CourseId;
            result.IsSuccess = true;

            return result;
        }

        // PUT api/courseinstruct/update/5
        [HttpPut("update/{id}")]
        public ResultModel UpdateCourse(int id, CourseInstructor _CourseInstructor)
        {
            var result = new ResultModel();

            if (id != _CourseInstructor.CourseId)
            {
                result.Message = BadRequest().ToString();
                result.IsSuccess = false;
                return result;
            }

            db.Entry(_CourseInstructor).State = EntityState.Modified;

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

        // DELETE api/courseinstruct/delete/5
        [HttpDelete("delete/{id}")]
        public ResultModel DeleteCourseInstructorById(int id)
        {
            var result = new ResultModel();

            var CourseInstructorItem = db.CourseInstructor.Find(id);
            if (CourseInstructorItem == null)
            {
                result.Message = NotFound().ToString();
                result.IsSuccess = false;
                return result;
            }

            db.CourseInstructor.Remove(CourseInstructorItem);
            db.SaveChanges();

            return result;

        }


        private bool TodoItemExists(long id) =>
         db.CourseInstructor.Any(e => e.InstructorId == id);
    }
}