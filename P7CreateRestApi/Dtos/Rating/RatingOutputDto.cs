namespace P7CreateRestApi.Dtos.Rating;

public record RatingOutputDto(int Id, string MoodysRating, string SandPRating, string FitchRating, byte? OrderNumber);