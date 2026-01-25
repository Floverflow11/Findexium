namespace Dot.Net.WebApi.Controllers.Domain
{
    public class Rating
    {
        public int Id { get; set; }
        public required string MoodysRating { get; set; }
        public required string SandPRating { get; set; }
        public required string FitchRating { get; set; }
        public byte? OrderNumber { get; set; }
    }
}