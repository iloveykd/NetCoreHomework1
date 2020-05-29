using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

//using homework1.Models;

namespace homework1
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ContosoUniversityContext db;
        public CourseController(ContosoUniversityContext db)
        {
            this.db = db;
        }

        // GET api/course
        [HttpGet]
        public ActionResult<IEnumerable<Course>> GetCourse()
        {
            return db.Course.Include(a=>a.Department).Where(delete => delete.IsDeleted != true).ToList();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Course>> GetCourse(int id)
        {
            var course = await db.Course.FindAsync(id);

            if (course == null)
            {
                return NotFound();
            }

            return course;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Course>> PostCourse(Course course)
        {
            db.Course.Add(course);
            await db.SaveChangesAsync();

            return CreatedAtAction("GetCourse", new { id = course.CourseId }, course);
        }



        // // POST api/course/
        // [HttpPost]
        // public ResultModel AddCourse (Course _Course) {
        //     var result = new ResultModel();
            
        //     db.Course.Add(new Course{Title=_Course.Title,Credits = _Course.Credits,DepartmentId = _Course.DepartmentId,DateModified=DateTime.UtcNow});

        //     db.SaveChanges();

        //     result.Data = _Course.CourseId;
        //     result.IsSuccess = true;

        //     return result;
        // }

        // PUT api/course/5
        [HttpPut("{id}")]
        public ResultModel UpdateCourse(int id, Course _Course)
        {
            var result = new ResultModel();
            _Course.DateModified = DateTime.UtcNow;

            if (id != _Course.CourseId)
            {
                result.Message = BadRequest().ToString();
                result.IsSuccess = false;
                return result;
            }

            db.Entry(_Course).State = EntityState.Modified;

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

        // DELETE api/course/5
        [HttpDelete("{id}")]
        public ResultModel DeleteCourseById(int id)
        {
            var result = new ResultModel();

            var CourseItem = db.Course.Find(id);
            if (CourseItem == null)
            {
                result.Message = NotFound().ToString();
                result.IsSuccess = false;
                return result;
            }

            // CourseItem.IsDeleted = true;

            db.Course.Remove(CourseItem);
            db.SaveChanges();

            

            // db.Entry(CourseItem).State = EntityState.Modified;

            // try
            // {
            //     db.SaveChanges();
            // }
            // catch (DbUpdateConcurrencyException)
            // {
            //     if (!TodoItemExists(id))
            //     {
            //         result.Message = NotFound().ToString();
            //         result.IsSuccess = false;
            //         return result;
            //     }
            //     else
            //     {
            //         throw;
            //     }
            // }

            return result;

        }


        private bool TodoItemExists(long id) =>
         db.Course.Any(e => e.CourseId == id);
    }
}