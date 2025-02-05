using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The_Outfit.Models;

namespace Application
{
    public class CartServiceRepository:ICartService
    {
        private readonly ICart _Cart;

        public CartServiceRepository(ICart cart)
        {
            _Cart = cart;
        }
        public CartItems GetItem(int id, string userId)
        {
            return _Cart.GetItem(id, userId);  
        }
        public void Add(CartItems cartItem)
        {
            _Cart.Add(cartItem);
        }
        public void Update(CartItems cartItem)
        {
            _Cart.Update(cartItem);
        }
        public List<CartItems> GetItemsByUser(string userId)
        {
            return _Cart.GetItemsByUser(userId);
        }
        public void Remove(CartItems cartItem)
        {
            _Cart.Remove(cartItem);
        }
    }
}
