using System.Text.Json.Serialization;

namespace WebShop.WebShop.Data.Models
{
    public class Brand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        [JsonIgnore]
        public Category Category { get; set; } = null!;
        [JsonIgnore]
        public int CategoryId { get; set; }
        [JsonIgnore]
        public ICollection<Product> Products { get; } = new List<Product>();
    }
}