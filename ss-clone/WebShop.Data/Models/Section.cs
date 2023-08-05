using System.Text.Json.Serialization;

namespace WebShop.WebShop.Data.Models
{
    public class Section
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }

        public ICollection<Category> Categories { get; } = new List<Category>();
        [JsonIgnore]
        public ICollection<Product> Products { get; } = new List<Product>();
    }
}
