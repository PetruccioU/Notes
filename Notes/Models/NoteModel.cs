namespace Notes.Models;

public class NoteModel
{
    public NoteModel(string title, string description)
    {
        Title = title;
        Description = description;
        CreatedAt = DateTime.Now;
        Id = Guid.NewGuid();
    }
    
    public Guid Id { get; set; }
    
    public string Title { get; set; }
    
    public string Description { get; set; }
    
    public DateTime CreatedAt { get; set; }
}