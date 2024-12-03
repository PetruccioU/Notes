namespace Notes.Contracts;

public record UpdateNotesRequestContract(Guid Id, string? Title, string Description);