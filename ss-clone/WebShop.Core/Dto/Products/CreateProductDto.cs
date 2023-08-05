namespace WebShop.WebShop.Core.Dto.Products
{
    public class CreateProductDto
    {
        public int ProfileId { get; set; }
        public int Year { get; set; }
        public string ShortBio { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public float Price { get; set; }
        public int SectionId { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
    }
}
