namespace WebShop.WebShop.Core.Dto.Products
{
    public class GetProductsByPriceDto
    {
        public float? minPrice { get; set; } = 0;
        public float maxPrice { get; set; }
    }
}
