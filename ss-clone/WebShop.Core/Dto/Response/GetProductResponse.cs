namespace WebShop.WebShop.Core.Dto.Response
{
    public class GetProductResponse
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public string ShortBio { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public float Price { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public ProfileDTO Profile { get; set; }
        public SectionDto Section { get; set; }
        public CategoryDto Category { get; set; }
        public BrandDto Brand { get; set; }
    }
    public class ProfileDTO
    {
        public int Id { get; set; }
        public string NickName { get; set; }
        public DateTime Created { get; set; }
    }
    public class BaseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
    }
    public class SectionDto : BaseDto
    {
    }
    public class CategoryDto : BaseDto
    {

    }
    public class BrandDto : BaseDto
    {

    }
}
