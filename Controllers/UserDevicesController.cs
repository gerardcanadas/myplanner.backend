using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using devices.Db;
using devices.Db.Repositories;
using devices.Models;
using Microsoft.AspNetCore.Mvc;

namespace devices.Controllers
{
    [Route("api/[controller]")]
    public class UserDevicesController : Controller
    {
        private UserDeviceRespository udevRepo = null;

        public UserDevicesController()
        {
            AppDb db = new AppDb();
            udevRepo = new UserDeviceRespository(db);
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            UserDevice res = await udevRepo.FindUserDevicesAsync(id);
            return Ok(res);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]UserDevice value)
        {
            await udevRepo.InsertUserDeviceAsync(value);
            return Ok();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
