namespace P7CreateRestApi.Dtos.Rating;

public class RatingInputDto
{
    public required string MoodysRating { get; set; }
    public required string SandPRating { get; set; }
    public required string FitchRating { get; set; }
    public byte? OrderNumber { get; set; }
}