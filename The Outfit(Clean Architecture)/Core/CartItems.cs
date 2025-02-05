namespace The_Outfit.Models
{
    public class CartItems
    {
        public string UserId { get; set; }  
        public int id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal Discprice { get; set; }
        public string Image { get; set; }
        public string Category { get; set; }
        public int Quantity { get; set; }
    }

}
