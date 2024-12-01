using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notes.Contracts;
using Notes.DataAccess;
using Notes.Models; // controller base import to inherit class NotesController

namespace Notes.Controllers;

[ApiController]  // attribute for this controller
[Route("[controller]")] // route for this controller
public class NotesController : ControllerBase
{
    private readonly NotesDbContext _dbContext;  // create a field for storing db context

    private readonly ILogger<NotesController> _logger;
    
    public NotesController(NotesDbContext dbContext, ILogger<NotesController> logger)  // constructor method must have the same name as class
    {
        _dbContext = dbContext;
        _logger = logger;
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateNotesRequestContract request, CancellationToken ct) // put the creation method to external class "CreateNotesContract"
    {
        var note = new NotesModel(request.Title, request.Description); 
        // obtain parameters from req and store into notes var 
        
        await _dbContext.Notes.AddAsync(note, ct);  // add NotesModel object to db context
        await _dbContext.SaveChangesAsync(ct);  // save changes to db 
        // ToDo: learn about CancellationToken (csrf protection?) 
        
        return Ok("Post");
    }
    
    [HttpGet]  // attribute for this method 
    public async Task<IActionResult> Get([FromQuery] GetNotesRequestContract request, CancellationToken ct)
    {
        try
        {
            // search filtering 
            var notesQuery = _dbContext.Notes
                .Where(n => string.IsNullOrEmpty(request.Search) || 
                            n.Title.ToLower().Contains(request.Search.ToLower())
                            || n.Description.ToLower().Contains(request.Search.ToLower()));

            if (!string.IsNullOrEmpty(request.SortItem))
            {
                var selectorKey = GetSelectorKey(request.SortItem);
                // sorting 
                if (request.SortOrder == "desc")
                {
                    notesQuery = notesQuery.OrderByDescending(selectorKey);
                }
                else if (request.SortOrder == "asc")
                {
                    notesQuery = notesQuery.OrderBy(selectorKey);
                }
            } 
            
            // ToDo: Check pagination is correct
            // Pagination logic
            var pageNumber = request.PageNumber > 0 ? request.PageNumber : 1; // Default to page 1
            var pageSize = request.PageSize > 0 ? request.PageSize : 10;     // Default to 10 items per page

            var totalRecords = await notesQuery.CountAsync(ct); // Get the total count of records
            var notesDtos = await notesQuery
                .Skip((pageNumber - 1) * pageSize) // Skip records for previous pages
                .Take(pageSize)                   // Take the records for the current page
                .Select(n => new NoteDto(n.Id, n.Title, n.Description, n.CreatedAt, n.UpdatedAt))
                .ToListAsync(ct);
        
            return Ok(new GetNotesResponseContract(notesDtos));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while processing the Get request.");
            return StatusCode(500, new { message = "Internal server error", details = e.Message });
        }
        
    }

    private Expression<Func<NotesModel, object>> GetSelectorKey(string sortItem)
    {
        switch (sortItem)
        {
            case "createdAt": return note => note.CreatedAt;
            case "title": return note => note.Title;
            default: return note => note.Id;
        }
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