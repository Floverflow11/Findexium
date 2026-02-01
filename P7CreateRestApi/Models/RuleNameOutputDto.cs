namespace P7CreateRestApi.Models;

public record RuleNameOutputDto(
    int Id,
    string Name,
    string Description,
    string Json,
    string Template,
    string SqlStr,
    string SqlPart);