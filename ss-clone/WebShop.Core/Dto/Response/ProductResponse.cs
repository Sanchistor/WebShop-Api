using WebShop.WebShop.Data.Models;

namespace WebShop.WebShop.Core.Dto.Response
{
    public class ProductResponse
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public string ShortBio { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public float Price { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public Profile Profile { get; set; }
    }
}
