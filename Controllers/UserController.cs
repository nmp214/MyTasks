using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyTasks.Models;
using MyTasks.Services;
using MyTasks.Interfaces;


namespace MyTasks.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private IUser userSer;
        AdminController u = new AdminController();

        public UserController(IUser userSer)
        {
            this.userSer = userSer;
        }

        [HttpGet]
        [Authorize(Policy = "Admin")]
        public IEnumerable<User> Get()
        {
            return userSer.GetAll();
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "Admin")]
        public ActionResult<User> Get(int id)
        {
            var t = userSer.Get(id);
            if (t == null)
                return NotFound();
            return t;
        }

        [HttpPost]
        [Authorize(Policy = "Admin")]
        public ActionResult Post(User user)
        {
            userSer.Add(user);
            return CreatedAtAction(nameof(Post), new { id = user.UserId }, user);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "Admin")]
        public ActionResult Put(int id, User user)
        {
            if (!userSer.Update(id, user))
                return BadRequest();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "Admin")]
        public ActionResult Delete(int id)
        {
            if (!userSer.Delete(id))
                return NotFound();
            return NoContent();
        }
    }

}