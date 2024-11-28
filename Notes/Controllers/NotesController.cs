using Microsoft.AspNetCore.Mvc;  // controller base import to inherit class NotesController

namespace Notes.Controllers;

[ApiController]  // attribute for this controller
[Route("[controller]")] // route for this controller
public class NotesController : ControllerBase
{
    [HttpGet]  // attribute for this method 
    public async Task<IActionResult> Get()
    {
        return Ok("Get");
    }
    
    [HttpPost]
    public async Task<IActionResult> Post()
    {
        return Ok("Post");
    }

    [HttpPut]
    public async Task<IActionResult> Put()
    {
        return Ok("Put");
    }

    [HttpDelete]
    public async Task<IActionResult> Delete()
    {
        return Ok("Delete");
    }
}