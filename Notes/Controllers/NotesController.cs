using Microsoft.AspNetCore.Mvc;
using Notes.Contracts;
using Notes.DataAccess;
using Notes.Models; // controller base import to inherit class NotesController

namespace Notes.Controllers;

[ApiController]  // attribute for this controller
[Route("[controller]")] // route for this controller
public class NotesController : ControllerBase
{
    private readonly NotesDbContext _dbContext;  // create a field for storing db context

    public NotesController(NotesDbContext dbContext)  // constructor method must have the same name as class
    {
        _dbContext = dbContext;
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateNotesContract request, CancellationToken ct) // put the creation method to external class "CreateNotesContract"
    {
        var note = new NotesModel(request.Title, request.Description); 
        // obtain parameters from req and store into notes var 
        
        await _dbContext.NotesModels.AddAsync(note, ct);  // add NotesModel object to db context
        await _dbContext.SaveChangesAsync(ct);  // save changes to db 
        // ToDo: learn about CancellationToken (csrf protection?) 
        
        return Ok("Post");
    }
    
    [HttpGet]  // attribute for this method 
    public async Task<IActionResult> Get()
    {
        // ToDo: make pagination
        
        
        return Ok("Get");
    }
    

    [HttpPut]
    public async Task<IActionResult> Update()
    {
        // ToDo: add implementation of Update method
        return Ok("Put");
    }

    [HttpDelete]
    public async Task<IActionResult> Delete()
    {
        // ToDo: add implementation of Delete method
        return Ok("Delete");
    }
}