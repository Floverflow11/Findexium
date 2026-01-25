namespace Dot.Net.WebApi.Domain
{
    public class User
    {
        public int Id { get; set; }
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public required string Fullname { get; set; }
        public required string Role { get; set; }
    }
}