using EventPlatform.Api.Classes;
using EventPlatform.Api.Interfaces;
using EventPlatform.Entities.DTO;
using EventPlatform.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Task = System.Threading.Tasks.Task;
using TaskECP = EventPlatform.Entities.ECP.Task;

namespace EventPlatform.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TaskController
    : CustomController<ITaskService>
{
    public TaskController(ITaskService service) 
        : base(service)
    {
    }

    [HttpPost]
    [Route("addTask")]
    public async Task<ActionResult<PostResult<TaskECP, TaskDto>>> AddTask(Guid? sessionToken, TaskECP? task)
    {
        if (sessionToken is null || task is null)
            return BadRequest("Missing parameters");

        return Ok(await Task.FromResult(_service.AddTask((Guid)sessionToken, task)));
    }

    [HttpDelete]
    [Route("deleteTask")]
    public async Task<ActionResult<DeleteResult<TaskECP, TaskDto>>> DeleteTask(Guid? sessionToken, TaskECP? task)
    {
        if (sessionToken is null || task is null)
            return BadRequest("Missing parameters");

        return Ok(await Task.FromResult(_service.DeleteTask((Guid)sessionToken, task)));
    }

    [HttpPut]
    [Route("updateTask")]
    public async Task<ActionResult<PutResult<TaskECP, TaskDto>>> UpdateTask(Guid? sessionToken, TaskECP? task)
    {
        if (sessionToken is null || task is null)
            return BadRequest("Missing parameters");

        return Ok(await Task.FromResult(_service.UpdateTask((Guid)sessionToken, task)));
    }
}
