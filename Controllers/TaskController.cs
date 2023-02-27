using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using MyTasks.Interfaces;
using MyTasks.Models;
using MyTask = MyTasks.Models.MyTask;


namespace MyTasks.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class TasksController : ControllerBase
    {

        private ITask taskSer;

        [HttpGet]
        public IEnumerable<MyTask> Get()
        {
            return taskSer.GetAll();
        }

        [HttpGet("{id}")]
        public ActionResult<MyTask> Get(int id)
        {
            var t = taskSer.Get(id);
            if (t == null)
                return NotFound();
            return t;
        }

        [HttpPost]
        public ActionResult Post(MyTask task)
        {
            taskSer.Add(task);
            return CreatedAtAction(nameof(Post), new { id = task.Id }, task);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, MyTask task)
        {
            if (!taskSer.Update(id, task))
                return BadRequest();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (!taskSer.Delete(id))
                return NotFound();
            return NoContent();
        }
    }
}