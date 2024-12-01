namespace Notes.Models;

public class NotesModel
{
    public NotesModel(string title, string description)
    {
        Title = title;
        Description = description;
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
        Id = Guid.NewGuid();
    }
    
    public Guid Id { get; init; }
    
    public string Title { get; init; }
    
    public string Description { get; init; }
    
    public DateTime CreatedAt { get; init; }
    
    public DateTime UpdatedAt { get; init; }
    
}