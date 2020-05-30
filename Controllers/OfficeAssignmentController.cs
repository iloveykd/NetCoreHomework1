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
    public class OfficeAssignmentController : ControllerBase {
        private readonly ContosoUniversityContext db;
        public OfficeAssignmentController (ContosoUniversityContext _db) {
            this.db = _db;
        }

        // GET api/officeassignment/get
        [HttpGet]
        public ActionResult<IEnumerable<OfficeAssignment>> GetOfficeAssignments () {

            return db.OfficeAssignment.Include(office => office.Instructor).ToList();
        }

        // POST api/officeassignment/add
        [HttpPost]
        public ResultModel PostOfficeAssignment (OfficeAssignment _office) { 
            var result = new ResultModel();
            
            db.OfficeAssignment.Add(new OfficeAssignment{InstructorId = _office.InstructorId,Location = _office.Location});

            db.SaveChanges();

            result.Data = _office.InstructorId;
            result.IsSuccess = true;

            return result;
        }

        // PUT api/officeassignment/5
        [HttpPut ("{id}")]
        public ResultModel PutOfficeAssignment (int id, OfficeAssignment _office) { 

            var result = new ResultModel();

            if (id != _office.InstructorId)
            {
                result.Message = BadRequest().ToString();
                result.IsSuccess = false;
                return result;
            }

            db.Entry(_office).State = EntityState.Modified;

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

        // DELETE api/officeassignment/5
        [HttpDelete ("{id}")]
        public ResultModel DeleteOfficeAssignmentById (int id) { 

            var result = new ResultModel();

            var OfficeAssignmentItem = db.OfficeAssignment.Find(id);
            if (OfficeAssignmentItem == null)
            {
                result.Message = NotFound().ToString();
                result.IsSuccess = false;
                return result;
            }

            db.OfficeAssignment.Remove(OfficeAssignmentItem);
            db.SaveChanges();

            return result;
        }


        
        private bool TodoItemExists(long id) =>
         db.OfficeAssignment.Any(e => e.InstructorId == id);
    }
}