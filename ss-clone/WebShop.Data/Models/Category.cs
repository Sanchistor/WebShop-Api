using System.Text.Json.Serialization;

namespace WebShop.WebShop.Data.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        [JsonIgnore]
        public Section Section { get; set; } = null!;
        public ICollection<Brand> Brands { get; } = new List<Brand>();
        [JsonIgnore]
        public int SectionId { get; set; }
        [JsonIgnore]
        public ICollection<Product> Products { get; } = new List<Product>();
    }
}
