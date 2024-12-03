namespace Notes.Contracts;

public record GetNotesRequestContract(string? Search, string? SortItem, string? SortOrder, int PageNumber, int PageSize); // nullable fields
// use Capitals in the parameters names