namespace The_Outfit.Models
{
    public interface ICartService
    {
        public CartItems GetItem(int id, string userId);
        public void Add(CartItems cartItem);
        public void Update(CartItems cartItem);
        public List<CartItems> GetItemsByUser(string userId);
        public void Remove(CartItems cartItem);

    }
}
