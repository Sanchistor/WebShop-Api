using Microsoft.Extensions.Hosting;
using System.Text.Json.Serialization;

namespace WebShop.WebShop.Data.Models
{
    public class Profile
    {
        public int Id { get; set; }
        [JsonIgnore]
        public int UserId { get; set; }
        public string NickName { get; set; }
        public DateTime Created { get; set; }
        [JsonIgnore]
        public User User { get; set; } = null!;
        [JsonIgnore]
        public ICollection<Product> Products { get; } = new List<Product>();
    }
}
