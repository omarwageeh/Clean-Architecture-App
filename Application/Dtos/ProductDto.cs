namespace Application.Dtos
{
    public class ProductDto
    {
        public string Name { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public decimal Price { get; set; }
        public string Merchant { get; set; } = String.Empty;
    }
}
