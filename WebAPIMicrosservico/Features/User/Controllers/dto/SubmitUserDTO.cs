namespace WebAPIMicrosservico.Features.User.Controllers.dto
{
    public class SubmitUserDTO
    {
        public string Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }

        public string? Message { get; set; }
    }
}
