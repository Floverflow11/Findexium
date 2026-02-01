namespace P7CreateRestApi.Models;

public record RatingOutputDto(int Id, string MoodysRating, string SandPRating, string FitchRating, byte? OrderNumber);