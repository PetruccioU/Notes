namespace Notes.Contracts;

public record GetNotesRequestContract(string? Search, string? SortItem, string? SortOrder); // nullable fields
// use Capitals in the parameters names