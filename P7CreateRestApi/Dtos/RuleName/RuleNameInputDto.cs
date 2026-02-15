namespace P7CreateRestApi.Dtos.RuleName;

public class RuleNameInputDto
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string Json { get; set; }
    public required string Template { get; set; }
    public required string SqlStr { get; set; }
    public required string SqlPart { get; set; }
}