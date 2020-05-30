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
    public class PersonController : ControllerBase {
        private readonly ContosoUniversityContext db;
        public PersonController (ContosoUniversityContext _db) {
            this.db = _db;
        }

        // GET api/person
        [HttpGet]
        public ActionResult<IEnumerable<Person>> GetPersons () {
            return db.Person.Where(delete => delete.IsDeleted != true).ToList();
        }

        // POST api/person
        [HttpPost]
        public ResultModel PostPerson (Person _person) { 
            var result = new ResultModel();
            
            db.Person.Add(new Person{LastName = _person.LastName,FirstName = _person.FirstName,
                            HireDate = _person.HireDate,EnrollmentDate = _person.EnrollmentDate,
                            Discriminator = _person.Discriminator,DateModified = DateTime.UtcNow});

            db.SaveChanges();

            result.Data = _person.Id;
            result.IsSuccess = true;

            return result;
        }

        // PUT api/person/5
        [HttpPut ("{id}")]
        public ResultModel PutPerson (int id, Person _person) { 
            var result = new ResultModel();

            if (id != _person.Id)
            {
                result.Message = BadRequest().ToString();
                result.IsSuccess = false;
                return result;
            }
            _person.DateModified = DateTime.UtcNow;

            db.Entry(_person).State = EntityState.Modified;

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

        // DELETE api/person/5
        [HttpDelete ("{id}")]
        public ResultModel DeletePersonById (int id) { 
            var result = new ResultModel();

            var PersonItem = db.Person.Find(id);
            if (PersonItem == null)
            {
                result.Message = NotFound().ToString();
                result.IsSuccess = false;
                return result;
            }
            PersonItem.IsDeleted = true;

            // db.Person.Remove(PersonItem);
            // db.SaveChanges();

            db.Entry(PersonItem).State = EntityState.Modified;

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

            return result;
        }


        private bool TodoItemExists(long id) =>
         db.Person.Any(e => e.Id == id);
    }
}