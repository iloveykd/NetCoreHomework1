using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using homework1.Helpers;
using Microsoft.AspNetCore.Mvc;
//using homework1.Models;

namespace homework1 {
    [Route ("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase {
        private readonly JwtHelpers jwt;
        public TokenController (JwtHelpers _jwt) {
            this.jwt = _jwt;
        }

        // GET api/token
        [HttpGet ("")]
        public ActionResult<IEnumerable<string>> Getstrings () {
            return new List<string> { };
        }

        // GET api/token/5
        [HttpGet ("{id}")]
        public ActionResult<string> GetstringById (int id) {
            return null;
        }

        // POST api/token
        [HttpPost ("")]
        public ActionResult<string> Poststring (LoginViewModel login) {
            if (ValidateUser (login)) {
                return jwt.GenerateToken (login.Username);
            } else {
                return BadRequest ();
            }
        }

        private bool ValidateUser (LoginViewModel login) {
            return true; // TODO
        }

        // PUT api/token/5
        [HttpPut ("{id}")]
        public void Putstring (int id, string value) { }

        // DELETE api/token/5
        [HttpDelete ("{id}")]
        public void DeletestringById (int id) { }

        public class LoginViewModel {
            public string Username { get; set; }
            public string Password { get; set; }
        }
    }
}