using System.Text.Json.Serialization;

namespace WebShop.WebShop.Data.Models
{
    public class Product
    {
        public int Id { get; set; }
        [JsonIgnore]
        public int ProfileId { get; set; }
        public int Year { get; set; }
        public string ShortBio { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public float Price { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public Profile Profile { get; set; } = null!;

        public Section Section { get; set; } = null!;
        [JsonIgnore]
        public int SectionId { get; set; }

        public Category Category { get; set; } = null!;
        [JsonIgnore]
        public int CategoryId { get; set; }

        public Brand Brand { get; set; } = null!;
        [JsonIgnore]
        public int BrandId { get; set; }
    }
}
