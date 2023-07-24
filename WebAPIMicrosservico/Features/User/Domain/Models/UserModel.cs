using Newtonsoft.Json;

namespace WebAPIMicrosservico.Features.User.Domain.Models
{
    public class UserModel
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Message { get; set; }
    }
}
