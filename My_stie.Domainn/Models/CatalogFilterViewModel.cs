namespace My_site.ViewModels
{
    public class CatalogFilterViewModel
    {
        public float MinPrice { get; set; }
        public float MaxPrice { get; set; }
        public List<string> GPU { get; set; } = [];
        public float MinRating { get; set; }
    }
}
