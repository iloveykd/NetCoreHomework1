using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
//using homework1.Models;

namespace homework1 {
    [Route ("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase {
        private readonly ContosoUniversityContext db;
        public DepartmentController (ContosoUniversityContext _db) {
            this.db = _db;
        }

        // GET api/department
        [HttpGet ("get")]
        public ActionResult<IEnumerable<Department>> GetDepartments () {

            var temp = db.Department.ToList();
            return db.Department.Where(delete => delete.IsDeleted != true).ToList();
        }


        // POST api/department/add
        [HttpPost ("add")]
        public ResultModel PostDepartment (Department _department) { 

            
            var result = new ResultModel();

            db.Database.ExecuteSqlRaw("dbo.Department_Insert @Name = {0},@Budget = {1},@StartDate = {2},@InstructorID={3},@DateModified={4}",
            _department.Name,_department.Budget,_department.StartDate,_department.InstructorId,DateTime.UtcNow);


            return result;
        }

        // PUT api/department/update/5
        [HttpPut ("update/{id}")]
        public ResultModel PutDepartment (int id, Department _department) { 

            var result = new ResultModel();

            if (id != _department.DepartmentId)
            {
                result.Message = BadRequest().ToString();
                result.IsSuccess = false;
                return result;
            }

            db.Database.ExecuteSqlRaw("dbo.Department_Update @DepartmentID={0},@Name = {1},@Budget = {2},@StartDate = {3},@InstructorID={4},@RowVersion_Original={5},@DateModified={6}",
            _department.DepartmentId,_department.Name,_department.Budget,_department.StartDate,_department.InstructorId,_department.RowVersion,DateTime.UtcNow);

            return result;

        }

        // DELETE api/department/5/
        [HttpDelete ("delete/{id}")]
        public ResultModel DeleteDepartmentById (int id, string rowversion) {
            var result = new ResultModel();


            db.Database.ExecuteSqlRaw("dbo.Department_Delete @DepartmentID={0},@RowVersion_Original={1}",
                                        id,Convert.FromBase64String(rowversion));

            return result;
         }
    }
}