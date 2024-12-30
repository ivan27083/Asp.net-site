namespace My_site.Domain.Models
{
    public class CatalogFilterViewModel
    {
        public float MinPrice { get; set; }
        public float MaxPrice { get; set; }
        public List<string> GPU { get; set; } = [];
        public float MinRating { get; set; }
    }
}
