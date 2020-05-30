using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
//using homework1.Models;

namespace homework1 {
    [Route ("api/[controller]")]
    [ApiController]
    public class OptionsController : ControllerBase {
        private readonly AppSettings settings;
        public OptionsController (IOptions<AppSettings> options) {
            this.settings = options.Value;

        }
        // GET api/options/5
        [HttpGet ("")]
        public ActionResult<AppSettings> GetAppSettings () {
            return this.settings;
        }
    }
}